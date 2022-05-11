using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testData : MonoBehaviour
{
    public class playerdata1
    { 
    public int Upgrade;
    public string ItemName;
    }

    playerdata1[] Playerdata1 = new playerdata1[4];
    public List<playerdata1> Inventory = new List<playerdata1>();

    private void Start()
    {

        Playerdata1[0] = new playerdata1
        {
            ItemName = "메리",
            Upgrade = 0
        };
        Playerdata1[1] = new playerdata1
        {
            ItemName = "시리",
            Upgrade = 0
        };



        Inventory.Add(Playerdata1[0]);
        Inventory.Add(Playerdata1[0]);

        

        Inventory[1].Upgrade += 1;

        Debug.Log(Inventory[0].Upgrade);
        Debug.Log(Inventory[1].Upgrade);
    }

}
