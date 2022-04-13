using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManger : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    public bool IntroScene=true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeLayTime(3));
    }

  
    IEnumerator DeLayTime(float time)  //��Ʈ�� ����� �ʱ�ȭ�� ��Ÿ�����ϴ� �ڵ�
    {
        yield return new WaitForSeconds(time);
        IntroPanel.SetActive(false);
        StartPanel.SetActive(true);
        IntroScene = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoSelectScene()
    {
        if (IntroScene == false)
        {
            SceneManager.LoadScene(1);
        }
       
       
    }
}
