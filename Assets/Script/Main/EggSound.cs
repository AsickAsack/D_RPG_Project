using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSound : MonoBehaviour
{
    public AudioClip StartEgg;
    public AudioClip EndEgg;
    public AudioSource Eggaudio;
    public GameObject[] pet;
    public Animator Effect;
    public TMPro.TMP_Text Pet_name;
    public GameObject summonButton;
    public GameObject[] RealPet;
   
    public void StartEggSound()
    {
        Eggaudio.PlayOneShot(StartEgg);
    }


    public void broken()
    {
        Effect.SetTrigger("Ex");
        Eggaudio.PlayOneShot(EndEgg);
        
    }

    public void Summon()
    {
        summonButton.SetActive(false);
        int a = Random.Range(0, 3);
        

        pet[a].gameObject.SetActive(true);
        GameData.Instance.playerdata.PetList.Add(pet[a].gameObject);
        Pet_name.gameObject.SetActive(true);

        foreach(GameObject Monster in RealPet)
        {
            Monster.SetActive(false);
        }

        switch (a)
        {
            case 0:
                Pet_name.text = "소녀 엔트";
                RealPet[0].SetActive(true);
                break;
            case 1:
                Pet_name.text = "진흙 구울";
                RealPet[1].SetActive(true);
                break;
            case 2:
                Pet_name.text = "해적 미노";
                RealPet[2].SetActive(true);
                break;
        }

        

    }

    public void EndSummon()
    {
        
        this.gameObject.SetActive(false);
    }
}
