using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUIPosition : MonoBehaviour
{
    public Transform Uipos;
    public GameObject Lock;
    public GameObject SelectImg;
    public Transform clicktr;
    public Transform camtr;
    
    public GameObject backbtn;
    public GameObject character;
    public Transform backcampos;
    public GameObject OptionPanel;
    public GameObject Selectcanvas;
    [SerializeField] bool clicktf = true;
    public void OnClickButton()
    {

       
        if (Lock.activeSelf == false && OptionPanel.activeSelf == false && clicktf == true)
        {
            PMoveCamera.Ins.Click = false;
            StartCoroutine(click());
           
            SelectImg.SetActive(true);  
        }
        else
        {
            Debug.Log("클릭불가능");
            


        }
    }
    public void BackButtonClick()
    {
        
        StartCoroutine(back());
        backbtn.SetActive(false);
        character.SetActive(true);
        SelectImg.SetActive(false);
        Selectcanvas.SetActive(false);
    }
    IEnumerator click()
    {

        
        while (Vector3.Distance( camtr.position ,clicktr.position) > 0.1f)
        {
            clicktf = false;
            camtr.position = Vector3.Lerp(camtr.position, clicktr.position, Time.deltaTime * 1.5f);
            yield return null;
            
        }
        Selectcanvas.SetActive(true);

        backbtn.SetActive(true);
        character.SetActive(false);
        clicktf = true;
    }
    IEnumerator back()
    {
        while(Vector3.Distance(camtr.position,backcampos.position)>0.1f)
        {
            clicktf = false;
            camtr.position = Vector3.Lerp(camtr.position, backcampos.position, Time.deltaTime * 1.5f);
            yield return null;
        }

        
        PMoveCamera.Ins.Click = true;
        clicktf = true;
    }
    
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
       this.transform.position = Camera.allCameras[0].WorldToScreenPoint(Uipos.position);

    }   
}

