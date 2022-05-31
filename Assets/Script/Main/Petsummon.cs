using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Petsummon : MonoBehaviour
{
    public GameObject Egg;
    public GameObject petPanel;
    public AudioSource effect_Audio;
    public AudioClip eggstart;
    public AudioClip eggLoop;
    public TMPro.TMP_Text Popup_Text;
    public Button yesButton;
    public GameObject[] CheckPopup;
    public Canvas Black_Bar;

    public void Popup()
    {
        if(GameData.Instance.playerdata.Emerald < 10000)
        {
            Popup_Text.text = "에메랄드가 부족합니다.";
        }
        else
        {
            yesButton.interactable = true;
            Popup_Text.text = "1만 에메랄드가 소모됩니다.\n 진행 하시겠습니까?";
        }
    }


   
    public void OpenPetPanel()
    {
        Black_Bar.enabled = false;
        CheckPopup[0].SetActive(false);
        CheckPopup[1].SetActive(false);
        yesButton.interactable = false;
        GameData.Instance.playerdata.Emerald -= 10000;
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
