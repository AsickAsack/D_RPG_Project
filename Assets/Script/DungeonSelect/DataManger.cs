using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManger : MonoBehaviour
{
    public TMPro.TMP_Text nick;
    public TMPro.TMP_Text Level;
    public TMPro.TMP_Text Gold;
    public TMPro.TMP_Text Emerald;
    public TMPro.TMP_Text Key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Change_BlackBar();
    }
    void Change_BlackBar() // 상단바 text 바꾸는 함수
    {
        nick.text = GameData.Instance.playerdata.Nickname;
        Gold.text = GameData.Instance.playerdata.Gold.ToString("N0");
        Emerald.text = GameData.Instance.playerdata.Emerald.ToString("N0");
        Level.text = "Lv. " + GameData.Instance.playerdata.Level.ToString();
        Key.text = GameData.Instance.playerdata.Key.ToString();
    }

    public void KeyPlustBtnClick()
    {
        GameData.Instance.playerdata.Key += 1;
    }

    public void StartBtnClick()
    {
        if (GameData.Instance.playerdata.Key <= 0 && GameData.Instance.playerdata.Key == 1) return;
        if(GameData.Instance.playerdata.Key >=2)
        {
            GameData.Instance.playerdata.Key -= 2;
        }
        
    }

    public void Gomn()
    {
        SceneLoader.Instance.Loading_LoadScene(2);
    }
}
