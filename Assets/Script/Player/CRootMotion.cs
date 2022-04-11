using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRootMotion : MonoBehaviour
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

    public Transform myRoot;
    public float MoveSpeed = 1.0f; // �÷��̾� �̵� �ӵ�

    private void OnAnimatorMove()
    {
        myRoot.GetComponent<Rigidbody>().MovePosition(myRoot.position + myAnim.deltaPosition * MoveSpeed); // �������� �̵��� �ϰ� ����
    }
}
