/* 
    Firebase Auth Imports
*/
import { 
    getAuth, 
    initializeAuth, 
    createUserWithEmailAndPassword, 
    signInWithEmailAndPassword, 
    onAuthStateChanged, signOut, 
    setPersistence, 
    browserLocalPersistence,
    updateProfile,
    updatePassword,
    sendPasswordResetEmail
} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-auth.js";

/* 
    Firebase Database Imports
*/
import { 
    getDatabase, 
    ref,
    child, 
    push, 
    get, 
    set, 
    onValue, 
    orderByChild, 
    orderByKey, 
    query 
} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-database.js";

const db = getDatabase();
const playerRef = ref(db, "players");
const playerStatsRef = ref(db, "playerStats");

const auth = getAuth();
const user = auth.currentUser;

var key;

var signInEmail = $("#in__email");
var signInPassword = $("#in__password");

var signUpEmail = $("#up__email");
var signUpName = $("#up__username");
var signUpPassword1 = $("#up__password--1");
var signUpPassword2 = $("#up__password--2");

var recoveryEmail = $("#recovery__email");

var changePassword1 = $("#recovery__password--1");
var changePassword2 = $("#recovery__password--2");

var isUsernameSimilar = false;

// For player parent
var playerData = {
    email: "",
    displayname: "",
    active: true,
    createdOn: Date.now(),
    updatedOn: Date.now(),
    lastLoggedIn: Date.now()
}

// For playerStats parent
var playerStatsData = {
    displayname: "",
    leaderboardPosition: "N/A",
    fastestTime: "N/A",
    totalTime: 0,
    totalGame: 0,
    updatedOn: Date.now()
}

// Check if user is signed in or not
onAuthStateChanged(auth, (user) => {
    if (user) {
        // User is signed in.
        $("#in").css("display", "none");
        $("#out").css("display", "block");
        $("#userbar").css("display", "inline-block");
        $("#username").html(`
        ${user.displayName}
        <svg width="8" height="5" viewBox="0 0 8 5" class="arrow-down" xmlns="http://www.w3.org/2000/svg"><path d="M0.707109 1.70711L3.29289 4.29289C3.68342 4.68342 4.31658 4.68342 4.70711 4.29289L7.29289 1.70711C7.92286 1.07714 7.47669 0 6.58579 0H1.41421C0.523309 0 0.0771438 1.07714 0.707109 1.70711Z"></path></svg>
        `);
    } else {
        // No user is signed in.
        $("#in").css("display", "inline-block");
        $("#out").css("display", "none");
        $("#userbar").css("display", "none");

        console.log("Not signed in");
    }
});

// If form input value is undefined, form label will still be visible
function formCheck(e) {
    if (e.value != "") {
        $(e).addClass("field__input--non-empty");
    } else {
        $(e).removeClass("field__input--non-empty");
    }
}

// Check if sign in form inputs are valid or not, enabling the submit button
function signInFormCheck() {
    if (signInEmail.val() == "" || signInPassword.val() == "") {
        $("#in__submit").prop("disabled", true);
    }
    else if ((signInPassword.val().length > "0" && signInPassword.val().length < "6")) {
        $("#in__submit").prop("disabled", true);
        $(".field__error--first").css("display", "block");
    }
    else {
        $("#in__submit").prop("disabled", false);
        $(".field__error--first").css("display", "none");
    }
}

// Check if sign up form inputs are valid or not, enabling the submit button
function signUpFormCheck() {
    if (signUpEmail.val() == "" || signUpName.val() == "" || signUpPassword1.val() == "" || signUpPassword2.val() == "") {
        $("#up__submit").prop("disabled", true);
    }
    else if ((signUpPassword1.val() != signUpPassword2.val()) && (signUpPassword1.val().length >= "6") && (signUpPassword2.val().length > "0")) {
        $("#up__submit").prop("disabled", true);
        $(".field__error--second").css("display", "block");
    }
    else if ((signUpPassword1.val().length > "0" && signUpPassword1.val().length < "6")) {
        $("#up__submit").prop("disabled", true);
        $(".field__error--first").css("display", "block");
    }
    else {
        $("#up__submit").prop("disabled", false);
        $(".field__error--first").css("display", "none");
        $(".field__error--second").css("display", "none");
    }
}

function recoveryFormCheck() {
    if (recoveryEmail.val() == "") {
        $("#recovery__submit").prop("disabled", true);
    }
    else {
        $("#recovery__submit").prop("disabled", false);
    }
}

function changeFormCheck() {
    if (changePassword1.val() == "" || changePassword2.val() == "" ) {
        $("#change__submit").prop("disabled", true);
    }
    else if (changePassword1.val().length > "0" && changePassword1.val().length < "6") {
        $("#change__submit").prop("disabled", true);
        $(".field__error--first").css("display", "block");
    }
    else if ((changePassword1.val() != changePassword2.val()) && (changePassword1.val().length >= "6") && (changePassword2.val().length > "0")) {
        $("#change__submit").prop("disabled", true);
        $(".field__error--second").css("display", "block");
    }
    else {
        $("#change__submit").prop("disabled", false);
        $(".field__error--first").css("display", "none");
        $(".field__error--second").css("display", "none");
    }
}

