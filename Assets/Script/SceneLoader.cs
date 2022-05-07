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

        Slider loadingSlider = GameObject.Find("LoadingProgress")?.GetComponent<Slider>(); //LoadingProgress��� �̸��� ���� ���ӿ�����Ʈ�� ã�Ƽ� ���� �ƴ϶��?
        AsyncOperation ao = SceneManager.LoadSceneAsync(i);
        //���ε��� ������ ������ ���� Ȱ��ȭ���� �ʴ´�.
        ao.allowSceneActivation = false;

        //isDone == false -> �ε��� / true ->�ε��� ��
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
