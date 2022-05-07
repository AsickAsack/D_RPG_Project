using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobackMain : MonoBehaviour
{
    public void Goback_MainScene()
    {
        ClickCanvas.Instance.Click_Canvas.gameObject.SetActive(true);
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(2);

    }

}