using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
