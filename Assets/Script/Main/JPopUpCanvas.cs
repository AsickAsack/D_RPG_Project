using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;

public class JPopUpCanvas : MonoBehaviour
{

    [Header("[UI 캔버스들]")]
    public Camera mainCamera;
    public Canvas Main_Canvas;
    public Canvas BackGround_Canvas;
    public Canvas Equip_Canvas;
    public Canvas Scroll_Canvas;
    public Canvas Equip_DetailCanvas;
    public Canvas Detail_GameObjectCanvas;
    public Canvas Option_Canvas;
    public Canvas ShopCanvas;
    public Canvas Inventory_Canvas;
    public Canvas Exit_Canvas;
    //public Canvas Equip_Canvas;

    [Header("[UI open 확인 변수]")]
    public static bool IsUIopen = false; // ui가 현재 열려있는지 아닌지 확인하는 불값


    //[Header("[장비창 장비 클릭 이미지 배열]")]
    //public Image[] equip_image; // 클릭했을때 활성화 시킬 장비 이미지 배열

    [Header("[장비창 왼쪽 메뉴 이미지 배열]")]
    public GameObject[] Equip_Menu; // 클릭했을때 활성화 시킬 옵션 이미지 배열

    [Header("[옵션 클릭 이미지 배열]")]
    public Image[] option_image; // 클릭했을때 활성화 시킬 옵션 이미지 배열

    [Header("[옵션 시간 텍스트]")]
    public TMPro.TMP_Text Time_Text;

    [Header("[옵션 아이콘 이동기능]")]
    public Slider[] OptionSlider;
    public TMPro.TMP_Text[] Slider_text;
    public RectTransform[] sliderMoveObjectLeft;
    public RectTransform[] sliderMoveObjectRight;
    public GameObject[] option_UIpanel;

    [Header("[옵션 볼륨 기능]")]
    public Slider[] VolumeSlider;

    [Header("[장비창]")]
    public TMPro.TMP_Text[] EquipOption_Text;



    [Header("[장비창 관련]")]
    public GameObject Home;
    public GameObject Character_Icon;
    public GameObject Message_Icon;
    public GameObject Equip_Backbutton;
    public JEquipScroll Scroll_script;

    [Header("[인벤토리]")]
    public GameObject[] inven_Leftmenu;
    public GameObject[] inven_item;
    public InvenItemSetting invenItemSetting;
    public GameObject inventory_okButton;
    public GameObject Use_popup;

    [Header("[오디오 소스,클립]")]
    public AudioClip Ui_Click; // UI클릭했을때 재생할 효과음
    public AudioClip DunGeon_Click; // 던전 선택 클릭했을때 재생할 효과음
    AudioSource audioSource; // 효과음 재생할 오디오클립
    public AudioClip Moneyclip;
    public AudioClip Hammer;
    public AudioClip Teemo;

    #region 옵션창 enum
    public enum Popup //어떤 팝업인지 알려줄 열거자 
    {
        None=0,Iventory_Popup,Equip_Popup,Equip_detail,Shop_Popup
    }

    public Popup popup=Popup.None;
    
    #endregion


    #region 어웨이크 함수
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

    private void Update()
    {
        if(!IsUIopen && Input.GetKeyDown(KeyCode.Escape))
        {
            IsUIopen = true;
            Exit_Canvas.enabled = true;
        }
    }

    public void ExitPopup_NO()
    {
        IsUIopen = false;
        Exit_Canvas.enabled = false;
        audioSource.PlayOneShot(Ui_Click);

    }

    public void ExitPopup_YES()
    {
        audioSource.PlayOneShot(Ui_Click);
        Application.Quit();
    }


    #region 팝업창 끌때 함수

    void set_icon(bool check1,bool check2)
    {
        Equip_Backbutton.SetActive(check1);
        Home.SetActive(check1);
        Message_Icon.SetActive(check2);
        Character_Icon.SetActive(check2);
    }

