using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PClickEffect : MonoBehaviour
{
    public Image clickimg;
    public float timer=0.0f;
    float waitingtime =  0.3f;
    public Canvas optioncanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)&&optioncanvas.gameObject.activeSelf==false)
        {
            Click();
           
           
        }
        if(clickimg.gameObject.activeSelf==true)
        timer += Time.deltaTime;
        if (timer > waitingtime)
        {
           

            timer = 0.0f;
            clickimg.gameObject.SetActive(false);
        }

    }
    public void Click()
    {
      
        clickimg.transform.position = Input.mousePosition;
        clickimg.gameObject.SetActive(true);
       
    }
}