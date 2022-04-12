using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSpringArm : MonoBehaviour
{
    public FloatingJoystick joystick;
    public Transform myCam;

    Vector3 TargetRot;

    public float SmoothRotSpeed = 5.0f; 
    public float HorizontalRotSpeed = 2.0f; // ���� �̵��ӵ�

    public Vector2 VerticalRotRange; // ���� ȸ�� ���� 
    public LayerMask CrashMask; // ī�޶� �ٴ� �浹 layerMask
    public float VerticalRotSpeed = 2.0f; // ���� �̵��ӵ�
    public float CollsionOffset = 0.5f; // ī�޶� �ٴڿ� ���ִ� ����

    float ZoomDist = 0.0f;
    float InitialZoomDist = 0.0f;
    public float SmoothZoomSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        TargetRot = this.transform.rotation.eulerAngles; // �ʱ� ȸ����ġ ����   
        InitialZoomDist = -myCam.localPosition.z; // �ʱ� ī�޶�� �÷��̾��� �Ÿ� ����
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        TargetRot.y += joystick.Horizontal * HorizontalRotSpeed; // �¿�ȸ��
        TargetRot.x -= joystick.Vertical * VerticalRotSpeed; // ����ȸ��

        // ���� ���� ����
        if (TargetRot.x > 180.0f)
        {
            TargetRot.x -= 360.0f;
        }
        TargetRot.x = Mathf.Clamp(TargetRot.x, VerticalRotRange.x, VerticalRotRange.y);

        // SpringArm�� ���� ȸ��
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.Euler(TargetRot), Time.deltaTime * SmoothRotSpeed);

        // ī�޶� �Ʒ��� ���� ������ ���� �΋H���� zoom�� ��ó�� ���̰� ��
        Ray ray = new Ray(this.transform.position, -this.transform.forward);

        myCam.Translate(Vector3.forward * ZoomDist);
        ZoomDist = Mathf.Lerp(ZoomDist, InitialZoomDist, Time.deltaTime * SmoothZoomSpeed);

        if (Physics.Raycast(ray, out RaycastHit hit, CollsionOffset + ZoomDist, CrashMask))
        {
            ZoomDist = Vector3.Distance(hit.point - ray.direction * CollsionOffset, this.transform.position); // �浹�� �� �������� ī�޶� �Ű���
        }

        myCam.localPosition = -Vector3.forward * ZoomDist; // ī�޶� ������ �� ��Ŵ
    }
}
