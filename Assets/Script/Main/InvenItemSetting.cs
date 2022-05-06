using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenItemSetting : MonoBehaviour
{
    public GameObject BigItem;
    public int num = 0;

   public void setItemNum(int index)
    {
       
        num = index;

    }

    public void setItemdetail(int index)
    {
        BigItem.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Player_inventory2[index].ItemName;
        //BigItem.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Player_inventory2[index].ItemName; 보유개수
        BigItem.transform.GetChild(2).GetComponent<Image>().sprite = GameData.Instance.playerdata.Player_inventory2[index].Mysprite;
        BigItem.transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Player_inventory2[index].Description;
    }

}
