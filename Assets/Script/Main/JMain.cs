using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class JMain : MonoBehaviour
{
    [Header("[Ŭ�� �̹��� ���ӿ�����Ʈ]")]
    public GameObject Click;

    [Header("[�������Թ�ư �Һ� ȸ�� �̹���]")]
    public UnityEngine.UI.Image Battlebtn_light;

    
    [Header("[��� ��]")]
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

    void Change_BlackBar() // ��ܹ� text �ٲٴ� �Լ�
    {
        nick.text = GameData.Instance.itemdata.Nickname;
        Gold.text = GameData.Instance.itemdata.Gold.ToString("N0");
        Emerald.text = GameData.Instance.itemdata.Emerald.ToString("N0");
        Level.text = "Lv. " + GameData.Instance.itemdata.Level.ToString();
    }

    void Rotate_light() // �������� ��ư ������ �� �����ϴ� �Լ�
    {
        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
    }


}
