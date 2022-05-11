using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUIPosition : MonoBehaviour
{
    public Transform Uipos;
    public GameObject Lock;
    public GameObject SelectImg;

    
    public void OnClickButton()
    {
        
        
        if(Lock.activeSelf == false )
        {
            Debug.Log("클릭가능");
            SelectImg.SetActive(true);
        }
        else
        {
            Debug.Log("클릭불가능");
            


        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       this.GetComponent<RectTransform>().anchoredPosition = Camera.allCameras[0].WorldToScreenPoint(Uipos.position);

    }   
}

