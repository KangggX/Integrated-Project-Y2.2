// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-analytics.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyDJavYfxSr8P_SxgFjMr87xP5OjtyQSl2U",
  authDomain: "itddda-asg2.firebaseapp.com",
  databaseURL: "https://itddda-asg2-default-rtdb.asia-southeast1.firebasedatabase.app/",
  projectId: "itddda-asg2",
  storageBucket: "itddda-asg2.appspot.com",
  messagingSenderId: "621309398240",
  appId: "1:621309398240:web:4a1bd0e6e075ee3309d485",
  measurementId: "G-KGZN4MT99W"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);