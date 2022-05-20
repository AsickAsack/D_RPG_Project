using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class POptionPanel : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject[] option_UIpanel;
    public GameObject OptionPanel;
    public TMPro.TMP_Text Time_Text;
    [Header("[옵션 클릭 이미지 배열]")]
    public Image[] option_image;
    public AudioClip Ui_Click;
    // Start is called before the first frame update
    void Start()
    {
        if(OptionPanel.activeSelf)
        {
            StartCoroutine(TimeText());
            option_image[0].enabled = true;
            PMoveCamera.Ins.Click = false;
        }
        
    }
    public void close()
    {
        PMoveCamera.Ins.Click = true;
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
            else if (index == 2)
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
        //audioSource.PlayOneShot(Ui_Click);
    }
    // Update is called once per frame
    IEnumerator TimeText()
    {
        while (true)
        {
            Time_Text.text = System.DateTime.Now.ToString("yyyy-MM-dd tt hh:mm:ss");

            yield return null;
        }
    }
    void Update()
    {
        
    }
}
