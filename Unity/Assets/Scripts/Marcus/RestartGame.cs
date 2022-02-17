using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
Author: Marcus

Name of Class: RestartGame

Description of Class: To restart the level by loading the scene again

Date Created: 17/08/2022
**/
public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Skiing");
    }
}
