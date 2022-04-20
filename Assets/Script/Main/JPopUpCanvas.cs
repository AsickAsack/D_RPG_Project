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
    public bool IsUIopen = false; // ui가 현재 열려있는지 아닌지 확인하는 불값

    public Slider UI_Move_Slider;
    public GameObject Right_Button;

    [Header("equip_select_image")]
    public Image[] equip_image; // 클릭했을때 활성화 시킬 장비 이미지 배열

    [Header("Option_select_image")]
    public Image[] option_image; // 클릭했을때 활성화 시킬 옵션 이미지 배열


    //[Header("Option_Button")]
    //public int[] option_Button;

    [SerializeField]
    private AudioClip Ui_Click; // UI클릭했을때 재생할 효과음
    [SerializeField]
    private AudioClip DunGeon_Click; // 던전 선택 클릭했을때 재생할 효과음
    
    AudioSource audioSource; // 효과음 재생할 오디오클립

    public enum Popup //어떤 팝업인지 알려줄 열거자 
    {
        None=0,Iventory_Popup,Equip_Popup,Option_Popup
    }

    Popup popup=Popup.None;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }



    public void ExitPopUp() // 팝업창 끌때 함수
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
        popup = Popup.Iventory_Popup;  // popup enum을 인벤토리 팝업으로 설정
        audioSource.PlayOneShot(Ui_Click); 
        IsUIopen = true;
        BackGround_Canvas.enabled = true; // 화면을 가리기위해 백그라운드 캔버스 켜줌(검은화면)
        mainCamera.enabled = false; // 메인카메라 꺼줌(ui가 켜지면 볼 필요없는 카메라를 꺼줘서 자원을 아낌)
        Inventory_Canvas.enabled = true; // 인벤토리 캔버스 켜줌

    }

    public void Open_Option()
    {
        popup = Popup.Option_Popup;
        audioSource.PlayOneShot(Ui_Click);
        IsUIopen = true;
        BackGround_Canvas.enabled = true; 
        mainCamera.enabled = false; 
        Option_Canvas.enabled = true;
        option_image[0].enabled = true; // 옵션창을 켰을때 가장 첫번째 버튼을 활성화 시키기 위함

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




    public void EnterDunGeon() // 던전선택 아이콘 눌렀을때 함수
    {
        audioSource.PlayOneShot(DunGeon_Click);
        StartCoroutine(Delay(1)); // 효과음 재생을 위한 1초 딜레이 코루틴 (안하면 효과음 소리가 안나고 씬이동함)
    }

    IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelecteDungeon");
    }

    
    public void hilight_equiped(int index) //장비창 아이템 아이콘 눌렀을때 활성화 시키는 함수 (왼쪽 메뉴들은 아직 구현x)
    {
        for(int i=0; i<equip_image.Length; i++)
        {
            equip_image[i].enabled = (i == index); // 버튼 onclick()에 함수를 넣고 써놓은 index와 같은 배열 이미지만 켜지고 나머진 다 꺼짐
            audioSource.PlayOneShot(Ui_Click);

        }

    }


    public void hilight_option(int index) //옵션 상단 메뉴들 눌렀을때
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
