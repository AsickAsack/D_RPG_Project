using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackGround : MonoBehaviour
{
    int a;

    private void Awake()
    {
        a = Random.Range(0,4);

        this.transform.GetChild(a).GetComponent<UnityEngine.UI.Image>().enabled = true;
    }

}
