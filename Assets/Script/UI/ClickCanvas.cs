using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [Header("[클릭 이미지 게임오브젝트]")]
    public GameObject Click;
    public Camera ClickUiCamera;
    public Canvas Click_Canvas;


    void Update()
    {
        _Click();
    }


    void _Click()
    {
        if (Input.GetMouseButtonUp(0))
        {

            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), Input.mousePosition, ClickUiCamera, out Vector2 anchoredpos);
            Click.GetComponent<RectTransform>().anchoredPosition = anchoredpos;
            Click.GetComponentInChildren<ParticleSystem>().Play();


        }
    }
}
