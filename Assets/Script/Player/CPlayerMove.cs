using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CPlayerMove : cCharacter
{    
    public FloatingJoystick joystick;
    Vector3 moveVec;

    Vector2 joystickPos;
    public float MoveSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // ���̽�ƽ �Է°�
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        // �̵�
        moveVec = new Vector3(x, 0, z) * MoveSpeed * Time.deltaTime;
        myRigid.MovePosition(myRigid.position + moveVec);

        // �Է��� ������� ȸ��x
        if (moveVec.sqrMagnitude == 0) return; // sqrMganitude : ������ ����ũ�� ��ȯ

        // ȸ��
        Quaternion dirQuat = Quaternion.LookRotation(moveVec); // ȸ���ؾ��ϴ� ���� ����
        Quaternion moveQuat = Quaternion.Slerp(myRigid.rotation, dirQuat, 0.3f); // ���� ȸ������ �ٲ� ȸ������ ����
        myRigid.MoveRotation(moveQuat);

    }

    private void LateUpdate()
    {
        myAnim.SetFloat("x", joystick.Horizontal);
        myAnim.SetFloat("y", joystick.Vertical);
    }
}
