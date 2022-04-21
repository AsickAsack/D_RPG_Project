using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMain : MonoBehaviour
{
    public GameObject popupprefab;

    public void CreatName()
    {
        GameObject obj=Instantiate(popupprefab);
        obj.transform.Find("bg").gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