    public void ExitPopUp()
    {
        switch(popup)
        {
            case Popup.None:
                break;
            case Popup.Iventory_Popup:
                {
                    popup = Popup.None;
                    Inventory_Canvas.enabled = false;
                    set_icon(false, true);
                    BackGround_Canvas.enabled = false;
                }
                break;
            case Popup.Equip_Popup:
                {
                    Equip_Canvas.enabled = false;
                    Scroll_Canvas.enabled = false;
                    set_icon(false, true);
                    popup = Popup.None;
                    BackGround_Canvas.enabled = false;
                }
                break;
            case Popup.Equip_detail:
                {
                    popup = Popup.Equip_Popup;
                    Main_Canvas.enabled = true;
                    Equip_DetailCanvas.enabled = false;
                    Detail_GameObjectCanvas.enabled = false;
                    //Main_Canvas.enabled = true;
                    EquipMenu_Setting();
                    Equip_Backbutton.GetComponentInChildren<TMPro.TMP_Text>().text = "장비";
                    switch(GameData.Instance.playerdata.Player_inventory[CurNum].ItemCode)
                    {
                        case 000:
                            Detail_GameObjectCanvas.transform.GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(false);
                            break;
                        case 001:
                            Detail_GameObjectCanvas.transform.GetChild(2).GetComponent<RectTransform>().gameObject.SetActive(false);
                            break;
                        case 002:
                            Detail_GameObjectCanvas.transform.GetChild(3).GetComponent<RectTransform>().gameObject.SetActive(false);
                            break;
                        case 003:
                            Detail_GameObjectCanvas.transform.GetChild(4).GetComponent<RectTransform>().gameObject.SetActive(false);
                            break;
                    }
                }
                break;
            case Popup.Shop_Popup:
                {

                    popup = Popup.None;
                    ShopCanvas.enabled = false;
                    set_icon(false, true);
                    BackGround_Canvas.enabled = false;
                }
                break;
        }

        audioSource.PlayOneShot(Ui_Click);
        IsUIopen = false;
       // mainCamera.enabled = true;
        
       
        
    }
    #endregion

    #region 상점 함수
    public void ShopPopup_Open()
    {
        popup = Popup.Shop_Popup;
        ShopCanvas.enabled = true;
        //열때 백버튼 수정
        BackGround_Canvas.enabled = true;
        Equip_Backbutton.GetComponentInChildren<TMPro.TMP_Text>().text = "상점";
        set_icon(true, false);
        audioSource.PlayOneShot(Ui_Click);


    }

