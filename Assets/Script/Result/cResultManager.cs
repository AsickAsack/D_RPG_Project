using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cResultManager : MonoBehaviour
{
    public TMPro.TMP_Text elapsedTime; // ����ð�

    private void Start()
    {
        

        if (!GameData.Instance.playerdata.desertclear)
        { 
            GameData.Instance.playerdata.Gold += 10000;
            GameData.Instance.playerdata.CurEXP += 1000;
            GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[1]);
            GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[3]);
            GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[4]);
        }
        else
        {
            GameData.Instance.playerdata.Gold += 5000;
            GameData.Instance.playerdata.CurEXP += 500;
            GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[3]);
        }

        if (!GameData.Instance.playerdata.desertclear)
            GameData.Instance.playerdata.desertclear = true;

        ShowElapsedTime(); // ����ð��� ������
    }

    void ShowElapsedTime()
    {
        string min = GameData.Instance.playerdata.elapsedTime.Minutes.ToString();
        string sec = GameData.Instance.playerdata.elapsedTime.Seconds.ToString();

        if (GameData.Instance.playerdata.elapsedTime.Seconds < 10)
        {
            sec = "0" + GameData.Instance.playerdata.elapsedTime.Seconds;
        }

        elapsedTime.text = "����ð�  " + min + " : " + sec;
    }

    public void ExitGame()
    {
        
        SceneLoader.Instance.Loading_LoadScene(3);
    }
}
