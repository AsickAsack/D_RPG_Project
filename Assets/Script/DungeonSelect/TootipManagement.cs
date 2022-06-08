using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class TootipManagement : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    public PitemData itemdata;
    bool clear = false;
    public ItemTooltip tooltip;
    public Image img;
    public TextMeshProUGUI itemvalue;



    private void Awake()

    {

        string name = img.sprite.name;

        clear = GameData.Instance.playerdata.desertclear;
        if (clear == true)
        {

            switch (name)
            {
                case "belts":
                    Destroy(this.gameObject);
                    break;
                case "ui_icon_01_29":
                    string b = itemvalue.text;
                    int a = int.Parse(b) / 2;
                    itemvalue.text = a.ToString();

                    tooltip.SetupTooltip(itemdata.itemDB[0].itemname, itemdata.itemDB[0].des);
                    break;
                case "ingots":
                    tooltip.SetupTooltip(GameData.Instance.playerdata.Itemdata2[3].ItemName, GameData.Instance.playerdata.Itemdata2[3].Description);
                    break;
                case "BunnyGauntlet":
                    Destroy(this.gameObject);
                    break;
                case "012":
                    tooltip.SetupTooltip(itemdata.itemDB[1].itemname, itemdata.itemDB[1].des);
                    break;


            }


        }


        else
        {
            switch (name)
            {
                case "belts":
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
        }


    }




    public void OnPointerEnter(PointerEventData eventData)
    {
        string name = img.sprite.name;

        clear = GameData.Instance.playerdata.desertclear;
        if (clear == true)
        {

            switch (name)
            {
                case "belts":
                    Destroy(this.gameObject);
                    break;
                case "ui_icon_01_29":
                    string b = itemvalue.text;
                    int a = int.Parse(b) / 2;
                    itemvalue.text = a.ToString();

                    tooltip.SetupTooltip(itemdata.itemDB[0].itemname, itemdata.itemDB[0].des);
                    break;
                case "ingots":
                    tooltip.SetupTooltip(GameData.Instance.playerdata.Itemdata2[3].ItemName, GameData.Instance.playerdata.Itemdata2[3].Description);
                    break;
                case "BunnyGauntlet":
                    Destroy(this.gameObject);
                    break;
                case "012":
                    tooltip.SetupTooltip(itemdata.itemDB[1].itemname, itemdata.itemDB[1].des);
                    break;


            }


        }


        else
        {
            switch (name)
            {
                case "belts":
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
        }

        tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
    

}