    public void buy_item(int index)
    {
        switch(index)
        {
            case 1:
                GameData.Instance.playerdata.Gold -= 50000;
                GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[2]);
                break;
            case 2:
                GameData.Instance.playerdata.Gold -= 10000;
                GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[1]);
                break;
            case 3:
                GameData.Instance.playerdata.Gold -= 10000;
                GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[3]);
                break;
            case 4:
                GameData.Instance.playerdata.Gold -= 10000;
                GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[0]);
                break;
            case 5:
                GameData.Instance.playerdata.Gold -= 10000;
                GameData.Instance.playerdata.Player_inventory.Add(GameData.Instance.playerdata.Itemdata[2]);
                break;




        }

        audioSource.PlayOneShot(Moneyclip);
    }


    #endregion

    #region 인벤토리창 함수들
    [SerializeField]
    InvenType invenType = InvenType.Total;


    enum InvenType
    {
        Total = 0, Use, Material
    }

    private int Curitemnum = 0;

    public void ClickInvenItem()
    {
        for(int i=0;i<inven_item.Length;i++)
        {
            inven_item[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }

        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        clickObject.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        invenItemSetting.setItemdetail(clickObject.GetComponent<InvenItemSetting>().num);
        Curitemnum = clickObject.GetComponent<InvenItemSetting>().num;
        if(GameData.Instance.playerdata.Player_inventory2[Curitemnum].itemType2 == ItemType2.Use)
        {
            inventory_okButton.SetActive(true);
        }
        else
        {
            inventory_okButton.SetActive(false);
        }
    }

    public void OpenPopup_use_item()
    {   if(GameData.Instance.playerdata.Player_inventory2.Count!=0)
        { 
        if (GameData.Instance.playerdata.Player_inventory2[Curitemnum].ItemCode==002)
        {
            Use_popup.transform.GetChild(1).GetComponent<Image>().sprite = GameData.Instance.mySprite[GameData.Instance.playerdata.Player_inventory2[Curitemnum].Mysprite];
            Use_popup.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Player_inventory2[Curitemnum].ItemName+"\n"+"정말 사용 하시겠습니까?";
            Use_popup.transform.GetChild(3).gameObject.SetActive(true);
            Use_popup.transform.GetChild(4).gameObject.SetActive(true);
            Use_popup.transform.GetChild(5).gameObject.SetActive(false);
            Use_popup.SetActive(true);
        }
        }
    }

    public void use_item()
    {
        Use_popup.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = "\n  두루마리 사용중..";
        Use_popup.transform.GetChild(3).gameObject.SetActive(false);
        Use_popup.transform.GetChild(4).gameObject.SetActive(false);
        StartCoroutine(use_sroll());
    }

    IEnumerator use_sroll()
    {
        int a = Random.Range(10000, 500001);
        yield return new WaitForSeconds(1.5f);

        Use_popup.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = "\n   "+a.ToString("N0")+"    골드를\n얻었습니다!";
        GameData.Instance.playerdata.Gold += a;
        audioSource.PlayOneShot(Moneyclip);
        Use_popup.transform.GetChild(5).gameObject.SetActive(true);
        GameData.Instance.playerdata.Player_inventory2.Remove(GameData.Instance.playerdata.Player_inventory2[Curitemnum]);
        inven_setting();
        for (int i = 0; i < inven_item.Length; i++)
        {
            inven_item[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        if (GameData.Instance.playerdata.Player_inventory2.Count != 0)
        {
            invenItemSetting.setItemdetail(inven_item[0].GetComponent<InvenItemSetting>().num);
            inven_item[0].transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
    }


    public void Open_Inventory()
    {
        popup = Popup.Iventory_Popup;
        Inventory_Canvas.enabled = true;
        BackGround_Canvas.enabled = true;
        Equip_Backbutton.GetComponentInChildren<TMPro.TMP_Text>().text = "소지품";
        set_icon(true, false);
        audioSource.PlayOneShot(Ui_Click);
        inven_setting();
        for (int i = 0; i < inven_item.Length; i++)
        {
            inven_item[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        if (GameData.Instance.playerdata.Player_inventory2.Count != 0)
        {
            invenItemSetting.setItemdetail(inven_item[0].GetComponent<InvenItemSetting>().num);
            inven_item[0].transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
    }

    public void hilight_invenMenu(int index)
    {
        for (int i = 0; i < inven_Leftmenu.Length; i++)
        {
            inven_Leftmenu[i].SetActive(i == index);
            
            switch(index)
            {
                case 0: 
                    invenType = InvenType.Total;
                    break;
                case 1:
                    invenType = InvenType.Use;
                    break;
                case 2:
                    invenType = InvenType.Material;
                    break;

            }
        }
        inven_setting();
        audioSource.PlayOneShot(Ui_Click);
        for (int i = 0; i < inven_item.Length; i++)
        {
            inven_item[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        if (GameData.Instance.playerdata.Player_inventory2.Count != 0)
        {
            invenItemSetting.setItemdetail(inven_item[0].GetComponent<InvenItemSetting>().num);
            inven_item[0].transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
    }
   
    public void inven_setting()
    {   if(GameData.Instance.playerdata.Player_inventory2.Count != 0)
        { 
        switch(invenType)
        {
            case InvenType.Total:
                inven_initialize(ItemType2.Use, ItemType2.Material);
                if(GameData.Instance.playerdata.Player_inventory2[inven_item[0].GetComponent<InvenItemSetting>().num].itemType2==ItemType2.Use)
                    inventory_okButton.SetActive(true);
                else
                    inventory_okButton.SetActive(false);
                break;
            case InvenType.Use:
                inven_initialize(ItemType2.Use, ItemType2.None);
                inventory_okButton.SetActive(true);
                break;
            case InvenType.Material:
                inven_initialize(ItemType2.Material, ItemType2.None);
                inventory_okButton.SetActive(false);
                break;

        }
        }
        else
        {
            inven_initialize(ItemType2.Use, ItemType2.Material);
        }
    }

    void inven_initialize(ItemType2 a, ItemType2 b)
    {
        for (int j = 0; j < inven_item.Length; j++)
        {
            inven_item[j].SetActive(false);

        }
            for (int i = 0; i < GameData.Instance.playerdata.Player_inventory2.Count; i++)
        {
            if (GameData.Instance.playerdata.Player_inventory2[i].itemType2 == a || GameData.Instance.playerdata.Player_inventory2[i].itemType2 == b)
            {

                for (int j = 0; j < GameData.Instance.playerdata.Player_inventory2.Count; j++)
                {
                    if (inven_item[j].activeSelf == false)
                    {
                        inven_item[j].SetActive(true);
                        inven_item[j].transform.GetChild(1).GetComponent<Image>().sprite = GameData.Instance.mySprite[GameData.Instance.playerdata.Player_inventory2[i].Mysprite]; 
                        inven_item[j].GetComponent<InvenItemSetting>().setItemNum(i);
                        inven_item[j].GetComponent<Button>().onClick.AddListener(()=>ClickInvenItem());


                        break;
                    }
                }

            }
        }
    }


    #endregion


    #region 장비창 함수들

    [SerializeField]
    EquipType equipType;
    
    
    enum EquipType
    {
        Total = 0,Weapon,Armor
    }

    private int CurNum = 0;


    public void ClickEquipItem()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        popup = Popup.Equip_detail;
        //Main_Canvas.enabled = false;
        Equip_DetailCanvas.enabled = true;
        Detail_GameObjectCanvas.enabled = true;
        Main_Canvas.enabled = false;
        Equip_Backbutton.GetComponentInChildren<TMPro.TMP_Text>().text = "    장비 정보";
        jSetItemDetail.setItem(clickObject.GetComponent<JSetItemDetail>().num);
        CurNum = clickObject.GetComponent<JSetItemDetail>().num;
    }

    public GameObject[] upgradepannel;
    private bool upgrade_Check;

    public void Upgrade()
    {
        upgrade_Check = false;

        if (7 > GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade)
        {
            upgradepannel[0].SetActive(true);
            upgradepannel[1].SetActive(true);

            upgradepannel[1].transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text = "<color=grey>업그레이드</color> " + GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade +
           "  →  " + (GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade + 1).ToString();


            upgradepannel[1].transform.GetChild(4).GetComponent<TMPro.TMP_Text>().text =
                 "<color=grey>확률</color>  " + (GameData.Instance.Upgrade_chance - (GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade * 10)) + "%";
            upgradepannel[1].transform.GetChild(5).GetComponent<TMPro.TMP_Text>().text =
                 "<color=grey>필요 골드</color>  " + (GameData.Instance.Upgrade_Money * (GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade + 1)).ToString("N0");
        }
        else
        {

            upgradenotice[0].SetActive(true);
            upgradenotice[1].SetActive(true);
            upgradenotice[1].GetComponentInChildren<TMPro.TMP_Text>().text = "+7 이상 강화 불가!";
            upgradenotice[1].transform.GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(true);
        }
        

    }

    public GameObject[] upgradenotice;

    public void reloading()
    {
        if(upgrade_Check)
        jSetItemDetail.setItem(CurNum);
    }


    public void Upgrade_Yes()
    {
       
        if (GameData.Instance.playerdata.Gold < GameData.Instance.Upgrade_Money * (GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade + 1))
        {
            upgradenotice[0].SetActive(true);
            upgradenotice[1].SetActive(true);
            upgradenotice[1].GetComponentInChildren<TMPro.TMP_Text>().text = "돈이 부족합니다.";
            upgradenotice[1].transform.GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(UpgradeProcess());
        }
    }
    
    IEnumerator UpgradeProcess()
    {
        int random = Random.Range(1, 101);
        GameData.Instance.playerdata.Gold -= GameData.Instance.Upgrade_Money * (GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade + 1);
        if (random <= GameData.Instance.Upgrade_chance - (GameData.Instance.playerdata.Player_inventory[CurNum].Upgrade * 10))
        {
            upgradenotice[0].SetActive(true);
            upgradenotice[1].SetActive(true);
            upgradenotice[1].GetComponentInChildren<TMPro.TMP_Text>().text = "강화중...";
            audioSource.PlayOneShot(Hammer);
            yield return new WaitForSeconds(1.5f);
            switch(GameData.Instance.playerdata.Player_inventory[CurNum].itemType)
            {
                case ItemType.Weapon:
                    {
                        GameData.Instance.playerdata.UpgradeWeapon(GameData.Instance.playerdata.Player_inventory, CurNum, 50, 30);
                    }
                    break;
                case ItemType.Armor:
                    {
                        GameData.Instance.playerdata.UpgradeArmor(GameData.Instance.playerdata.Player_inventory, CurNum, 50, 100);
                    }
                    break;

            }
            GameData.Instance.playerdata.chanegeUpgrade(GameData.Instance.playerdata.Player_inventory, CurNum, 1);
            upgradenotice[1].GetComponentInChildren<TMPro.TMP_Text>().text = "강화 성공!";
            upgrade_Check = true;
            upgradenotice[1].transform.GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(true);
        }
        else
        {
            upgradenotice[0].SetActive(true);
            upgradenotice[1].SetActive(true);
            upgradenotice[1].GetComponentInChildren<TMPro.TMP_Text>().text = "잠시만 기다려 주세요...";
            audioSource.PlayOneShot(Hammer);
            yield return new WaitForSeconds(1.5f);

            upgradenotice[1].GetComponentInChildren<TMPro.TMP_Text>().text = "강화 실패!";
            audioSource.PlayOneShot(Teemo);
            upgradenotice[1].transform.GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(true);
        }

        yield return null;
    }


    public void Open_Equip_Popup()
    {
        popup = Popup.Equip_Popup; 
        audioSource.PlayOneShot(Ui_Click); 
        IsUIopen = true;
        BackGround_Canvas.enabled = true; // 화면을 가리기위해 백그라운드 캔버스 켜줌(검은화면)
        //mainCamera.enabled = false; // 메인카메라 꺼줌(ui가 켜지면 볼 필요없는 카메라를 꺼줘서 자원을 아낌)
        Equip_Canvas.enabled = true; // 인벤토리 캔버스 켜줌
        Scroll_Canvas.enabled = true;
        set_icon(true, false);
        equipType = EquipType.Total;
        //Scroll_script.Scrolling();
        EquipMenu_Setting();

    }



    public void hilight_Equip_Menu(int index) //장비창 아이템 아이콘 눌렀을때 활성화 시키는 함수 
    {
        
        for (int i = 0; i < Equip_Menu.Length; i++)
        {

            Equip_Menu[i].SetActive(i == index); // 버튼 onclick()에 함수를 넣고 써놓은 index와 같은 배열 이미지만 켜지고 나머진 다 꺼짐

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

        EquipMenu_Setting();
        audioSource.PlayOneShot(Ui_Click);
    }

    [Header("[장비창 기능 구현 변수들]")]
    public GameObject[] ITemUI_Panel;
    

    public void EquipMenu_Setting()
    {
        for (int j = 0; j < ITemUI_Panel.Length; j++)
        {
            ITemUI_Panel[j].SetActive(false);
            ITemUI_Panel[j].transform.GetChild(4).gameObject.SetActive(false);
            ITemUI_Panel[j].transform.GetChild(2).GetComponent<Image>().color = Color.white;
            ITemUI_Panel[j].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().color = Color.white;
        }
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
    public JSetItemDetail jSetItemDetail;

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
                            case Grade.rare:
                                ITemUI_Panel[j].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().color = Color.green;
                                ITemUI_Panel[j].transform.GetChild(2).GetComponent<Image>().color = Color.green;
                                break;
                            case Grade.epic:
                                ITemUI_Panel[j].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().color = Color.blue;
                                ITemUI_Panel[j].transform.GetChild(2).GetComponent<Image>().color = Color.blue;
                                break;
                            case Grade.legendary:
                                ITemUI_Panel[j].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().color = Color.yellow;
                                ITemUI_Panel[j].transform.GetChild(2).GetComponent<Image>().color = Color.yellow;
                                break;
                        }

                        if (GameData.Instance.playerdata.Player_inventory[i].Upgrade > 0)
                        { 
                            ITemUI_Panel[j].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = 
                            GameData.Instance.playerdata.Player_inventory[i].ItemName + " <color=white>+" + GameData.Instance.playerdata.Player_inventory[i].Upgrade+"</color>";
                        }
                        else
                        { 
                            ITemUI_Panel[j].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Player_inventory[i].ItemName;
                        }

                        if (GameData.Instance.playerdata.Player_inventory[i].itemType == ItemType.Weapon)
                        {
                            ITemUI_Panel[j].transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text =
                                    "<color=grey>공격력</color> " + GameData.Instance.playerdata.Player_inventory[i].ATK +
                                    "\n<color=grey>크리티컬</color> " + +GameData.Instance.playerdata.Player_inventory[i].Critical;
                        }
                        else if (GameData.Instance.playerdata.Player_inventory[i].itemType == ItemType.Armor)
                        {
                            ITemUI_Panel[j].transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text =
                                     "<color=grey>방어력</color> " + GameData.Instance.playerdata.Player_inventory[i].DEF +
                                     "\n<color=grey>체력</color> " + +GameData.Instance.playerdata.Player_inventory[i].HP;
                        }

                        ITemUI_Panel[j].transform.GetChild(3).GetComponent<Image>().sprite = GameData.Instance.mySprite[GameData.Instance.playerdata.Player_inventory[i].Mysprite];

                        if (GameData.Instance.playerdata.Player_inventory[i].Equipped)
                        {
                            ITemUI_Panel[j].transform.GetChild(4).gameObject.SetActive(true);
                        }

                        ITemUI_Panel[j].GetComponent<JSetItemDetail>().number(i);

                        //나중에 아이콘누르면 들어가게 바꾸기
                        ITemUI_Panel[j].GetComponent<Button>().onClick.AddListener(() => ClickEquipItem());
                        break;
                    }
                    
                }
            }
        }
    }


    #endregion

    #region 옵션창 기능들
    public void Exit_Option()
    {
        Option_Canvas.enabled = false;
        StopCoroutine(TimeText());
        audioSource.PlayOneShot(Ui_Click);
        IsUIopen = false;
        for (int i = 0; i < option_image.Length; i++)
        {

            option_image[i].enabled = false;

        }
    }


    public void Open_Option()
    {
       
        audioSource.PlayOneShot(Ui_Click);
        IsUIopen = true;
        //BackGround_Canvas.enabled = true; 
        //mainCamera.enabled = false; 
        Option_Canvas.enabled = true;
        option_image[0].enabled = true; // 옵션창을 켰을때 가장 첫번째 버튼을 활성화 시키기 위함
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

    public void Option_Volume()
    {
        Camera.main.GetComponent<AudioSource>().volume = VolumeSlider[0].value;
        VolumeSlider[0].transform.GetChild(4).GetComponent<TMPro.TMP_Text>().text = Mathf.RoundToInt(VolumeSlider[0].value*100).ToString();


        audioSource.volume = VolumeSlider[1].value;
        VolumeSlider[1].transform.GetChild(4).GetComponent<TMPro.TMP_Text>().text = Mathf.RoundToInt(VolumeSlider[1].value * 100).ToString();
    }


    public void hilight_option(int index) //옵션 상단 메뉴들 눌렀을때
    {
        for (int i = 0; i < option_image.Length; i++)
        {

            option_image[i].enabled = (i == index);

            if (index == 0)
            {
                option_UIpanel[0].SetActive(true);
                option_UIpanel[1].SetActive(false);
                option_UIpanel[2].SetActive(false);
            }
            else if(index == 1)
            {
                option_UIpanel[0].SetActive(false);
                option_UIpanel[1].SetActive(true);
                option_UIpanel[2].SetActive(false);
            }
            else
            {
                option_UIpanel[0].SetActive(false);
                option_UIpanel[1].SetActive(false);
                option_UIpanel[2].SetActive(true);
            }
        }
        audioSource.PlayOneShot(Ui_Click);
    }
    #endregion


    #region 던전선택창 이동 함수

    public void EnterDunGeon() // 던전선택 아이콘 눌렀을때 함수
    {
       
        audioSource.PlayOneShot(DunGeon_Click);
        StartCoroutine(Delay(1)); // 효과음 재생을 위한 1초 딜레이 코루틴 (안하면 효과음 소리가 안나고 씬이동함)
    }

    IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
        ClickCanvas.Instance.Click_Canvas.gameObject.SetActive(false);
        SceneLoader.Instance.LoadScene(3);
    }
    #endregion



    

}
