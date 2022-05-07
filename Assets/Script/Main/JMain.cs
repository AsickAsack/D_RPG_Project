using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class JMain : MonoBehaviour
{


    [Header("[던전진입버튼 불빛 회전 이미지]")]
    public UnityEngine.UI.Image Battlebtn_light;


    [Header("[상단 바]")]
    public TMPro.TMP_Text nick;
    public TMPro.TMP_Text Level;
    public TMPro.TMP_Text Gold;
    public TMPro.TMP_Text Emerald;

    [Header("[메인 BGM]")]
    public AudioClip mainBGM;

    private void Awake()
    {
       GameData.Instance._save();
        

    }



    void Start()
    {
        Sound.Instance.PlayBGM(mainBGM);
       
        for(int i=0;i< GameData.Instance.playerdata.Itemdata2.Length;i++)
        GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[i]);
        

    }

    public void gotitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }




    // Update is called once per frame
    void Update()
    {

        Change_BlackBar();
        Rotate_light();
     
    }

  



    void Change_BlackBar() // 상단바 text 바꾸는 함수
    {
        nick.text = GameData.Instance.playerdata.Nickname;
        Gold.text = GameData.Instance.playerdata.Gold.ToString("N0");
        Emerald.text = GameData.Instance.playerdata.Emerald.ToString("N0");
        Level.text = "Lv. " + GameData.Instance.playerdata.Level.ToString();
    }

    void Rotate_light() // 던전입장 버튼 주위에 빛 돌게하는 함수
    {
        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
    }


}
