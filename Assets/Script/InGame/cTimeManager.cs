using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cTimeManager : MonoBehaviour
{
    public TMPro.TMP_Text time_text;
    public float LimitTime = 30.0f; // 초 단위 제한시간

    // Update is called once per frame
    void Update()
    {
        TimerSetting();
    }

    void TimerSetting()
    {
        if (LimitTime > Mathf.Epsilon)
        {
            LimitTime -= Time.deltaTime;
        }
        else
        {
            LimitTime = 0.0f;
        }

        int min = (int)(LimitTime / 60);
        int sec = (int)(LimitTime % 60);
        int msec = (int)((LimitTime - (int)LimitTime) * 100);

        string str_min = min.ToString(); ;
        string str_sec = sec.ToString(); ;
        string str_msec = msec.ToString(); ;

        if (min < 10)
        {
            str_min = "0" + min; 
        }
        
        if (sec < 10)
        {
            str_sec = "0" + sec;
        }
        
        if (msec < 10)
        {
            str_msec = "0" + msec;
        }

        time_text.text = str_min + ":" + str_sec + ":" + str_msec;
    }
}
