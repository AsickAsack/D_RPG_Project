using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class jEquipPanelsetting : JPopUpCanvas
{
    public TMPro.TMP_Text Item_name;
    public TMPro.TMP_Text Item_option;
    public Image BackGround;
    public Image Icon;
    public GameObject Eqquied;
    public int num=0;

    public void setItem(int index)
    {
        switch(GameData.Instance.playerdata.Player_inventory[index].grade)
        {
            case Grade.common:
                {
                    Item_name.text = GameData.Instance.playerdata.Player_inventory[index].ItemName;
                }
                break;
            case Grade.rare:
                {
                    Item_name.text = "<color=green>"+GameData.Instance.playerdata.Player_inventory[index].ItemName+"</color>";
                    BackGround.color = Color.green;
                }
                break;
            case Grade.epic:
                {
                    Item_name.text = "<color=blue>" + GameData.Instance.playerdata.Player_inventory[index].ItemName + "</color>";
                    BackGround.color = Color.blue;
                }
                break;
            case Grade.legendary:
                {
                    Item_name.text = "<color=yellow>" + GameData.Instance.playerdata.Player_inventory[index].ItemName + "</color>";
                    BackGround.color = Color.yellow;
                }
                break;
        }

        Icon.sprite = GameData.Instance.playerdata.Player_inventory[index].Mysprite;


        if (GameData.Instance.playerdata.Player_inventory[index].Equipped)
            Eqquied.SetActive(true);

        num = index;
    }

  
}

