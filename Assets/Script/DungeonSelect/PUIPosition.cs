using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUIPosition : MonoBehaviour
{
    public Transform Desert01;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       this.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(Desert01.position);

    }   
}

