using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{

    static AudioSource audioSrc;
    public static AudioClip buttonSound, winSound, cleaningSound;

    // Start is called before the first frame update
    void Start()
    {
        //Load the sounds from the Resources folder
        buttonSound = Resources.Load<AudioClip>("button");
        
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        //Call references for other scripts to use
        switch(clip)
        {
            //if button is called, play button sound
            case "button":
                audioSrc.PlayOneShot(buttonSound);
                break;
        }
    }

    public void PlayButtonSound()
    {
        audioSrc.PlayOneShot(buttonSound);
    }
}
