using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JSetItemDetail : MonoBehaviour
{
    public Canvas Detail_Canvas;
    public JPopUpCanvas uimanager;
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
                Detail_Canvas.transform.GetChild(2).GetComponent<Text>().text = "주무기";
                Detail_Canvas.transform.GetChild(6).GetComponent<TMPro.TMP_Text>().text =
                    "<color=grey>공격력</color>  "+GameData.Instance.playerdata.Player_inventory[index].ATK+"    "+
                    "<color=grey>크리티컬</color>  " + GameData.Instance.playerdata.Player_inventory[index].Critical;
                break;
            case ItemType.Armor:
                Detail_Canvas.transform.GetChild(2).GetComponent<Text>().text = "방어구";
                Detail_Canvas.transform.GetChild(6).GetComponent<TMPro.TMP_Text>().text =
                    "<color=grey>방어력</color>  "+GameData.Instance.playerdata.Player_inventory[index].DEF+ "    " +
                    "<color=grey>체력</color>  " + GameData.Instance.playerdata.Player_inventory[index].HP;
                break;
        }
        if(GameData.Instance.playerdata.Player_inventory[index].Upgrade == 0)
        Detail_Canvas.transform.GetChild(3).GetComponent<Text>().text = GameData.Instance.playerdata.Player_inventory[index].ItemName;
        else
        Detail_Canvas.transform.GetChild(3).GetComponent<Text>().text = GameData.Instance.playerdata.Player_inventory[index].ItemName+ " +"+ GameData.Instance.playerdata.Player_inventory[index].Upgrade;

        switch (GameData.Instance.playerdata.Player_inventory[index].ItemCode)
        {
            case 000:
                {
                    uimanager.EquipObject[0].gameObject.SetActive(true);
                    uimanager.EquipOrgPos = uimanager.EquipObject[0].transform.localRotation.eulerAngles;
                    uimanager.EquipIndex = 0;
                }
                break;
            case 001:
                {
                    uimanager.EquipObject[1].gameObject.SetActive(true);
                    uimanager.EquipOrgPos = uimanager.EquipObject[1].transform.localRotation.eulerAngles;
                    uimanager.EquipIndex = 1;
                }
                break;
            case 002:
                {
                    uimanager.EquipObject[2].gameObject.SetActive(true);
                    uimanager.EquipOrgPos = uimanager.EquipObject[2].transform.localRotation.eulerAngles;
                    uimanager.EquipIndex = 2;
                }
                break;
            case 003:
                {
                    uimanager.EquipObject[3].gameObject.SetActive(true);
                    uimanager.EquipOrgPos = uimanager.EquipObject[3].transform.localRotation.eulerAngles;
                    uimanager.EquipIndex = 3;
                }
                break;

        }

        Detail_Canvas.transform.GetChild(13).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Player_inventory[index].Description;

    }

}
