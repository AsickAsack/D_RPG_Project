using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlramCheck : MonoBehaviour
{
    public GameObject[] RedDot;
    public GameObject AlarmPanel;
    public TMPro.TMP_Text Alarm_Text;
    bool check = false;


    void Update()
    {
        if(Mission.CurClearMission > 0 && !check)
        {
            RedDot[0].SetActive(true);
            RedDot[1].SetActive(true);
            AlarmPanel.SetActive(true);
            
            check = true;
        }

        if(Mission.CurClearMission > 0 )
        {
            Alarm_Text.text = "�Ϸᰡ���� �ӹ��� " + Mission.CurClearMission + "�� �ֽ��ϴ�.";
        }
        if(Mission.CurClearMission == 0 && check)
        {
            RedDot[0].SetActive(false);
            RedDot[1].SetActive(false);
            AlarmPanel.SetActive(false);
            check = false;
        }
    }

}
