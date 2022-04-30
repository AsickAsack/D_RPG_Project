using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;


public class JPopUpCanvas : MonoBehaviour
{

    [Header("[UI ĵ������]")]
    public Camera mainCamera;
    public Canvas BackGround_Canvas;
    public Canvas Equip_Canvas;
    public Canvas Option_Canvas;
    //public Canvas Equip_Canvas;

    [Header("[UI open Ȯ�� ����]")]
    public bool IsUIopen = false; // ui�� ���� �����ִ��� �ƴ��� Ȯ���ϴ� �Ұ�


    //[Header("[���â ��� Ŭ�� �̹��� �迭]")]
    //public Image[] equip_image; // Ŭ�������� Ȱ��ȭ ��ų ��� �̹��� �迭

    [Header("[���â ���� �޴� �̹��� �迭]")]
    public GameObject[] Equip_Menu; // Ŭ�������� Ȱ��ȭ ��ų �ɼ� �̹��� �迭

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

    [Header("[���â]")]
    public TMPro.TMP_Text[] EquipOption_Text;



    [Header("[���â ����]")]
    public GameObject Home;
    public GameObject Character_Icon;
    public GameObject Message_Icon;
    public GameObject Equip_Backbutton;

    [Header("[����� �ҽ�,Ŭ��]")]
    public AudioClip Ui_Click; // UIŬ�������� ����� ȿ����
    public AudioClip DunGeon_Click; // ���� ���� Ŭ�������� ����� ȿ����
    AudioSource audioSource; // ȿ���� ����� �����Ŭ��

    #region �ɼ�â enum
    public enum Popup //� �˾����� �˷��� ������ 
    {
        None=0,Iventory_Popup,Equip_Popup,Option_Popup
    }

    Popup popup=Popup.None;
    #endregion


