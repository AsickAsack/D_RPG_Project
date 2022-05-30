using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleSystem
{
    void OnDamage(float damage);    
}

public struct ROTDATA
{
    // 회전 데이터
    public float angle;
    public float rotDir;
}


public class cCharacteristic : MonoBehaviour
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get // 사용할 때 바로 적용되도록 property 사용
        {
            if (_anim == null) _anim = this.GetComponent<Animator>(); // _anim을 호출하면 항상 값을 가지고 있다는 확신이 생김 

            if (_anim == null) _anim = this.GetComponentInChildren<Animator>(); // 자식에 있을때도 호출

            return _anim;
        }
    }

    Rigidbody _rigid = null;
    protected Rigidbody myRigid
    {
        get // 사용할 때 바로 적용되도록 property 사용
        {
            if (_rigid == null) _rigid = this.GetComponent<Rigidbody>(); // _anim을 호출하면 항상 값을 가지고 있다는 확신이 생김 

            if (_rigid == null) _rigid = this.GetComponentInChildren<Rigidbody>(); // 자식에 있을때도 호출

            return _rigid;
        }
    }

    cAutoDetection _Detection = null;
    protected cAutoDetection myDetection
    {
        get // 사용할 때 바로 적용되도록 property 사용
        {
            if (_Detection == null) _Detection = this.GetComponent<cAutoDetection>(); // _anim을 호출하면 항상 값을 가지고 있다는 확신이 생김 

            if (_Detection == null) _Detection = this.GetComponentInChildren<cAutoDetection>(); // 자식에 있을때도 호출

            return _Detection;
        }
    }

    public static void CalculateAngle(Vector3 myForward, Vector3 myDir, Vector3 myRight, out ROTDATA myRotData)
    {
        myRotData = new ROTDATA();

        // 각도 계산
        float rad = Mathf.Acos(Vector3.Dot(myForward, myDir)); // 이동할 지점까지의 각도를 구함
        myRotData.angle = 180 * (rad / Mathf.PI); // degree 각도로 바꿈
        myRotData.rotDir = 1.0f; // 회전 방향값 => 오른쪽

        if (Vector3.Dot(myRight, myDir) < 0.0f)
        {
            myRotData.rotDir = -1.0f; // 왼쪽방향
        }
    }
}


