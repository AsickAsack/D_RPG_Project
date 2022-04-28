using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JItem1 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private GameObject Item_popup; //이미지에 마우스 올렸을때 띄울 팝업

    //[SerializeField]
   // private TMPro.TMP_Text Popup_Text;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item_popup.SetActive(true);
        Item_popup.GetComponent<Image>().rectTransform.position = eventData.position;
        //Popup_Text.enabled = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Item_popup.SetActive(false);
        //Popup_Text.enabled = false;

    }

    
}
