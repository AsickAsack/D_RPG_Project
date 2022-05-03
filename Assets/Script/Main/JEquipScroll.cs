using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class JEquipScroll : MonoBehaviour
{
    public jEquipPanelsetting Equip_Panel;
    ScrollRect myScroll;
    [SerializeField]
    List<jEquipPanelsetting> itemlist = new List<jEquipPanelsetting>();
    List<int> inventorylist = new List<int>();
    float itemheight =191.2f;
    

    public void Scrolling()
    {
        myScroll = GetComponent<ScrollRect>();
        itemlist.Clear();
        inventorylist.Clear();
        Equip_Panel.num = 0;

        for (int i = 0; i < GameData.Instance.playerdata.Player_inventory.Count; i++)
        {
            inventorylist.Add(i);
        }
        CreateItem();
        SetContentHeight();
        StartCoroutine(Updater());
    }

    public void Stop_Scroll()
    {
        StopCoroutine(Updater());
    }




    private void CreateItem()
    {
        RectTransform scrollect = myScroll.GetComponent<RectTransform>();
        int itemCount = (int)(scrollect.rect.height / itemheight) + 1 + 2;


        for (int i = 0; i < (inventorylist.Count < 8  ? inventorylist.Count : 8); i++)
        {
            jEquipPanelsetting item = Instantiate(Equip_Panel, myScroll.content);
            itemlist.Add(item);
            item.setItem(i);

        }


    }

    private void SetContentHeight()
    {
        if(inventorylist.Count % 2 == 0)
       myScroll.content.sizeDelta = new Vector2(myScroll.content.sizeDelta.x, inventorylist.Count * itemheight*0.5f);
        else
       myScroll.content.sizeDelta = new Vector2(myScroll.content.sizeDelta.x, inventorylist.Count * itemheight * 0.5f + itemheight*0.5f);
    }

    private bool RelocationItem(jEquipPanelsetting item,float ContentY)
    {
        RectTransform scrollect = myScroll.GetComponent<RectTransform>();

        if (item.transform.localPosition.y + (ContentY) > itemheight)
        {
            item.transform.localPosition -= new Vector3(0, itemlist.Count * itemheight * 0.5f);
            if(inventorylist.Count>item.num)
            {
               
                item.setItem(item.num + 8);
            }
            return true;
        }
        else if (item.transform.localPosition.y + (ContentY) < -scrollect.rect.height - itemheight )
        {
            item.transform.localPosition += new Vector3(0, itemlist.Count * itemheight * 0.5f);
            if (8 <= item.num)
            {
                item.setItem(item.num - 8); 
            }

            return true;
        }

        return false;
        
    }




    // Update is called once per frame
    IEnumerator Updater()
    {
        while(true)
        { 
            float ContentY = myScroll.content.anchoredPosition.y;

            foreach (jEquipPanelsetting data in itemlist)
            {
                RelocationItem(data, ContentY);
                //bool isChanged = 
                //if(isChanged)
                //{
                
                //}
            }

            yield return null;
        }
    }
}

