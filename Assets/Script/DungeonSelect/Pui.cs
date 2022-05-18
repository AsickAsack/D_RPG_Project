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
    public GameObject blackbar;
    public GameObject clickbar;
    public Transform backcampos;
    public void OnClickButton()
    {


        if (Lock.activeSelf == false)
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
        blackbar.SetActive(true);
        clickbar.SetActive(false);
        SelectImg.SetActive(false);
    }
    IEnumerator click()
    {


        while (Vector3.Distance(camtr.position, clicktr.position) > 0.1f)
        {
            camtr.position = Vector3.Lerp(camtr.position, clicktr.position, Time.deltaTime * 1.5f);
            yield return null;
        }

        blackbar.SetActive(false);
        clickbar.SetActive(true);
    }
    IEnumerator back()
    {
        while (Vector3.Distance(camtr.position, backcampos.position) > 0.1f)
        {
            camtr.position = Vector3.Lerp(camtr.position, backcampos.position, Time.deltaTime * 1.5f);
            yield return null;
        }
        PMoveCamera.Ins.Click = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.allCameras[0].WorldToScreenPoint(Uipos.position);

    }
}

