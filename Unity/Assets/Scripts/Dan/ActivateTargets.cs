using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public void startGame()
    {
        StartTime = Time.time;
        CanSpawnTarget = true;
        isGameActive = true;
    }

    public void stopOutdoor()
    {
        isGameActive = false;
        Start();
    }

    void SpawnTarget()
    {
        var totalTargets = TargetstobeActivated.Count();
        if (totalTargets > 2)
        {
            for (int i = 0; i < 2; i++) 
                {
                    val = Random.Range(0, totalTargets - 1);
                    TargetstobeActivated[val].SetActive(true);
                    TargetstobeActivated.Remove(TargetstobeActivated[val]);
                }
            
            Debug.Log("Target has been activated");
        }
        else
        {
            Debug.Log("No more Targets to spawn");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
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
