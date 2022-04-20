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

    public void ChangeWomen()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
        // Update is called once per frame
     void Update()
     {
         transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0.0f, transform.localPosition.y, transform.localPosition.z), Time.deltaTime * 1.0f);
            if (Rot > Mathf.Epsilon)
             {
                float delta = Time.deltaTime * 180.0f;
                if (delta > Rot)
                 {
                    delta = Rot;
                 }
                Rot -= delta;
                transform.Rotate(Vector3.up * delta);
             }
           
     }
   

}

