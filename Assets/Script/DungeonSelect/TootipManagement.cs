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
            case  "belts":
                tooltip.SetupTooltip(GameData.Instance.playerdata.Itemdata2[4].ItemName, GameData.Instance.playerdata.Itemdata2[4].Description);
                break;
            case "ui_icon_01_29":
                tooltip.SetupTooltip(itemdata.itemDB[0].itemname, itemdata.itemDB[0].des);
                break;
            case "ingots":
                tooltip.SetupTooltip(GameData.Instance.playerdata.Itemdata2[3].ItemName, GameData.Instance.playerdata.Itemdata2[3].Description);
                break;
            case "BunnyGauntlet":
                tooltip.SetupTooltip(GameData.Instance.playerdata.Itemdata[1].ItemName, GameData.Instance.playerdata.Itemdata[1].Description);
                break;
            case "012":
                tooltip.SetupTooltip(itemdata.itemDB[1].itemname, itemdata.itemDB[1].des);
                break;
          
        
        }
        tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
    

}
