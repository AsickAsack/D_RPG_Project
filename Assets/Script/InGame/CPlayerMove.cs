using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CPlayerMove : cCharacteristic
{
    public FloatingJoystick joystick;
    public Transform myCharacter;
    public Transform myCam;

    Vector3 moveVec;

    public float MoveSpeed = 5.0f;
    public float SmoothRotSpeed = 360.0f;

    // Update is called once per frame
    void Update()
    {
        JoystickMove();
    }

    private void LateUpdate()
    {
        myAnim.SetFloat("x", joystick.Horizontal);
        myAnim.SetFloat("y", joystick.Vertical);
    }

    void JoystickMove()
    {
        // ���̽�ƽ �Է°�
        Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        bool isMove = moveInput.magnitude != 0; // �Է��� ���Դ����� �Ǵ�

        if (isMove)
        {
            Vector3 lookForward = new Vector3(myCam.forward.x, 0.0f, myCam.forward.z).normalized; // ���� ī�޶� �ٶ󺸴� ����(��-��)
            Vector3 lookRight = new Vector3(myCam.right.x, 0.0f, myCam.right.z).normalized; // ���� ī�޶� �ٶ󺸴� ����(��-��)
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // ���� ī�޶� �ٶ󺸴� ����

            // ī�޶� �ٶ󺸴� �������� �̵��ϵ��� ����
            if (moveDir != Vector3.zero)
            {
                myCharacter.forward = moveDir;
                moveVec = moveDir * MoveSpeed * Time.deltaTime;
                myRigid.MovePosition(myRigid.position + moveVec);
                this.transform.Translate(moveVec, Space.World);
            }
        }

        // �Է��� ������� ȸ��x
        if (moveVec.sqrMagnitude == 0) return; // sqrMganitude : ������ ����ũ�� ��ȯ

        // ȸ��
        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec); // ȸ���ؾ��ϴ� ���� ����
            Quaternion moveQuat = Quaternion.Slerp(myRigid.rotation, dirQuat, SmoothRotSpeed); // ���� ȸ������ �ٲ� ȸ������ ����
            myRigid.MoveRotation(moveQuat);
        }
    }

    public void Roll()
    {
        if (!myAnim.GetBool("IsDoing")) // ��ų�̳� ���� ������ �߿� ������x
        {
            myAnim.SetTrigger("Roll");
        }
    }
}
