using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class JMain : MonoBehaviour
{


    [Header("[던전진입버튼 불빛 회전 이미지]")]
    public UnityEngine.UI.Image Battlebtn_light;


    [Header("[상단 바]")]
    public TMPro.TMP_Text nick;
    public TMPro.TMP_Text Level;
    public TMPro.TMP_Text Gold;
    public TMPro.TMP_Text Emerald;
    public ProceduralImage Exp_Round;

    [Header("[메인 BGM]")]
    public AudioClip mainBGM;

    private void Awake()
    {
       GameData.Instance._save();
        GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[0]);

    }

    void Start()
    {
        
        

    }

    public void gotitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameData.Instance.playerdata.Gold += 500000;
            GameData.Instance.playerdata.Emerald += 500000;
            Debug.Log("번돈:"+GameData.Instance.playerdata.EarnMoney);
            Debug.Log("쓴돈:"+GameData.Instance.playerdata.SpendMoney);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameData.Instance.playerdata.Gold -= 500000;
            Debug.Log("번돈:" + GameData.Instance.playerdata.EarnMoney);
            Debug.Log("쓴돈:" + GameData.Instance.playerdata.SpendMoney);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameData.Instance.playerdata.CurEXP += 100;
            
        }

        Change_BlackBar();
        Rotate_light();
     
    }

  



    void Change_BlackBar() // 상단바 text 바꾸는 함수
    {
        nick.text = GameData.Instance.playerdata.Nickname;
        Gold.text = GameData.Instance.playerdata.Gold.ToString("N0");
        Emerald.text = GameData.Instance.playerdata.Emerald.ToString("N0");
        Level.text = "Lv. " + GameData.Instance.playerdata.Level.ToString();
        Exp_Round.fillAmount =((float)GameData.Instance.playerdata.CurEXP / (float)GameData.Instance.playerdata.MaxEXP);
    }

    void Rotate_light() // 던전입장 버튼 주위에 빛 돌게하는 함수
    {
        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
    }


}




