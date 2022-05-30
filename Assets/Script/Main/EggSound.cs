using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSound : MonoBehaviour
{
    public AudioClip StartEgg;
    public AudioClip EndEgg;
    public AudioSource audio;

    public void StartEggSound()
    {
        audio.PlayOneShot(StartEgg);
    }


    public void broken()
    {
        audio.PlayOneShot(EndEgg);
    }
}
