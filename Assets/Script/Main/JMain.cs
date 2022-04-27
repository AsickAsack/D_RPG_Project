using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JMain : MonoBehaviour
{
    [Header("[Ŭ�� �̹��� ���ӿ�����Ʈ]")]
    public GameObject Click;
    RectTransform click_rect;
    public Camera uicamera;
    Vector2 screenPoint;
    public RectTransform pannel;

    [Header("[�������Թ�ư �Һ� ȸ�� �̹���]")]
    public UnityEngine.UI.Image Battlebtn_light;

    
    [Header("[��� ��]")]
    public TMPro.TMP_Text nick;
    public TMPro.TMP_Text Gold;
    public TMPro.TMP_Text Emerald;
   



    void Start()
    {
        Click.GetComponent<ParticleSystem>().Stop();
        click_rect = Click.GetComponent<RectTransform>();
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
        if (Input.GetMouseButton(0))
        {
             Click.SetActive(true);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(pannel, Input.mousePosition, uicamera, out screenPoint);
            Click.GetComponent<ParticleSystem>().Play();
            click_rect.localPosition = screenPoint;
        }
        //else
        //{
        //    Click.SetActive(false);
        //    Click.GetComponent<ParticleSystem>().Stop();
        //}

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
    }

    void Rotate_light() // �������� ��ư ������ �� �����ϴ� �Լ�
    {
        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
    }


}
