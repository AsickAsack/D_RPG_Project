using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JPopUpCanvas : MonoBehaviour
{
    [Header("[UI ĵ������]")]
    public Camera mainCamera;
    public Canvas BackGround_Canvas;
    public Canvas Inventory_Canvas;
    public Canvas Option_Canvas;
    public Canvas Equip_Canvas;

    [Header("[UI open Ȯ�� ����]")]
    public bool IsUIopen = false; // ui�� ���� �����ִ��� �ƴ��� Ȯ���ϴ� �Ұ�

    //public GameObject Right_Button;

    [Header("[��� Ŭ�� �̹��� �迭]")]
    public Image[] equip_image; // Ŭ�������� Ȱ��ȭ ��ų ��� �̹��� �迭

    [Header("[�ɼ� Ŭ�� �̹��� �迭]")]
    public Image[] option_image; // Ŭ�������� Ȱ��ȭ ��ų �ɼ� �̹��� �迭

    [Header("[�ɼ� �ð� �ؽ�Ʈ]")]
    public TMPro.TMP_Text Time_Text;

    [Header("[�ɼ� ������ �̵����]")]
    public Slider[] OptionSlider;
    public TMPro.TMP_Text[] Slider_text;
    public RectTransform[] sliderMoveObjectLeft;
    public RectTransform[] sliderMoveObjectRight;
    public GameObject[] option_UIpanel;

    [Header("[����� �ҽ�,Ŭ��]")]
    public AudioClip Ui_Click; // UIŬ�������� ����� ȿ����
    public AudioClip DunGeon_Click; // ���� ���� Ŭ�������� ����� ȿ����
    AudioSource audioSource; // ȿ���� ����� �����Ŭ��


    public enum Popup //� �˾����� �˷��� ������ 
    {
        None=0,Iventory_Popup,Equip_Popup,Option_Popup
    }

    Popup popup=Popup.None;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        for(int i=0;i<sliderMoveObjectLeft.Length;i++)
        {
            rightIcon_orginpos[i] = sliderMoveObjectRight[i].anchoredPosition;
            leftIcon_orginpos[i] = sliderMoveObjectLeft[i].anchoredPosition;

        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }



    public void ExitPopUp() // �˾�â ���� �Լ�
    {
        switch(popup)
        {
            case Popup.None:
                break;
            case Popup.Iventory_Popup:
                { 
                Inventory_Canvas.enabled = false; 
                }
                break;
            case Popup.Equip_Popup:
                {
                    Equip_Canvas.enabled = false;
                }
                break;
            case Popup.Option_Popup:
                {
                    Option_Canvas.enabled = false;
                    StopCoroutine(TimeText());
                }
                break;
        }

        audioSource.PlayOneShot(Ui_Click);
        IsUIopen = false;
        mainCamera.enabled = true;
        BackGround_Canvas.enabled = false;

    }

    public void Open_Inventory()
    {
        popup = Popup.Iventory_Popup;  // popup enum�� �κ��丮 �˾����� ����
        audioSource.PlayOneShot(Ui_Click); 
        IsUIopen = true;
        BackGround_Canvas.enabled = true; // ȭ���� ���������� ��׶��� ĵ���� ����(����ȭ��)
        mainCamera.enabled = false; // ����ī�޶� ����(ui�� ������ �� �ʿ���� ī�޶� ���༭ �ڿ��� �Ƴ�)
        Inventory_Canvas.enabled = true; // �κ��丮 ĵ���� ����

    }

    public void Open_Option()
    {
        popup = Popup.Option_Popup;
        audioSource.PlayOneShot(Ui_Click);
        IsUIopen = true;
        //BackGround_Canvas.enabled = true; 
        //mainCamera.enabled = false; 
        Option_Canvas.enabled = true;
        option_image[0].enabled = true; // �ɼ�â�� ������ ���� ù��° ��ư�� Ȱ��ȭ ��Ű�� ����
        StartCoroutine(TimeText());
    }

    IEnumerator TimeText()
    {
        while (true)
        {
            Time_Text.text = System.DateTime.Now.ToString("yyyy-MM-dd tt hh:mm:ss");

            yield return null;
        }
    }

    public void Open_Equip_Pppup()
    {
        popup = Popup.Equip_Popup;
        audioSource.PlayOneShot(Ui_Click);
        IsUIopen = true;
        BackGround_Canvas.enabled = true;
        mainCamera.enabled = false;
        Equip_Canvas.enabled = true;
    }




    public void EnterDunGeon() // �������� ������ �������� �Լ�
    {
        audioSource.PlayOneShot(DunGeon_Click);
        StartCoroutine(Delay(1)); // ȿ���� ����� ���� 1�� ������ �ڷ�ƾ (���ϸ� ȿ���� �Ҹ��� �ȳ��� ���̵���)
    }

    IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelecteDungeon");
    }

    
    public void hilight_equiped(int index) //���â ������ ������ �������� Ȱ��ȭ ��Ű�� �Լ� (���� �޴����� ���� ����x)
    {
        for(int i=0; i<equip_image.Length; i++)
        {

            equip_image[i].enabled = (i == index); // ��ư onclick()�� �Լ��� �ְ� ����� index�� ���� �迭 �̹����� ������ ������ �� ����

        }
        audioSource.PlayOneShot(Ui_Click);
    }

    

    public void hilight_option(int index) //�ɼ� ��� �޴��� ��������
    {
        for (int i = 0; i < option_image.Length; i++)
        {

            option_image[i].enabled = (i == index);

            if (index == 0)
            {
                option_UIpanel[0].SetActive(true);
                option_UIpanel[1].SetActive(false);
            }
            else
            {
                option_UIpanel[0].SetActive(false);
                option_UIpanel[1].SetActive(true);
            }
        }
        audioSource.PlayOneShot(Ui_Click);
    }



    Vector2[] leftIcon_orginpos = new Vector2[2];
    Vector2[] rightIcon_orginpos = new Vector2[2];
    
    public void OptionUi_Move()
    {

        float[] x = new float[4];

        x[0] = leftIcon_orginpos[0].x + OptionSlider[0].value * 100;
        x[1] = leftIcon_orginpos[1].x + OptionSlider[0].value * 100;
        x[2] = rightIcon_orginpos[0].x - OptionSlider[1].value * 100;
        x[3] = rightIcon_orginpos[1].x - OptionSlider[1].value * 100;

        sliderMoveObjectLeft[0].anchoredPosition = new Vector2(x[0], sliderMoveObjectLeft[0].anchoredPosition.y);
        sliderMoveObjectLeft[1].anchoredPosition = new Vector2(x[1], sliderMoveObjectLeft[1].anchoredPosition.y);
        sliderMoveObjectRight[0].anchoredPosition = new Vector2(x[2], sliderMoveObjectRight[0].anchoredPosition.y);
        sliderMoveObjectRight[1].anchoredPosition = new Vector2(x[3], sliderMoveObjectRight[1].anchoredPosition.y);

        Slider_text[0].text = Mathf.RoundToInt(OptionSlider[0].value * 100).ToString();
        Slider_text[1].text = Mathf.RoundToInt(OptionSlider[1].value * 100).ToString();

    }


}