    #region �����ũ �Լ�
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        for(int i=0;i<sliderMoveObjectLeft.Length;i++)
        {
            rightIcon_orginpos[i] = sliderMoveObjectRight[i].anchoredPosition;
            leftIcon_orginpos[i] = sliderMoveObjectLeft[i].anchoredPosition;

        }
       
    }
    #endregion

    #region �˾�â ���� �Լ�
    public void ExitPopUp()
    {
        switch(popup)
        {
            case Popup.None:
                break;
            case Popup.Iventory_Popup:
                {
                
                }
                break;
            case Popup.Equip_Popup:
                {
                    Equip_Canvas.enabled = false;
                    Equip_Backbutton.SetActive(false);
                    //Equip_Menu[0].SetActive(true);
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
       // mainCamera.enabled = true;
        BackGround_Canvas.enabled = false;
        Home.SetActive(false);
        Message_Icon.SetActive(true);
        Character_Icon.SetActive(true);
    }
    #endregion

    #region ���â �Լ���
    [SerializeField]
    EquipType equipType;
    
    
    enum EquipType
    {
        Total = 0,Weapon,Armor
    }


    public void Open_Equip_Popup()
    {
        popup = Popup.Equip_Popup; 
        audioSource.PlayOneShot(Ui_Click); 
        IsUIopen = true;
        BackGround_Canvas.enabled = true; // ȭ���� ���������� ��׶��� ĵ���� ����(����ȭ��)
        //mainCamera.enabled = false; // ����ī�޶� ����(ui�� ������ �� �ʿ���� ī�޶� ���༭ �ڿ��� �Ƴ�)
        Equip_Canvas.enabled = true; // �κ��丮 ĵ���� ����
        Home.SetActive(true);
        Message_Icon.SetActive(false);
        Character_Icon.SetActive(false);
        Equip_Backbutton.SetActive(true);
        equipType = EquipType.Total;
        EquipMenu_Setting();

    }

    //public void hilight_equiped(int index) //���â ������ ������ �������� Ȱ��ȭ ��Ű�� �Լ� (���� �޴����� ���� ����x)
    //{
    //    for (int i = 0; i < equip_image.Length; i++)
    //    {

    //        equip_image[i].enabled = (i == index); // ��ư onclick()�� �Լ��� �ְ� ����� index�� ���� �迭 �̹����� ������ ������ �� ����
            
    //    }
    //    audioSource.PlayOneShot(Ui_Click);
    //}

    public void hilight_Equip_Menu(int index) //���â ������ ������ �������� Ȱ��ȭ ��Ű�� �Լ� 
    {
        
        for (int i = 0; i < Equip_Menu.Length; i++)
        {

            Equip_Menu[i].SetActive(i == index); // ��ư onclick()�� �Լ��� �ְ� ����� index�� ���� �迭 �̹����� ������ ������ �� ����

            switch (index)
            {
                case 0: 
                    equipType = EquipType.Total;
                    break;
                case 1:
                    equipType = EquipType.Weapon;
                    break;
                case 2:
                    equipType = EquipType.Armor;
                    break;
            }
        }
        for (int j = 0; j < ITemUI_Panel.Length; j++)
        {
            ITemUI_Panel[j].SetActive(false);
            equipped_check[j].SetActive(false);
            EquipIcon_Background[j].color = Color.white;
        }
        EquipMenu_Setting();
        audioSource.PlayOneShot(Ui_Click);
    }

    [Header("[���â ��� ���� ������]")]
    public GameObject[] ITemUI_Panel;
    public TMPro.TMP_Text[] ItemName;
    public TMPro.TMP_Text[] ItemOption;
    public Image[] ItemImage;
    public Image[] EquipIcon_Background;
    public GameObject[] equipped_check;
    

    public void EquipMenu_Setting()
    {
        switch (equipType)
        {
            case EquipType.Total:
                equip_initialize(ItemType.Weapon, ItemType.Armor);
                break;
            case EquipType.Weapon:
                equip_initialize(ItemType.Weapon, ItemType.none);
                break;
            case EquipType.Armor:
                equip_initialize(ItemType.Armor,ItemType.none);
                break;
        }
    }


    void equip_initialize(ItemType a,ItemType b)
    {
        for (int i = 0; i < GameData.Instance.playerdata.Player_inventory.Count; i++)
        {
            if (GameData.Instance.playerdata.Player_inventory[i].itemType == a || GameData.Instance.playerdata.Player_inventory[i].itemType == b)
            {

                for (int j = 0; j < GameData.Instance.playerdata.Player_inventory.Count; j++)
                {
                    if (ITemUI_Panel[j].activeSelf == false)
                    {
                        ITemUI_Panel[j].SetActive(true);

                        switch (GameData.Instance.playerdata.Player_inventory[i].grade)
                        {
                            
                            case Grade.common:

                                if (GameData.Instance.playerdata.Player_inventory[i].Upgrade > 0)
                                    ItemName[j].text = GameData.Instance.playerdata.Player_inventory[i].ItemName + " +" + GameData.Instance.playerdata.Player_inventory[i].Upgrade;
                                else
                                    ItemName[j].text = GameData.Instance.playerdata.Player_inventory[i].ItemName;
                                  break;

                            case Grade.rare:

                                EquipIcon_Background[j].color = Color.green;
                                if (GameData.Instance.playerdata.Player_inventory[i].Upgrade > 0)
                                    ItemName[j].text = "<color=green>"+GameData.Instance.playerdata.Player_inventory[i].ItemName+"</color> +" + GameData.Instance.playerdata.Player_inventory[i].Upgrade;
                                else
                                    ItemName[j].text = "<color=green>"+GameData.Instance.playerdata.Player_inventory[i].ItemName+ "</color>";
                                break;

                            case Grade.epic:

                                EquipIcon_Background[j].color = Color.blue;
                                if (GameData.Instance.playerdata.Player_inventory[i].Upgrade > 0)
                                    ItemName[j].text = "<color=blue>" + GameData.Instance.playerdata.Player_inventory[i].ItemName + "</color> +" + GameData.Instance.playerdata.Player_inventory[i].Upgrade;
                                else
                                    ItemName[j].text = "<color=blue>" + GameData.Instance.playerdata.Player_inventory[i].ItemName + "</color>";
                                break;

                            case Grade.legendary:
                                EquipIcon_Background[j].color = Color.yellow;
                                if (GameData.Instance.playerdata.Player_inventory[i].Upgrade > 0)
                                    ItemName[j].text = "<color=yellow>" + GameData.Instance.playerdata.Player_inventory[i].ItemName + "</color> +" + GameData.Instance.playerdata.Player_inventory[i].Upgrade;
                                else
                                    ItemName[j].text = "<color=yellow>" + GameData.Instance.playerdata.Player_inventory[i].ItemName + "</color>";
                                break;
                        }
                        
                        if (GameData.Instance.playerdata.Player_inventory[i].itemType == ItemType.Weapon)
                        {
                            ItemOption[i].text =
                                    "<color=grey>���ݷ�</color> " + GameData.Instance.playerdata.Player_inventory[i].ATK +
                                    "\n<color=grey>ũ��Ƽ��</color> " + +GameData.Instance.playerdata.Player_inventory[i].Critical;
                        }
                        else if (GameData.Instance.playerdata.Player_inventory[i].itemType == ItemType.Armor)
                        {
                            ItemOption[i].text =
                                     "<color=grey>����</color> " + GameData.Instance.playerdata.Player_inventory[i].DEF +
                                     "\n<color=grey>ü��</color> " + +GameData.Instance.playerdata.Player_inventory[i].HP;
                        }

                        ItemImage[j].sprite = GameData.Instance.playerdata.Player_inventory[i].Mysprite;

                        if (GameData.Instance.playerdata.Player_inventory[i].Equipped)
                        {
                            equipped_check[j].SetActive(true);
                        }
                        break;
                    }

                }
            }
        }
    }


    #endregion

    #region �ɼ�â ��ɵ�

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
    #endregion


    #region ��������â �̵� �Լ�

    public void EnterDunGeon() // �������� ������ �������� �Լ�
    {
        Home.SetActive(true);
        Message_Icon.SetActive(false);
        Character_Icon.SetActive(false);
        audioSource.PlayOneShot(DunGeon_Click);
        StartCoroutine(Delay(1)); // ȿ���� ����� ���� 1�� ������ �ڷ�ƾ (���ϸ� ȿ���� �Ҹ��� �ȳ��� ���̵���)
    }

    IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelecteDungeon");
    }
    #endregion


    #region ���� �޴��� �������� ���̶���Ʈ �Ǵ� �Լ���
  



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
    #endregion

    

}
