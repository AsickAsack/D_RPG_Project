using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI description; 
    
    public void SetupTooltip(string name, string des)
    {
        itemname.text = name;
        description.text = des;
    }

    private void Update()
    {
       // transform.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;  

        transform.position = Input.mousePosition;
    }
}
