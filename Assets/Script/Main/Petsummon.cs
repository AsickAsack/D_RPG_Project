using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petsummon : MonoBehaviour
{
    public GameObject Egg;
    public GameObject[] Pet;
    public GameObject petPanel;
    public AudioSource effect_Audio;
    public AudioClip eggstart;
    public AudioClip eggLoop;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPetPanel()
    {
        
        petPanel.gameObject.SetActive(true);
        Egg.gameObject.SetActive(true);
        effect_Audio.PlayOneShot(eggstart);
        effect_Audio.clip = eggLoop;
        effect_Audio.loop = true;
        effect_Audio.Play();
    }

    public void StartPetsummon()
    {
        effect_Audio.clip = null;
        effect_Audio.loop = false;
        
    }

    
}
