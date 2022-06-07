using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cResultManager : MonoBehaviour
{
    public TMPro.TMP_Text elapsedTime; // 경과시간

    private void Start()
    {

        GameData.Instance.playerdata.Gold += 500;
        GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[1]);
        GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[3]);
        GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[4]);

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

    public void ExitGame()
    {
        
        SceneLoader.Instance.Loading_LoadScene(3);
    }
}
