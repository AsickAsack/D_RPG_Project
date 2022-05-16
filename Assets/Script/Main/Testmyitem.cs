using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testmyitem : MonoBehaviour
{
    public List<itemstat> itemDB = new List<itemstat>();
    public List<itemstat> inventory = new List<itemstat>();

    private void Start()
    {


        itemDB[0] = new itemstat
        {
            atk = 1,
            name = "È¦¸®½²",
            upgrade = 0
            
        };

        inventory.Add(itemDB[0]);
        inventory.Add(itemDB[0]);

        inventory[0].atk += 1;
        inventory[1].atk -= 1;

        Debug.Log(inventory[0].atk);
        Debug.Log(inventory[1].atk);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}


[System.Serializable]
public class itemstat
{
    public string name;
    public int atk;
    public int upgrade;


}