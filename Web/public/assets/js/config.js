// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-analytics.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyDBdJu01Vu1HroqPK7Ya3SVInxTZ6i2ul0",
  authDomain: "y2s2-ip.firebaseapp.com",
  databaseURL: "https://y2s2-ip-default-rtdb.asia-southeast1.firebasedatabase.app",
  projectId: "y2s2-ip",
  storageBucket: "y2s2-ip.appspot.com",
  messagingSenderId: "338320651115",
  appId: "1:338320651115:web:2f669535cf1c25cd67522e",
  measurementId: "G-KZ1NKQXNWN"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);