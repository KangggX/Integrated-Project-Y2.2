using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kang Xuan

Name of Class: CoinDispenser

Description of Class: Script to dispense coin for players to use to play each game

Date Created: 18/02/2022
**/
public class CoinDispenser : MonoBehaviour
{
    [SerializeField] private MachineType _machineType;

    [SerializeField] private Transform _hole;
    [SerializeField] private Transform _coinParent;
    [SerializeField] private GameObject _coin;

    private List<GameObject> _coinChild = new List<GameObject>();
    private int _coinCount;

    private void Awake()
    {
        _coinCount = _coinParent.childCount;

        for (int i = 0; i < _coinCount; i++)
        {
            _coinChild.Add(_coinParent.GetChild(i).gameObject);
        }
    }

    // Dispense coin if dispenser can be used
    public void DispenseCoin()
    {
        if (CheckAvailability() && StillHaveCoin())
        {
            _coinChild[_coinCount - 1].SetActive(false);
            _coinCount--;

            GameObject coinInstance = Instantiate(_coin, _hole.position, Quaternion.identity);
            coinInstance.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
        }
    }

    // Check whether this dispenser can be used or not
    private bool CheckAvailability()
    {
        // Determine which machine this belongs to
        switch (_machineType)
        {
            case MachineType.Skiing:
                if (PlayerGameState.CanPlaySkiing)
                {
                    return true;
                }

                break;

            case MachineType.IndoorShooting:
                if (PlayerGameState.CanPlayIndoorShooting)
                {
                    return true;
                }

                break;

            case MachineType.OutdoorShooting:
                if (PlayerGameState.CanPlayOutdoorShooting)
                {
                    return true;
                }

                break;
        }

        return false;
    }

    // Check if the machine still have any coin. Returns a boolean
    private bool StillHaveCoin()
    {
        foreach (GameObject go in _coinChild)
        {
            if (go.gameObject.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }
}
