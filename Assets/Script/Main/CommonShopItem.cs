using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonShopItem : MonoBehaviour
{
    public int item_index = 0;
    public int item_Type = 0;
    public Image icon;
    public TMPro.TMP_Text itemName;

    public void Setitem()
    {    
        item_index = Random.Range(0, 5);

        icon.sprite = GameData.Instance.mySprite[GameData.Instance.playerdata.Itemdata2[item_index].Mysprite];
        itemName.text = GameData.Instance.playerdata.Itemdata2[item_index].ItemName;
        this.transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text = GameData.Instance.playerdata.Itemdata2[item_index].Price.ToString("N0");

    }



}
