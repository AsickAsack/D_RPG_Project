using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
