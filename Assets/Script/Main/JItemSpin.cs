using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JItemSpin : MonoBehaviour, IDragHandler
{

    public float ItemRotSpeed = 100.0f;

    public void OnDrag(PointerEventData eventData)
    {
     
        float z = eventData.delta.x * Time.deltaTime * ItemRotSpeed;
        transform.Rotate(0, 0, z);
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
        
    //    float x = eventData.delta.x * Time.deltaTime * ItemRotSpeed;
    //    transform.Rotate(0, -x, 0);
    //}
}
