using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;



public class IntroManger : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    public Canvas IntroCanvas;
    public PlayableDirector playable;
    public Basic basic;
    public RunAnim myBool;
    public Canvas SkipCanvas;

    void Start()
    {
        ClickCanvas.Instance.Click_Canvas.enabled = true;
        StartCoroutine(DeLayTime(3));
        
    }

  
    IEnumerator DeLayTime(float time)  //인트로 재생후 초기화면 나타나게하는 코드
    {
        
        yield return new WaitForSeconds(time);
        IntroPanel.SetActive(false);
        IntroCanvas.enabled = false;
        playable.enabled = true;
        playable.Play();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(playable.enabled == true)
        {
            SkipCanvas.enabled = true;
            basic.moveup();
            basic.MoveDown();

        }

        if(myBool.EndCinema)
        {
            
            SkipCanvas.enabled = false;
            IntroCanvas.enabled = true;
            StartPanel.SetActive(true);
            StartPanel.transform.GetChild(3).GetComponent<Image>().fillAmount -= Time.deltaTime;
            StartPanel.transform.GetChild(3).GetComponent<Image>().raycastTarget = false;
           
        }

    }

    public void Skip()
    {
        myBool.EndCinema = true;
        playable.Stop();
    }

    public void GoSelectScene()
    {
        if(StartPanel.activeSelf)
        { 
        if (GameData.Instance.playerdata.FirstGame)
        {
                SceneLoader.Instance.Loading_LoadScene(1);
            GameData.Instance.playerdata.FirstGame = false;
        }
        else
                SceneLoader.Instance.Loading_LoadScene(2);
        }
    }

    
}
