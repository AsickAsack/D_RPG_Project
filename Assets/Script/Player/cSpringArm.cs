using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSpringArm : MonoBehaviour
{
    public FloatingJoystick joystick;

    Vector3 TargetRot;

    public float SmoothRotSpeed = 5.0f;
    public float HorizontalRotSpeed = 10.0f; // ���� �̵��ӵ�

    public bool RootRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        TargetRot = this.transform.rotation.eulerAngles; // �ʱ� ȸ����ġ ����   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TargetRot.y += joystick.Horizontal * HorizontalRotSpeed; // �¿�ȸ��
            if (RootRotate)
            {
                // �θ�(Player)�� ȸ��
                this.transform.parent.rotation = Quaternion.Slerp(this.transform.parent.rotation, 
                    Quaternion.Euler(0.0f, TargetRot.y, 0.0f), Time.deltaTime * SmoothRotSpeed);                
            }
            else
            {
                // SpringArm�� ���� ȸ��
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                    Quaternion.Euler(TargetRot), Time.deltaTime * SmoothRotSpeed); 
            }
        }
    }
}
