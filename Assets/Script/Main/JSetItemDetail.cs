using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JSetItemDetail : MonoBehaviour
{
    public Canvas Detail_Canvas;
    public Canvas Detail_objectCanvas;
    public int num = 0;
  

    public void number(int index)
    {
        
        num = index;
    }



    public void setItem(int index)
    {
        

        switch(GameData.Instance.playerdata.Player_inventory[index].itemType)
        {
            case ItemType.Weapon:
                Detail_Canvas.transform.GetChild(0).GetComponent<Text>().text = "주무기";
                Detail_Canvas.transform.GetChild(4).GetComponent<TMPro.TMP_Text>().text =
                    "<color=grey>공격력</color>  "+GameData.Instance.playerdata.Player_inventory[index].ATK+"    "+
                    "<color=grey>크리티컬</color>  " + GameData.Instance.playerdata.Player_inventory[index].Critical;
                break;
            case ItemType.Armor:
                Detail_Canvas.transform.GetChild(0).GetComponent<Text>().text = "방어구";
                Detail_Canvas.transform.GetChild(4).GetComponent<TMPro.TMP_Text>().text =
                    "<color=grey>방어력</color>  "+GameData.Instance.playerdata.Player_inventory[index].DEF+ "    " +
                    "<color=grey>체력</color>  " + GameData.Instance.playerdata.Player_inventory[index].HP;
                break;
        }
        if(GameData.Instance.playerdata.Player_inventory[index].Upgrade == 0)
        Detail_Canvas.transform.GetChild(1).GetComponent<Text>().text = GameData.Instance.playerdata.Player_inventory[index].ItemName;
        else
        Detail_Canvas.transform.GetChild(1).GetComponent<Text>().text = GameData.Instance.playerdata.Player_inventory[index].ItemName+ " +"+ GameData.Instance.playerdata.Player_inventory[index].Upgrade;

        switch (GameData.Instance.playerdata.Player_inventory[index].ItemCode)
        {
            case 000:
                Detail_objectCanvas.transform.GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(true);
                break;
            case 001:
                Detail_objectCanvas.transform.GetChild(2).GetComponent<RectTransform>().gameObject.SetActive(true);
                break;
            case 002:
                Detail_objectCanvas.transform.GetChild(3).GetComponent<RectTransform>().gameObject.SetActive(true);
                break;
            case 003:
                Detail_objectCanvas.transform.GetChild(4).GetComponent<RectTransform>().gameObject.SetActive(true);
                break;
        }

        Detail_Canvas.transform.GetChild(11).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Player_inventory[index].Description;

    }

}
