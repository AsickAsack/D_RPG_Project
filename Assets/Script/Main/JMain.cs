using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class JMain : MonoBehaviour
{


    [Header("[�������Թ�ư �Һ� ȸ�� �̹���]")]
    public UnityEngine.UI.Image Battlebtn_light;


    [Header("[��� ��]")]
    public TMPro.TMP_Text nick;
    public TMPro.TMP_Text Level;
    public TMPro.TMP_Text Gold;
    public TMPro.TMP_Text Emerald;

    [Header("[���� BGM]")]
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

  



    void Change_BlackBar() // ��ܹ� text �ٲٴ� �Լ�
    {
        nick.text = GameData.Instance.playerdata.Nickname;
        Gold.text = GameData.Instance.playerdata.Gold.ToString("N0");
        Emerald.text = GameData.Instance.playerdata.Emerald.ToString("N0");
        Level.text = "Lv. " + GameData.Instance.playerdata.Level.ToString();
    }

    void Rotate_light() // �������� ��ư ������ �� �����ϴ� �Լ�
    {
        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
    }


}
