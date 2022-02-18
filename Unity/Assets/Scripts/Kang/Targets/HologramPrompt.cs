using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kirdesh

Name of Class: HologramPrompt

Description of Class: Class to trigger the hologram prompt 

Date Created: 18/02/2022
**/
public class HologramPrompt : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(AwakeTimeout());
    }

    // Coroutine to disable the prompt after 5s
    private IEnumerator AwakeTimeout()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
