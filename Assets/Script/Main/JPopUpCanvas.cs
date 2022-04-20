using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JPopUpCanvas : MonoBehaviour
{

    public Camera mainCamera;
    public Canvas BackGround_Canvas;
    public Canvas Inventory_Canvas;
    public Canvas Option_Canvas;
    public Canvas Equip_Canvas;
    public bool IsUIopen = false; // ui�� ���� �����ִ��� �ƴ��� Ȯ���ϴ� �Ұ�

    public Slider UI_Move_Slider;
    public GameObject Right_Button;

    [Header("equip_select_image")]
    public Image[] equip_image; // Ŭ�������� Ȱ��ȭ ��ų ��� �̹��� �迭

    [Header("Option_select_image")]
    public Image[] option_image; // Ŭ�������� Ȱ��ȭ ��ų �ɼ� �̹��� �迭


    //[Header("Option_Button")]
    //public int[] option_Button;

    [SerializeField]
    private AudioClip Ui_Click; // UIŬ�������� ����� ȿ����
    [SerializeField]
    private AudioClip DunGeon_Click; // ���� ���� Ŭ�������� ����� ȿ����
    
    AudioSource audioSource; // ȿ���� ����� �����Ŭ��

    public enum Popup //� �˾����� �˷��� ������ 
    {
        None=0,Iventory_Popup,Equip_Popup,Option_Popup
    }

    Popup popup=Popup.None;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        BackGround_Canvas.enabled = true; 
        mainCamera.enabled = false; 
        Option_Canvas.enabled = true;
        option_image[0].enabled = true; // �ɼ�â�� ������ ���� ù��° ��ư�� Ȱ��ȭ ��Ű�� ����

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
            audioSource.PlayOneShot(Ui_Click);

        }

    }


    public void hilight_option(int index) //�ɼ� ��� �޴��� ��������
    {
        for (int i = 0; i < option_image.Length; i++)
        {
         option_image[i].enabled = (i == index);
            audioSource.PlayOneShot(Ui_Click);
        }
    }

    public void Ui_Move()
    {


        //Right_Button.transform.position = 
        // new Vector3(Right_Button.transform.position.x, Right_Button.transform.position.y, Right_Button.transform.position.z); 
      // UI_Move_Slider.value
    }

}
