using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SCharacterSelect : MonoBehaviour
{
    public GameObject button;
    public Vector3 Dir;
    float Rot;
    bool Move = false;

    public void ChangeWomen()
    {
        Move = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
        // Update is called once per frame
     void Update()
     {
        if (Move == true)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0.0f, transform.localPosition.y, transform.localPosition.z), Time.deltaTime * 1.0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 170, 0), Time.deltaTime * 5.0f);
        } 
     }
   

}

