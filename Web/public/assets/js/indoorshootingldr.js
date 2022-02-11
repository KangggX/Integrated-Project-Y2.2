// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries
// Import the functions you need from the SDKs you need
import { 
    getAuth, 
    initializeAuth, 
    createUserWithEmailAndPassword, 
    signInWithEmailAndPassword,
    onAuthStateChanged
} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-auth.js";

import { 
    getDatabase, 
    ref, 
    child, 
    push,
    update,
    get, 
    set, 
    onValue, 
    orderByChild, 
    orderByKey, 
    query,
    remove,
    limitToLast

} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-database.js";

const db = getDatabase();
const playerRef = ref(db, "players");
const playerStatsRef = ref(db, "playerStats");
const leaderboardRef = ref(db, "indoorLeaderboard");

//Working with Auth
const auth = getAuth();
const user = auth.currentUser;

var leaderboardData2 = {
    displayname: "",
    indoorPoints: 0,
    updatedOn: Date.now()
}

// Player key, assigned by getKey() function
var key;
console.log(user);

// Test array (pls la don't delete)
var tempLeaderboardArray = [];

onAuthStateChanged(auth, (user) => {
    if (user) {
        const uid = user.uid;
        $("#usernameDetail").text(`${user.displayName}`);
        $("#emailDetail").text(`${user.email}`);
        
        updateProfilePage(user.displayName); // Update the profile page
        updateLeaderboard(); // Update the leaderboard page for newly created user
    } else {
        // User is signed out
        updateLeaderboard(); // Update the leaderboard page for newly created user
    }
});

leaderboardUpdater();

// Updates leaderboard every 10 seconds
// Credits: https://stackoverflow.com/questions/7188145/call-a-javascript-function-every-5-seconds-continuously
function leaderboardUpdater() {
    updateLeaderboard();

    setInterval(function() {
        updateLeaderboard();
    }, 10000); // Every 1000 is 1 second
}

function setLeaderboardData() {
    let que = query(playerStatsRef, orderByChild("indoorPoints"), limitToLast(1));
    
    get(que).then((snapshot) => {
        if (snapshot.exists()) {
            // console.log(snapshot.size)
            //remove(leaderboardRef); // Delete current leaderboard data in Database

            let index = 1;
            let position;

            tempLeaderboardArray = [];

            snapshot.forEach((childSnapshot) => {
                if (index == 1) {
                    position = "1st";
                }
                else if (index == 2) {
                    position = "2nd";
                }
                else if (index == 3) {
                    position = "3rd"
                }
                else {
                    position = index + "th"
                }

                update(ref(db), {["/playerStats/" + childSnapshot.key + "/indoorPos"]: position});

                leaderboardData2.displayname = childSnapshot.child("displayname").val();
                leaderboardData2.indoorPoints = childSnapshot.child("indoorPoints").val();
                console.log(leaderboardData2);

                
                set(ref(db, "indoorLeaderboard/" + childSnapshot.key), leaderboardData2); // Pushing new data into Database
                
                index++;
                
                // tempLeaderboardArray.push({key: childSnapshot.key, username: childSnapshot.child("displayname").val(), indoorPoints: childSnapshot.child("indoorPoints").val()});
            });
        }
    });
}

function updateLeaderboard(){
    setLeaderboardData();
    
    setTimeout(() => {
        // Clear leaderboard content first
        $(".leaderboard__list").empty();

        // Appending header
        $(".leaderboard__list").append(`
            <li>
                <div class="leaderboard__list--content leaderboard__title--ranking">Ranking</div>
                <div class="leaderboard__list--content leaderboard__title--score">Points</div>
                <div class="leaderboard__list--content leaderboard__title--username">Username</div>
            </li>
        `);
        let que = query(leaderboardRef, orderByChild("indoorPoints"));

        // Appending leaderboard data
        get(que).then((snapshot) => {
            if (snapshot.exists()) {

                let index = 1;
                var displaynameArray = [];
                var indoorPointsArray = [];

                snapshot.forEach((childSnapshot) => {
                    displaynameArray.push(childSnapshot.child("displayname").val());                   
                    indoorPointsArray.push(childSnapshot.child("indoorPoints").val());
                })

                displaynameArray.reverse();
                indoorPointsArray.reverse();
                console.log(displaynameArray);
                console.log(indoorPointsArray);
                    
                    for (const i in displaynameArray)
                    {
                            $(".leaderboard__list").append(`<li>
                            <div class="leaderboard__list--content leaderboard--ranking">${index}</div>
                            <div class="leaderboard__list--content leaderboard--score">${indoorPointsArray[i]}</div>
                            <div class="leaderboard__list--content leaderboard--username">${displaynameArray[i]}</div>
                            </li>`)

                            console.log(index);
                        
                            index++;
                        
                    }
                    
            }
        });
    }, 100);
}

// Updates the profile.html page whenver 
function updateProfilePage(username) {
    getKey(username);
    
    setTimeout(() => {
        get(ref(db, "playerStats/" + key)).then((snapshot) => {
            if (snapshot.exists()) {
                $("#indoorPointsDetail").text(`${snapshot.child("indoorPoints").val()}`);
            }
            else {
                
            }
        });
    }, 500);
}

function getKey(username) {
    get(playerRef).then((snapshot) => {
        if (snapshot.exists()) {
            try {
                snapshot.forEach((childSnapshot) => {
                    if (username == childSnapshot.child("displayname").val()) {
                        key = childSnapshot.key;
                        console.log(key);
                        
                        return;
                    }
                });
            }
            catch(error) {
                console.log("Error getKey" + error);
            }
        }
    });
}

// Converts seconds to HH:MM:SS
// Modified to convert to MM:SS instead and a check to see if timing is N/A
// Courtesy of: https://www.codegrepper.com/code-examples/javascript/convert+seconds+to+hours+minutes+seconds+javascript
function convertHMS(value) {
    const sec = parseInt(value, 10); // convert value to number if it's string
    
    let result;
    let hours   = Math.floor(sec / 3600); // get hours
    let minutes = Math.floor((sec - (hours * 3600)) / 60); // get minutes
    let seconds = sec - (hours * 3600) - (minutes * 60); //  get seconds
    // add 0 if value < 10; Example: 2 => 02
    if (hours   < 10) {hours   = "0" + hours;}
    if (minutes < 10) {minutes = "0" + minutes;}
    if (seconds < 10) {seconds = "0" + seconds;}
    // return hours+':'+minutes+':'+seconds; // Return is HH : MM : SS

    result = minutes+':'+seconds;

    // Check if initial value input is N/A
    // if it is indeed N/A, make sure to return "N/A" instead of "NaN:NaN"
    if (result == "NaN:NaN") {
        return "N/A"
    }
    else if (hours == 0 && minutes == 0) {
        return seconds + "s";
    }
    else if (hours == 0) {
        return minutes + "m" + " " + seconds + "s";
    }
    else {
        // return minutes+':'+seconds; // Return is MM : SS
        return hours + "h" + " " + minutes + "m" + " " + seconds + "s";
    }
}