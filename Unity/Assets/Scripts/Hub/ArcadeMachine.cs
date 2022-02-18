using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
Author: Kang Xuan

Name of Class: ArcadeMachine

Description of Class: Main script assigned to each Arcade Machine to bring player to the respective game scene

Date Created: 18/02/2022
**/
public class ArcadeMachine : MonoBehaviour
{
    [SerializeField] private MachineType _machineType;

    // Public function that is used to load respective scene when Socket Interactor is interacted with
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
