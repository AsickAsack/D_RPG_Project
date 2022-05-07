using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyRotate : MonoBehaviour
{

    public RectTransform Select_Light;
 
    void Update()
    {

        Select_Light.Rotate(-Vector3.forward * Time.deltaTime * 50.0f);

    }
}
