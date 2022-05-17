using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cTimeManager : MonoBehaviour
{
    public TMPro.TMP_Text time_text;

    public float LimitTime = 300f; // 초 단위 제한시간
    public float RemainTime = 0.0f; // 남은시간

    public bool TimerAvailable = true; // 타이머 작동여부

    private void Start()
    {
        RemainTime = LimitTime; // 남은시간 세팅
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerAvailable)
        {
            TimerSetting();
        }
    }

    void TimerSetting()
    {
        if (RemainTime > Mathf.Epsilon)
        {
            RemainTime -= Time.deltaTime;
        }
        else
        {
            RemainTime = 0.0f;
        }

        int min = (int)(RemainTime / 60);
        int sec = (int)(RemainTime % 60);
        int msec = (int)((RemainTime - (int)RemainTime) * 100);

        string str_min = min.ToString();
        string str_sec = sec.ToString();
        string str_msec = msec.ToString();

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

    public void SaveTime()
    {
        float elapsedTime = LimitTime - RemainTime; // 경과시간

        int min = (int)(elapsedTime / 60);
        int sec = (int)(elapsedTime % 60);

        // 게임데이터에 저장
        GameData.Instance.playerdata.elapsedTime.Minutes = min;
        GameData.Instance.playerdata.elapsedTime.Seconds = sec;

        Debug.Log(min);
        Debug.Log(sec);
    }
}
