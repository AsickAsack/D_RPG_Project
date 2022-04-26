using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttackManager : cCharacteristic
{
    public LayerMask AttackMask;
    public Transform[] myAttackPoints; // Ÿ������
    public Collider[] colPoints; // �΋H�� ����

    public int Combo = 0;
    public float ComboLimitTime = 1.5f;

    float playTime = 0.0f;
    float curTime = 0.0f;   

    private void Update()
    {
        playTime += Time.deltaTime;
    }

    void OnAttack()
    {
        // �΋H�� ������ �������� �� �ݶ��̴��� �΋H�� �ݶ��̴����� ����
        foreach (Transform trans in myAttackPoints)
        {
            colPoints = Physics.OverlapSphere(trans.position, 1.0f, AttackMask);
        }

        //Collider[] list = Physics.OverlapSphere(myAttackPoints[0].position, 1.0f, AttackMask); 

        foreach (Collider col in colPoints)
        {
            BattleSystem bs = col.gameObject.GetComponent<BattleSystem>();

            if (bs != null)
            {
                bs.OnDamage(35.0f);
            }
        }
    }

    public void BasicAttack()
    {
        float ComboDurationTime = playTime - curTime; // �޺����ӽð�
        curTime = playTime;

        if (ComboDurationTime > ComboLimitTime)
        {
            Combo = 0; // �޺� �ʱ�ȭ

            // ó�� �޺� ���
            if (!myAnim.GetBool("IsDoing")) // ��ų�̳� ���� ������ �߿� ����x
            {
                myAnim.SetFloat("Combo", Combo);
                myAnim.SetTrigger("Attack");
                Combo = (Combo++) % 3;
                OnAttack();
            }

            // �ð� �ʱ�ȭ
            playTime = 0.0f; 
            curTime = 0.0f;
        }
        else
        {
            // ���� �޺� ���
            if (!myAnim.GetBool("IsDoing")) // ��ų�̳� ���� ������ �߿� ����x
            {
                myAnim.SetFloat("Combo", Combo);
                myAnim.SetTrigger("Attack");
                Combo = (Combo + 1) % 3;
                OnAttack();
            }
        }        
    }

    //IEnumerator ComboReset()
    //{
    //    while (isCombo)
    //    {
    //        if (!isCombo) break; // ���ݹ�ư�� ������ �޺� ��� ����

    //        // ���� �ð� �ȿ� �޺��� ���ŵ��� ������ �޺� ����
    //        yield return new WaitForSeconds(1.5f);
    //        Combo = 0;
    //        isCombo = false;
    //    }
    //}

    public void Skill_1()
    {
        if (!myAnim.GetBool("IsDoing")) // ��ų�̳� �����̳� ������ �߿� ��ųx
        {
            myAnim.SetTrigger("Skill");
            OnAttack();
        }
    }
}
