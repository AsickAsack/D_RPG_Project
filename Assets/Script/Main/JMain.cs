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
  
   
    void Start()
    {
        //Click.GetComponent<ParticleSystem>().Stop();
        //click_rect = Click.GetComponent<RectTransform>();

       
    }

    // Update is called once per frame
    void Update()
    {

        Change_BlackBar();
        Rotate_light();
        _Click();

        

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
        nick.text = GameData.Instance.itemdata.Nickname;
        Gold.text = GameData.Instance.itemdata.Gold.ToString("N0");
        Emerald.text = GameData.Instance.itemdata.Emerald.ToString("N0");
        Level.text = "Lv. " + GameData.Instance.itemdata.Level.ToString();
    }

    void Rotate_light() // 던전입장 버튼 주위에 빛 돌게하는 함수
    {
        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
    }


}
