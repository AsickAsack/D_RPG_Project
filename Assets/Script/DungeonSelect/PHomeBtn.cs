using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PHomeBtn : MonoBehaviour
{
    // Start is called before the first frame update
  public void OnclickHomeBtn()
    {
        SceneLoader.Instance.Loading_LoadScene(2);
    }
}
