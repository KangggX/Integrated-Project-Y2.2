using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
Author: Dan

Name of Class: Activate Targets

Description of Class: Contains script for the target game mechanics

Date Created: 3 / 02 / 2022
*/
public class ActivateTargets : MonoBehaviour
{
    public float SpawnCoolDown = 3.0f;

    //public GameObject[] TargetstoActivate;

    [SerializeField]
    public List<GameObject> TargetstobeActivated;


    [SerializeField]
    //private time to track for the spawn cooldownn
    private float StartTime;


    [SerializeField]
    //bool to check when it can spawn or not
    private bool CanSpawnTarget;

    
    public bool isGameActive;

    private int val;


    //start game function, sets the starttime to the current time in the game and lets the script spawn targets
    public void startGame()
    {
        StartTime = Time.time;
        CanSpawnTarget = true;
        isGameActive = true;
    }

    //stop game function, called when the timer reaches 0, sets all targets to false again
    public void stopOutdoor()
    {
        isGameActive = false;
        Start();
    }

    // spawn target void
    void SpawnTarget()
    {
        //gets the total number of targets existing in the scene
        var totalTargets = TargetstobeActivated.Count();
        if (totalTargets > 2)
        {
            //spawns 2 targets per instance
            for (int i = 0; i < 2; i++) 
                {
                    //activates a random target in the array and removes them from the array afterwards
                    val = Random.Range(0, totalTargets - 1);
                    TargetstobeActivated[val].SetActive(true);
                    TargetstobeActivated.Remove(TargetstobeActivated[val]);
                }
            
            Debug.Log("Target has been activated");
        }
        else
        {
            // when theres not enough targets to spawn
            Debug.Log("No more Targets to spawn");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //finds all targets in the scene
        TargetstobeActivated = new List<GameObject>(GameObject.FindGameObjectsWithTag("outdoorTarget"));
        foreach (GameObject r in TargetstobeActivated)
            {
                r.SetActive(false);
            }
    }

    

    // Update is called once per frame
    void Update()
    {
        //if can spawn target, runs the spawn target function and resets the cooldown
        if(CanSpawnTarget == true)
        {
            SpawnTarget();
            StartTime = Time.time;
            CanSpawnTarget = false;
        }
        else{
            if (isGameActive == true)
            {
                //Can Spawn a target after every 3 seconds
                if (Time.time - StartTime > SpawnCoolDown)
                {
                    CanSpawnTarget = true;
                    Debug.Log("Finished Cooldown");
                }
            }
            else
            {
                return;
            }
        }
    }
}
