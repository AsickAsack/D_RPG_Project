using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatePanel : MonoBehaviour, IDragHandler
{
    Vector3 PivotRot = Vector3.zero;
    public Vector2 VerticalRotRange = new Vector2(-50, 50);
    public float RotSpeed = 1.0f;
    public float SmoothRotSpeed = 8.0f;
    public Transform Pivot;


    void Start()
    {
        PivotRot = this.transform.localRotation.eulerAngles;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
            PivotRot.y += eventData.scrollDelta.x * RotSpeed;
            PivotRot.x += eventData.scrollDelta.y * RotSpeed;
            PivotRot.x = Mathf.Clamp(PivotRot.x, VerticalRotRange.x, VerticalRotRange.y);
        

        Pivot.transform.localRotation = Quaternion.Slerp(Pivot.transform.localRotation, Quaternion.Euler(PivotRot), Time.deltaTime * SmoothRotSpeed);
    }

    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
