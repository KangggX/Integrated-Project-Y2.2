using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class AuthManager: MonoBehaviour
{
    //Firebase
    public FirebaseAuth auth;
    public DatabaseReference dbReference;

    //Retrieve user input
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;
    public string displayname;

    //Error Handling
    public TMP_Text errorMsgContent;

    //Check for transition
    public static bool authTransition = false;
    public static bool signOutTransition = false;

    private void Awake()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        Debug.Log(auth.CurrentUser.UserId);
        Debug.Log(auth.CurrentUser.DisplayName);
    }

    public async void Register()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();
        string username = usernameInput.text.Trim();

        if (ValidateUsername(username))
        {
            Debug.Log("Validation successful!");
            FirebaseUser newPlayer = await RegisterOnly(email, password, username);

            if (newPlayer != null)
            {
                await CreateNewSimplePlayer(newPlayer.UserId, username, username, newPlayer.Email);
                await UpdatePlayerDisplayName(username);
                authTransition = true;
                AudioClipManager.PlaySound("button");
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            errorMsgContent.text = "Error in registering. Invalid username.";
            errorMsgContent.gameObject.SetActive(true);
        }
        
    }

    public async Task<FirebaseUser> RegisterOnly(string email, string password, string username)
    {
        FirebaseUser newPlayer = null;

        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                if (task.Exception != null)
                {
                    errorMsgContent.gameObject.SetActive(true);
                    string errorMsg = this.HandleRegisterError(task);
                    errorMsgContent.text = errorMsg;
                    Debug.Log("Error registering: " + errorMsg);
                }

                //Debug.LogError("Sorry, there was an error creating your new account, ERROR: " + task.Exception);
                
            }
            else if (task.IsCompleted)
            {
                newPlayer = task.Result;
                Debug.Log("Welcome!" + newPlayer.Email);
                
            }
        });
        return newPlayer; 
    }

    public async Task UpdatePlayerDisplayName(string displayname)
    {
        if (auth.CurrentUser != null)
        {
            UserProfile profile = new UserProfile { DisplayName = displayname };
            await auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encounted an error" + task.Exception);
                    return;
                }
                authTransition = true;
                AudioClipManager.PlaySound("button");
                SceneManager.LoadScene(1);
                Debug.Log("User profile updated successfully");
                Debug.LogFormat("Get current user display name {0}", GetCurrentUserDisplayName());
            });
        }
    }

    public async Task CreateNewSimplePlayer(string uuid, string displayname, string username, string email)
    {
        SimpleGamePlayer newPlayer = new SimpleGamePlayer(displayname, username, email);
        Debug.LogFormat("Player details: {0}", newPlayer.PrintPlayer());

        //root/players/$uuid
        await dbReference.Child("players/" + uuid).SetRawJsonValueAsync(newPlayer.SimplerGamePlayerToJson());

        UpdatePlayerDisplayName(displayname);
    }
    
    public string GetCurrentUserDisplayName()
    {
        return auth.CurrentUser.DisplayName;
    }

    public FirebaseUser GetCurrentUser()
    {
        return auth.CurrentUser;
    }

    public async Task ReadUsername()
    {
        string currentUUID = auth.CurrentUser.UserId;

        Debug.Log(currentUUID);
        DatabaseReference dbSingleUserProfile = FirebaseDatabase.DefaultInstance.GetReference("players/" + currentUUID);
        await dbSingleUserProfile.GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("null");
                }
                else if (task.IsCompleted)
                { 
                    DataSnapshot snapshot = task.Result;
                    //Debug.Log(task.Result);
                    
                    SimpleGamePlayer player = JsonUtility.FromJson<SimpleGamePlayer>(snapshot.GetRawJsonValue());

                    displayname = player.displayname;
                    Debug.Log(displayname);
                    
                }
            });
    }

    public async void SignIn()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        FirebaseUser currentPlayer = await SignInOnly(email, password);

        if (currentPlayer != null)
        {
            await ReadUsername();
            await UpdatePlayerDisplayName(displayname);
        }

    }

    public async Task<FirebaseUser> SignInOnly(string email, string password)
    {

        FirebaseUser currentPlayer = null;
        
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                string errorMsg = this.HandleSignInError(task);
                errorMsgContent.text = errorMsg;
                errorMsgContent.gameObject.SetActive(true);
                Debug.LogError("Sorry, there was an error signing into your account, ERROR: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                currentPlayer = task.Result;
                Debug.Log("Welcome!" + currentPlayer.UserId + ", " + currentPlayer.Email);
                SceneManager.LoadScene(1);
            }
        });
        return currentPlayer;
    }


    public void SignOut()
    {
        if (auth.CurrentUser != null)
        {
            Debug.Log("See you next time!" + auth.CurrentUser.UserId + "," + auth.CurrentUser.Email);
            auth.SignOut();
            signOutTransition = true;
            AudioClipManager.PlaySound("button");
            SceneManager.LoadScene(0);
        }
    }

    public void ForgetPassword()
    {
        string email = emailInput.text.Trim();

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error sending a password reset, ERROR: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                Debug.LogFormat("Forget password email sent successfully!");
            }
        });
    }

    //Client side email validation
    public bool ValidateEmail(string email)
    {
        bool isValid = false;

        //for all emails have @
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        //Check email
        if (email != "" && Regex.IsMatch(email, pattern, options))
        {
            isValid = true;
        }

        return isValid;
    }

    public bool ValidatePassword(string password)
    {
        bool isValid = false;

        if (password != "" && password.Length >= 6)
        {
            isValid = true;
        }

        return isValid;
    }

    public bool ValidateUsername(string username)
    {
        bool isValid = false;

        if (username != "" && username.Length <= 16 && username.Length > 3)
        {
            isValid = true;
        }

        return isValid;
    }

    public string HandleRegisterError(Task<FirebaseUser> task)
    {
        string errorMsg = "";

        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign up failed\n";
            switch (errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    errorMsg += "Email already in use, try another email.";
                    break;
                case AuthError.WeakPassword:
                    errorMsg += "Password too weak, use at least 6 characters.";
                    break;
                case AuthError.MissingPassword:
                    errorMsg += "Password is missing.";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email is invalid, try another email.";
                    break;
                default:
                    errorMsg += "Issue in authentication: " + errorCode;
                    break;
            }
            Debug.Log("Error message: " + errorMsg);

        }

        return errorMsg;
    }

    public string HandleSignInError(Task<FirebaseUser> task)
    {
        string errorMsg = "";
        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign in failed\n";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMsg += "Email is missing.";
                    break;
                case AuthError.MissingPassword:
                    errorMsg += "Password is missing.";
                    break;
                case AuthError.WrongPassword:
                    errorMsg += "Password is wrong.";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email is invalid";
                    break;
                case AuthError.UserNotFound:
                    errorMsg += "User not found.";
                    break;
                default:
                    errorMsg += "Issue in authentication: " + errorCode;
                    break;
            }
            Debug.Log("Error message: " + errorMsg);
        }
        return errorMsg;
    }

}