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
                SceneManager.LoadScene("Skiing");

                break;

            case MachineType.IndoorShooting:
                SceneManager.LoadScene("Indoor Shooting");
                PlayerGameState.HasEnteredBefore = true;

                break;

            case MachineType.OutdoorShooting:
                SceneManager.LoadScene("Outdoor Shooting");

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
