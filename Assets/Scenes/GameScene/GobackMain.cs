using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobackMain : MonoBehaviour
{
    public void Goback_MainScene()
    {
        ClickCanvas.Instance.Click_Canvas.enabled = true;
        Time.timeScale = 1;
        SceneLoader.Instance.Loading_LoadScene(2);

    }

}