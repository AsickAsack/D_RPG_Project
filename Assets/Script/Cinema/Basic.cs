using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basic : MonoBehaviour
{
    public RectTransform upper;
    public RectTransform down;

    // Start is called before the first frame update
    void Start()
    {

    }

  
    void Update()
    {
       
    }

    public void MoveDown()
    {
        Vector2 pos = upper.anchoredPosition;
        pos.y = Mathf.Clamp(pos.y, -100.0f, 0f);
        upper.anchoredPosition = Vector2.Lerp(pos, new Vector2(0, -100.0f), Time.deltaTime * 2.0f);
    }

    public void moveup()
    {
        Vector2 pos = down.anchoredPosition;
        pos.y = Mathf.Clamp(pos.y, 0.0f, 100.0f);
        down.anchoredPosition = Vector2.Lerp(pos, new Vector2(0, 100.0f),Time.deltaTime * 2.0f);

    }


}
