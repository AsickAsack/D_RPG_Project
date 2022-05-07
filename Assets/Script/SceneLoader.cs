using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    private static SceneLoader instance = null;

    public static SceneLoader Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
                if(instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SceneLoader";
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<SceneLoader>();
                }
            }
            return instance;
        } 
    }

    public void LoadScene(int i)
    {
        StartCoroutine(SceneLoading(i));



    }

    IEnumerator SceneLoading(int i)
    {
        yield return SceneManager.LoadSceneAsync("LoadingScene");
        yield return StartCoroutine(Loading(i));
        
    }

    IEnumerator Loading(int i)
    {

        Slider loadingSlider = GameObject.Find("LoadingProgress")?.GetComponent<Slider>(); //LoadingProgress라는 이름을 가진 게임오브젝트를 찾아서 널이 아니라면?
        AsyncOperation ao = SceneManager.LoadSceneAsync(i);
        //씬로딩이 끝나기 전까진 씬을 활성화하지 않는다.
        ao.allowSceneActivation = false;

        //isDone == false -> 로딩중 / true ->로딩이 끝
        while (!ao.isDone)
        {
            float v = Mathf.Clamp01(ao.progress / 0.9f);
            if (loadingSlider != null)
                loadingSlider.value = v;

            if (Mathf.Approximately(v, 1.0f))
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }



    }


}
