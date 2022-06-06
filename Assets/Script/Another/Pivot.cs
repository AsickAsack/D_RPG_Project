using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Pivot : MonoBehaviour
{

    Vector3 PivotRot = Vector3.zero;
    public Vector2 VerticalRotRange = new Vector2(-50,50);
    public float RotSpeed = 0.5f;
    public float SmoothRotSpeed = 8.0f;
    public Transform myCam;
    float ScreenSize;
    int rightFingerID = -1;
    int LeftFingerID = -1;
    public bool isRotate = false;

    void Start()
    {
        ScreenSize = Screen.width / 2f; 
    }
    void Update()
    {
        for(int i=0; i<Input.touchCount;i++)
        {
            Touch t = Input.GetTouch(i);

            switch(t.phase)
            {
                case TouchPhase.Began:
                    if(t.position.x > ScreenSize && rightFingerID == -1)
                    {
                        rightFingerID = t.fingerId;
                     
                    }
                    if (t.position.x < ScreenSize && LeftFingerID == -1)
                    {
                        LeftFingerID = t.fingerId;
                        
                    }
                    break;
                case TouchPhase.Moved:
                    
                    if (rightFingerID == t.fingerId)
                    {
                        isRotate = true;
                        PivotRot.y += t.deltaPosition.x * RotSpeed;
                        PivotRot.x += t.deltaPosition.y * RotSpeed;
                        PivotRot.x = Mathf.Clamp(PivotRot.x, VerticalRotRange.x, VerticalRotRange.y);


                        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(PivotRot), Time.deltaTime * SmoothRotSpeed);
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if(rightFingerID == t.fingerId)
                    {
                        isRotate = false;
                        rightFingerID = -1;
                        
                    }
                    else if (LeftFingerID == t.fingerId)
                    {
                        LeftFingerID = -1;
                        
                    }
                    
                    
                    break;

            }
        }

        
        /*
        if (Input.GetMouseButton(0))
        {
            PivotRot.y += Input.GetAxis("Mouse X")  * RotSpeed;
            PivotRot.x += Input.GetAxis("Mouse Y")  * RotSpeed;
            PivotRot.x = Mathf.Clamp(PivotRot.x, VerticalRotRange.x, VerticalRotRange.y);
        }

        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(PivotRot), Time.deltaTime * SmoothRotSpeed);
        */
    }
}
