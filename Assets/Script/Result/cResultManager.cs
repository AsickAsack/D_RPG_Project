using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cResultManager : MonoBehaviour
{
    public TMPro.TMP_Text elapsedTime; // 경과시간

    private void Start()
    {
        ShowElapsedTime(); // 경과시간을 보여줌
    }

    void ShowElapsedTime()
    {
        string min = GameData.Instance.playerdata.elapsedTime.Minutes.ToString();
        string sec = GameData.Instance.playerdata.elapsedTime.Seconds.ToString();

        if (GameData.Instance.playerdata.elapsedTime.Seconds < 10)
        {
            sec = "0" + GameData.Instance.playerdata.elapsedTime.Seconds;
        }

        elapsedTime.text = "경과시간  " + min + " : " + sec;
    }
}
