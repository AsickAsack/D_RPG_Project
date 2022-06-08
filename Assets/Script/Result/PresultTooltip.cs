using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PresultTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI description;

    public void SetupTooltip(string name, string des)
    {
        itemname.text = name;
        description.text = des;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
