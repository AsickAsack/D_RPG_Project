using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraWalk : MonoBehaviour
{
    public float NoTouchTime = 0.0f;
    Vector3 OriginPos = Vector3.zero;
    public Transform[] CameraMovePos;
    Quaternion OriginRot = Quaternion.identity;  
    public GameObject Player;
    public int CameraMoveindex = 0;
    public float RotSpeed=10.0f;
    public Transform pivot;
    public SpringArm springarm;

    private void Awake()
    {
        OriginPos = Camera.main.transform.localPosition;
        OriginRot = Camera.main.transform.localRotation;
  
    }
    
    void Update()
    {

        if (NoTouchTime < 10.0f)
        {
            NoTouchTime += Time.deltaTime;

            if (NoTouchTime >= 5.0f)
            {
                NoTouchTime = 10.0f;
                springarm.Rot = Vector3.zero;
                pivot.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
        }


        if (NoTouchTime >=5.0f)
        { 
            switch (CameraMoveindex)
            {
                case 0:
                    {
                        Camera.main.transform.RotateAround(Player.transform.localPosition, Vector3.right, Time.deltaTime * RotSpeed);
                        if(Camera.main.transform.localRotation.eulerAngles.x > 42.0f)
                        {
                            Camera.main.transform.localPosition = CameraMovePos[0].localPosition;
                            Camera.main.transform.localRotation = CameraMovePos[0].localRotation;
                            CameraMoveindex++;
                        }
                    }
                    break;
                case 1:
                    {
                        Camera.main.transform.RotateAround(Player.transform.localPosition, Vector3.down, Time.deltaTime * RotSpeed);
                        if(Camera.main.transform.localRotation.eulerAngles.y < 139.0f)
                        {
                            Camera.main.transform.localPosition = CameraMovePos[1].localPosition;
                            Camera.main.transform.localRotation = CameraMovePos[1].localRotation;
                            CameraMoveindex++;
                        }
                    }
                    break;
                case 2:
                    {
                        Camera.main.transform.RotateAround(Player.transform.localPosition, -Vector3.right, Time.deltaTime * RotSpeed);
                        if (Camera.main.transform.localRotation.eulerAngles.x < 0.5f)
                        {
                            Camera.main.transform.localPosition = CameraMovePos[2].localPosition;
                            Camera.main.transform.localRotation = CameraMovePos[2].localRotation;
                            CameraMoveindex++;
                        }
                    }
                    break;
                case 3:
                    {
                        Camera.main.transform.RotateAround(Player.transform.localPosition, -Vector3.down, Time.deltaTime * RotSpeed);
                        if (Camera.main.transform.localRotation.eulerAngles.y > 234.0f)
                        {
                            Camera.main.transform.localPosition = OriginPos;
                            Camera.main.transform.localRotation = OriginRot;
                            CameraMoveindex = 0;
                        }
                    }
                    break;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            Camera.main.transform.localPosition = OriginPos;
            Camera.main.transform.localRotation = OriginRot;
            springarm.Rot = Vector3.zero;
            pivot.transform.localRotation = Quaternion.Euler(0, 0, 0);
            NoTouchTime = 0.0f;
            
        }

    }
}
