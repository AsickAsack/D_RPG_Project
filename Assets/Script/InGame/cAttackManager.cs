using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttackManager : cCharacteristic
{
    public int Combo = 0;
    public float ComboLimitTime = 1.5f;

    float playTime = 0.0f;
    float curTime = 0.0f;   

    private void Update()
    {
        playTime += Time.deltaTime;
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
        }
    }
}
