using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class JMain : MonoBehaviour
{


    [Header("[�������Թ�ư �Һ� ȸ�� �̹���]")]
    public UnityEngine.UI.Image Battlebtn_light;


    [Header("[��� ��]")]
    public TMPro.TMP_Text nick;
    public TMPro.TMP_Text Level;
    public TMPro.TMP_Text Gold;
    public TMPro.TMP_Text Emerald;
    public ProceduralImage Exp_Round;

    [Header("[���� BGM]")]
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
            Debug.Log("����:"+GameData.Instance.playerdata.EarnMoney);
            Debug.Log("����:"+GameData.Instance.playerdata.SpendMoney);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameData.Instance.playerdata.Gold -= 500000;
            Debug.Log("����:" + GameData.Instance.playerdata.EarnMoney);
            Debug.Log("����:" + GameData.Instance.playerdata.SpendMoney);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameData.Instance.playerdata.CurEXP += 100;
            
        }

        Change_BlackBar();
        Rotate_light();
     
    }

  



    void Change_BlackBar() // ��ܹ� text �ٲٴ� �Լ�
    {
        nick.text = GameData.Instance.playerdata.Nickname;
        Gold.text = GameData.Instance.playerdata.Gold.ToString("N0");
        Emerald.text = GameData.Instance.playerdata.Emerald.ToString("N0");
        Level.text = "Lv. " + GameData.Instance.playerdata.Level.ToString();
        Exp_Round.fillAmount =((float)GameData.Instance.playerdata.CurEXP / (float)GameData.Instance.playerdata.MaxEXP);
    }

    void Rotate_light() // �������� ��ư ������ �� �����ϴ� �Լ�
    {
        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
    }


}




