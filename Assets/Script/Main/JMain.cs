using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class JMain : MonoBehaviour
{
    [Header("[클릭 이미지 게임오브젝트]")]
    public GameObject Click;

    [Header("[던전진입버튼 불빛 회전 이미지]")]
    public UnityEngine.UI.Image Battlebtn_light;


    [Header("[상단 바]")]
    public TMPro.TMP_Text nick;
    public TMPro.TMP_Text Level;
    public TMPro.TMP_Text Gold;
    public TMPro.TMP_Text Emerald;


    private void Awake()
    {
        

    }



    void Start()
    {
        

    }





    // Update is called once per frame
    void Update()
    {

        Change_BlackBar();
        Rotate_light();
        _Click();


        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[0]);
           foreach(itemdata a in GameData.Instance.playerdata.Player_inventory)
            {
                Debug.Log(a.ItemName);
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[1]);
            foreach (itemdata a in GameData.Instance.playerdata.Player_inventory)
            {
                Debug.Log(a.ItemName);
            }
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[2]);
            for (int i = 0; i < GameData.Instance.playerdata.Player_inventory.Count; i++)
            {
                Debug.Log(GameData.Instance.playerdata.Player_inventory[i].ItemName);
            }
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[3]);
            for (int i = 0; i < GameData.Instance.playerdata.Player_inventory.Count; i++)
            {
                Debug.Log(GameData.Instance.playerdata.Player_inventory[i].ItemName);
            }
        }



    }

    void _Click()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Click.transform.position = Input.mousePosition;
            Click.SetActive(true);
            StartCoroutine(Click_Delay(0.25f));

        }
    }   

    IEnumerator Click_Delay(float t)
    {
        yield return new WaitForSeconds(t);
            Click.SetActive(false);
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
