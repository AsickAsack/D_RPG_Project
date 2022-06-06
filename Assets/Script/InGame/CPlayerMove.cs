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

    public bool isMove = false;

    public void OnPlayerMoveControl()
    {
        JoystickMove();
        myAnim.SetFloat("x", joystick.Horizontal);
        myAnim.SetFloat("y", joystick.Vertical);

        
    }
    
    void JoystickMove()
    {
        // 조이스틱 입력값
        Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        isMove = moveInput.magnitude != 0; // 입력이 들어왔는지를 판단

        if (isMove)
        {
            Vector3 lookForward = new Vector3(myCam.forward.x, 0.0f, myCam.forward.z).normalized; // 현재 카메라가 바라보는 방향(상-하)
            Vector3 lookRight = new Vector3(myCam.right.x, 0.0f, myCam.right.z).normalized; // 현재 카메라가 바라보는 방향(좌-우)
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // 현재 카메라가 바라보는 방향

            // 카메라가 바라보는 방향으로 이동하도록 설정
            if (moveDir != Vector3.zero)
            {
                myCharacter.forward = moveDir;
                moveVec = moveDir * MoveSpeed * Time.deltaTime;
                myRigid.MovePosition(myRigid.position + moveVec);
                this.transform.Translate(moveVec, Space.World);
            }
        }

        // 입력이 없을경우 회전x
        if (moveVec.sqrMagnitude == 0) return; // sqrMganitude : 벡터의 제곱크기 반환

        // 회전
        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec); // 회전해야하는 값을 저장
            Quaternion moveQuat = Quaternion.Slerp(myRigid.rotation, dirQuat, SmoothRotSpeed); // 현재 회전값과 바뀔 회전값을 보간
            myRigid.MoveRotation(moveQuat);
        }
    }

    public void Roll()
    {
        if (!myAnim.GetBool("IsDoing") && this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY) // 스킬이나 공격 구르기 중에 구르기x
        {
            myAnim.SetTrigger("Roll");
        }
    }
}
