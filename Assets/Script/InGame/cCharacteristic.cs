using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Stats
{
    public float HP; // ü��

    public float ATK; // ���ݷ� 
    public float DEF; // ���� 
}

public interface BattleSystem
{
    void OnDamage(float damage);    
}

public struct ROTDATA
{
    // ȸ�� ������
    public float angle;
    public float rotDir;
}


public class cCharacteristic : MonoBehaviour
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get // ����� �� �ٷ� ����ǵ��� property ���
        {
            if (_anim == null) _anim = this.GetComponent<Animator>(); // _anim�� ȣ���ϸ� �׻� ���� ������ �ִٴ� Ȯ���� ���� 

            if (_anim == null) _anim = this.GetComponentInChildren<Animator>(); // �ڽĿ� �������� ȣ��

            return _anim;
        }
    }

    Rigidbody _rigid = null;
    protected Rigidbody myRigid
    {
        get // ����� �� �ٷ� ����ǵ��� property ���
        {
            if (_rigid == null) _rigid = this.GetComponent<Rigidbody>(); // _anim�� ȣ���ϸ� �׻� ���� ������ �ִٴ� Ȯ���� ���� 

            if (_rigid == null) _rigid = this.GetComponentInChildren<Rigidbody>(); // �ڽĿ� �������� ȣ��

            return _rigid;
        }
    }

    cAutoDetection _Detection = null;
    protected cAutoDetection myDetection
    {
        get // ����� �� �ٷ� ����ǵ��� property ���
        {
            if (_Detection == null) _Detection = this.GetComponent<cAutoDetection>(); // _anim�� ȣ���ϸ� �׻� ���� ������ �ִٴ� Ȯ���� ���� 

            if (_Detection == null) _Detection = this.GetComponentInChildren<cAutoDetection>(); // �ڽĿ� �������� ȣ��

            return _Detection;
        }
    }

    protected void CalculateAngle(Vector3 myForward, Vector3 myDir, Vector3 myRight, out ROTDATA myRotData)
    {
        myRotData = new ROTDATA();

        // ���� ���
        float rad = Mathf.Acos(Vector3.Dot(myForward, myDir)); // �̵��� ���������� ������ ����
        myRotData.angle = 180 * (rad / Mathf.PI); // degree ������ �ٲ�
        myRotData.rotDir = 1.0f; // ȸ�� ���Ⱚ => ������

        if (Vector3.Dot(myRight, myDir) < 0.0f)
        {
            myRotData.rotDir = -1.0f; // ���ʹ���
        }
    }

    protected IEnumerator LookingTarget(Transform myTrans, Vector3 myDir)
    {
        while (true)
        {
            CalculateAngle(myTrans.forward, myDir, myTrans.right, out ROTDATA myRotData); // ���� ��� -> �Ź� ���־�� ��

            if (!Mathf.Approximately(myRotData.angle, 0.0f))
            {
                float delta = 360.0f * Time.deltaTime;

                delta = delta > myRotData.angle ? myRotData.angle : delta;

                myTrans.Rotate(Vector3.up * delta * myRotData.rotDir);
            }

            yield return null;
        }
    }
}

