using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test11 : MonoBehaviour
{
    public void gogame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }

}
