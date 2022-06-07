using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCanvas : MonoBehaviour
{
    private static ClickCanvas instance = null;

    public static ClickCanvas Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ClickCanvas>();
                if (instance == null)
                {
                    GameObject click = Instantiate(Resources.Load("Click")) as GameObject;
                    click.name = "ClickEffect";
                    instance = click.GetComponent<ClickCanvas>();
                    DontDestroyOnLoad(click);

                }
            }

            return instance;
        }

    }

    public Canvas Click_Canvas;
    public Image clickimg;
    public float timer = 0.0f;
    float waitingtime = 0.3f;
   


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _Click();
        }

        if (clickimg.gameObject.activeSelf == true)
        { 
            timer += Time.deltaTime;
        }
        if (timer > waitingtime)
        {
            timer = 0.0f;
            clickimg.gameObject.SetActive(false);
        }
    }


    void _Click()
    {
        
            clickimg.transform.position = Input.mousePosition;
            clickimg.gameObject.SetActive(true);

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), Input.mousePosition, ClickUiCamera, out Vector2 anchoredpos);
            //Click.GetComponent<RectTransform>().anchoredPosition = anchoredpos;
            //Click.GetComponentInChildren<ParticleSystem>().Play();


        
    }
}

