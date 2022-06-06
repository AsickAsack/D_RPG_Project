using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltime : MonoBehaviour
{
    public Image[] Cooltime;
    public Button[] SkillButton;
    public float[] CoolTimeSec;
    public TMPro.TMP_Text[] CoolTimetx;
    public GameObject joystic;
   

    private void Start()
    {
        CoolTimeSec[0] = 10.0f;
        CoolTimeSec[1] = 2.0f;
        CoolTimeSec[2] = 8.0f;

   
    }


    public void CooltimeSet(int index)
    {
        SkillButton[index].interactable = false;
        Cooltime[index].fillAmount = 1;
        StartCoroutine(CoolTimeProcess(index));
    }

   

    IEnumerator CoolTimeProcess(int index)
    {
        CoolTimetx[index].gameObject.SetActive(true);

        while (Cooltime[index].fillAmount != 0)
        {
           
            Cooltime[index].fillAmount -= (Time.deltaTime/ CoolTimeSec[index]);
            CoolTimetx[index].text = ((int)(Cooltime[index].fillAmount*CoolTimeSec[index]+1)).ToString();
            yield return null;


        }
        CoolTimetx[index].gameObject.SetActive(false);
        SkillButton[index].interactable = true;
    }

}
