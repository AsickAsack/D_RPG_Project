using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUIPosition : MonoBehaviour
{
    public float Rot;
    public Transform MainCamera;
    public Transform Uipos;
    public GameObject Lock;
    public GameObject SelectImg;
    PMoveCamera pmovecamera;
    public Transform ClickCamPos;
    public GameObject mypanel;
    public GameObject selectpanel;
    public void OnClickButton()
    {
        
        
        if(Lock.activeSelf == false )
        {

           pmovecamera.Cameramovestate = false;
            MainCamera.position = ClickCamPos.position;
            
            MainCamera.rotation = ClickCamPos.rotation;
            mypanel.SetActive(true);
            SelectImg.SetActive(true);
            selectpanel.SetActive(false);
        }
        else
        {
            Debug.Log("클릭불가능");
            


        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        pmovecamera = PMoveCamera.instance;
    }

    // Update is called once per frame
    void Update()
    {
       this.transform.position = Camera.allCameras[0].WorldToScreenPoint(Uipos.position);

    }   
}

