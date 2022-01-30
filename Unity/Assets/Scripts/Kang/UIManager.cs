using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private GameObject _magazineEmptyPrompt;

    public static void DisplayMagazineError()
    {
        Debug.Log("hi");
    }
}
