using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;


public class SCharacterSelect : MonoBehaviour
{

    public TMPro.TMP_Text inputText;
    public GameObject[] popup;
    public GameObject[] popup2;
    public GameObject[] Costume_Selected;
    public GameObject[] Costume;
  
    void Start()
    {
        
    }

    public void Cos_Change(int index)
    {
        for (int i = 0; i < Costume.Length; i++)
        {
            Costume[i].SetActive(i == index);
        }

    }

    public void Cos_Selected(int index)
    {
        for(int i=0;i< Costume_Selected.Length;i++)
        {
            Costume_Selected[i].SetActive(i == index);
        }


    }


    
    void Update()
    {
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 170, 0), Time.deltaTime * 5.0f);
    }

    public void selectMan()
    {
        popup2[1].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "남자 캐릭터는 준비중입니다.";
        popup2[0].SetActive(true);
        popup2[1].SetActive(true);

    }


    public void inputNickname()
    {


            popup[0].SetActive(true);
            popup[1].SetActive(true);

            if(inputText.text.Length>=3)
            {
                popup[0].SetActive(false);
                popup[1].SetActive(false);
                GameData.Instance.playerdata.Nickname = inputText.text;
                SceneLoader.Instance.Loading_LoadScene(2);
            }
            else
            {
                popup[1].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "닉네임을 2글자 이상 입력해 주세요.";
            }
    }

    }

   


