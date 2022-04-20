using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JMain : MonoBehaviour 
{

    public GameObject Click;
    public GameObject ClickTarget;
    public UnityEngine.UI.Image Battlebtn_light;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Battlebtn_light.gameObject.transform.Rotate(-Vector3.forward * Time.deltaTime * 90.0f);
       
            if (Input.GetMouseButtonUp(0))
            {
                Click.transform.position = Input.mousePosition;
                Click.SetActive(true);
               StartCoroutine(Click_Delay(0.5f));
                
            }
      
    }

    IEnumerator Click_Delay(float t)
    {
        yield return new WaitForSeconds(t);
        Click.SetActive(false); 


    }


}
