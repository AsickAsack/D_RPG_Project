using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManger : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    
 
    void Start()
    {
       
        StartCoroutine(DeLayTime(3));
    }

  
    IEnumerator DeLayTime(float time)  //인트로 재생후 초기화면 나타나게하는 코드
    {
        yield return new WaitForSeconds(time);
        IntroPanel.SetActive(false);
        StartPanel.SetActive(true);
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoSelectScene()
    {
        //if(StartPanel.activeSelf)
        //{ 
        //if (GameData.Instance.playerdata.FirstGame)
        //{
        //    SceneManager.LoadScene("CharacterSelectScene");
        //    GameData.Instance.playerdata.FirstGame = false;
        //}
        //else
            SceneManager.LoadScene("MainScene");
        //}
    }

    
}
