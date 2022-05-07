using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
        popup2[1].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "���� ĳ���ʹ� �غ����Դϴ�.";
        popup2[0].SetActive(true);
        popup2[1].SetActive(true);

    }


    public void inputNickname()
    {
       if(inputText.text.Length<=3)
        {
            popup[1].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "�ּ� 2���� �̻� �Է����ּ���!";
            popup[0].SetActive(true);
            popup[1].SetActive(true);
        }
       else if(inputText.text.Length>11)
        {
            popup[1].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "10���� ���Ϸ� �Է����ּ���!";
            popup[0].SetActive(true);
            popup[1].SetActive(true);
        }
        else
        {
            if(inputText.text.Contains(" "))
            {
                popup[1].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "���� ���� �Է����ּ���!";
                popup[0].SetActive(true);
                popup[1].SetActive(true);
            }
            else 
            { 
                GameData.Instance.playerdata.Nickname = inputText.text;
                SceneLoader.Instance.LoadScene(2);
            }



        }

       
    }

   
}

