using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    public GameObject TargetToSpawn;

    //Time it takes between spawning targets
    public float SpawnCoolDown = 3.0f;

    public float minRangeX = -10.0f;
    public float maxRangeX = 10.0f;
    public float minRangeZ = -10.0f;
    public float maxRangeZ = 10.0f;


    [SerializeField]
    //private time to track for the spawn cooldownn
    private float StartTime;


    [SerializeField]
    //bool to check when it can spawn or not
    private bool CanSpawnTarget;

    
    public bool isGameActive;


    // Start is called before the first frame update
    void Start()
    {
        //StartTime = Time.time;
        //CanSpawnTarget = true;
    }
    public void startGame()
    {
        StartTime = Time.time;
        CanSpawnTarget = true;
        isGameActive = true;
    }
    public void stopOutdoor()
    {
        isGameActive = false;
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

    void SpawnTarget()
    {
        var position = new Vector3(Random.Range(minRangeX, maxRangeX), 0, Random.Range(minRangeZ, maxRangeZ));
        Instantiate(TargetToSpawn, position, Quaternion.identity);

        Debug.Log("Object is spawned");
    }
}