// Function for user to login
function userLogin() {
    setPersistence(auth, browserLocalPersistence)
    .then(() => {
        return signInWithEmailAndPassword(auth, signInEmail.val(), signInPassword.val())
        .then((userCredential) => {
            // Signed in
            var user = userCredential.user;
            location.href = "index.html";
        })
        .catch((error) => {
            var errorCode = error.code;
            var errorMessage = error.message;
            console.log(errorCode);

            $(".form__error span").text("The email or password you have entered is incorrect, or the account does not exist");
            $(".form__error").css("display", "block");
        });;
    })
    .catch((error) => {
        // Handle Errors here.
        const errorCode = error.code;
        const errorMessage = error.message;
    });
}

// Function for user to create a new account
function userRegister(username) {
    compareUsername(username);
    
    setTimeout(() => {
        console.log(isUsernameSimilar);
        if (isUsernameSimilar) {
            $(".form__error span").text("The account with the email or username already exist");
            $(".form__error").css("display", "block");
        }
        else {
            createUserWithEmailAndPassword(auth, signUpEmail.val(), signUpPassword2.val())
            .then((userCredential) => {
                createUserDatabase(signUpEmail.val(), username);
        
                //signedin
                const user = userCredential.user;
                console.log("created user ... " + userCredential);
                console.log("User is now signed in ");
        
                // Set the user's displayName in Authenticator based on the username parameter
                updateProfile(user, {
                    displayName: username
                }).then(() => {
                    console.log("Display name set successfully!");
                    location.href = "index.html";
                }).catch((error) => {
                    const errorCode = error.code;
                    const errorMessage = error.message;
                    console.log(`ErrorCode: ${errorCode} -> Message: ${errorMessage}`);
                })
            }).catch((error) => {
                const errorCode = error.code;
                const errorMessage = error.message;
                console.log(`ErrorCode: ${errorCode} -> Message: ${errorMessage}`);
                $(".form__error span").text("An account with the email or username already exist");
                $(".form__error").css("display", "block");
            });
        }
    }, 500);
}

// Function for user to logout
function userLogout() {
    signOut(auth).then(() => {
        // Sign-out successful.
        location.href = "index.html";
    }).catch((error) => {
        // An error happened.
    });
}

// Function for user password recovery email
function userRecovery(email) {
    sendPasswordResetEmail(auth, email)
    .then(() => {
        console.log("Password Reset Email Sent Successfully!");
        $("#recovery-form").css("display", "none");
        $("#recovery-success").css("display", "flex");
    })
    .catch((error) => {
        const errorCode = error.code;
        const errorMessage = error.message;
        console.log("User Recovery" + errorMessage);
    })
}

// Change user's password
function changePassword(password) {
    updatePassword(user, password)
    .then(() => {
        console.log("Password Changed!");
    }).catch((error) => {
        console.log("changePassword() error: " + error);
    });
}

// Create a new set of data in the database for newly registered users
function createUserDatabase(email, username) {
    const key = push(playerRef).key;

    playerData.email = email;
    playerData.displayname = username;

    playerStatsData.displayname = username;

    set(ref(db, "players/" + key), playerData);
    set(ref(db, "playerStats/" + key), playerStatsData);
}

// Check if a username is already present in database, show error message
function compareUsername(username) {
    isUsernameSimilar = false;

    get(playerRef).then((snapshot) => {
        if (snapshot.exists()) {
            try {
                snapshot.forEach((childSnapshot) => {
                    if (!isUsernameSimilar) {
                        if (username == childSnapshot.child("displayname").val()) {
                            console.log("same");
    
                            isUsernameSimilar = true;
                            return;
                        }
                        else {
                            isUsernameSimilar = false;
                        }
                    }
                });
            }
            catch(error) {
                console.log("Error compareUsername" + error);
            }
        }
    });
}

// On blur with these IDs, run the formCheck() function
signInEmail
.add(signInPassword)
.add(signUpEmail)
.add(signUpName)
.add(signUpPassword1)
.add(signUpPassword2)
.add(recoveryEmail)
.add(changePassword1)
.add(changePassword2).on("blur", function () {
    formCheck(this);
});

// On blur with these IDs, run the signInFormCheck() function
signInEmail
.add(signInPassword).on("blur", function() {
    signInFormCheck();
});

// On blur with these IDs, run the signUpFormCheck() function
signUpEmail
.add(signUpName)
.add(signUpPassword1)
.add(signUpPassword2).on("blur", function() {
    signUpFormCheck();
});

// On blur with these IDs, run the recoveryFormCheck() function
recoveryEmail.on("blur", function () {
    recoveryFormCheck();
});

changePassword1
.add(changePassword2).on("blur", function() {
    changeFormCheck();
});

$("#in__submit").click(function (e) {
    e.preventDefault();
    userLogin();
});

$("#up__submit").click(function (e) {
    e.preventDefault();
    userRegister(signUpName.val());
});

$("#recovery__submit").click(function (e) {
    e.preventDefault();
    userRecovery(recoveryEmail.val());
});

$("#change__submit").click(function (e) {
    e.preventDefault();
    changePassword(changePassword2.val());
});

$("#out").on("click", function (e) {
    userLogout();
});