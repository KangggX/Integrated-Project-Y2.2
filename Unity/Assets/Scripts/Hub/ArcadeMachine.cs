using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine : MonoBehaviour
{
    [SerializeField] private MachineType _machineType;

    public void TriggerGameScene()
    {
        switch (_machineType)
        {
            case MachineType.Skiing:
                Debug.Log("Loading Skii Scene");

                break;

            case MachineType.IndoorShooting:
                Debug.Log("Loading Indoor Shooting Scene");

                break;

            case MachineType.OutdoorShooting:
                Debug.Log("Loading Outdoor Shooting Scene");

                break;
        }
    }
}

public enum MachineType
{
    Skiing,
    IndoorShooting,
    OutdoorShooting
}
