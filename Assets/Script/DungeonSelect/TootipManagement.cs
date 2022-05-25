using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TootipManagement : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    public PitemData itemdata;
    
    public ItemTooltip tooltip;
    public Image img;
    public void OnPointerEnter(PointerEventData eventData)
    {
        string name = img.sprite.name;
       switch(name)
        {
            case  "desertov":
                tooltip.SetupTooltip(itemdata.itemDB[0].itemname, itemdata.itemDB[0].des);
                break;
            case "ui_icon_01_29":
                tooltip.SetupTooltip(itemdata.itemDB[1].itemname, itemdata.itemDB[1].des);
                break;
            case "012":
                tooltip.SetupTooltip(itemdata.itemDB[2].itemname, itemdata.itemDB[2].des);
                break;
            case "013":
                tooltip.SetupTooltip(itemdata.itemDB[3].itemname, itemdata.itemDB[3].des);
                break;
            case "014":
                tooltip.SetupTooltip(itemdata.itemDB[4].itemname, itemdata.itemDB[4].des);
                break;
          
        
        }
        tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
    

}
