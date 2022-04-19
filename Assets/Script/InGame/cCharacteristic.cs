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
}


