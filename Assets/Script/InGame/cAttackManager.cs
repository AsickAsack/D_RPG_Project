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

    Coroutine myCoroutine = null;

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
        FindMonster();

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

        //if (myCoroutine != null)
        //{
        //    StopCoroutine(myCoroutine);
        //}
    }

    public void Skill_1()
    {
        if (!myAnim.GetBool("IsDoing")) // ��ų�̳� �����̳� ������ �߿� ��ųx
        {
            myAnim.SetTrigger("Skill");
            OnAttack();
        }
    }

    void FindMonster()
    {
        Vector3 dir = Vector3.zero;

        if (myDetection.Target == null) return;

        // ������ �������� �޾ƿ� 
        dir = myDetection.Target.transform.position - this.transform.position; // ���͸� �ٶ󺸴� ����
        
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        myCoroutine =  StartCoroutine(LookingTarget2(myAnim.transform, dir)); // ���͸� �ٶ󺸵��� ��
        //StartCoroutine(LookingTarget(myAnim.transform, dir));
    }

    IEnumerator LookingTarget2(Transform myTrans, Vector3 myDir)
    {
        while (true)
        {
            CalculateAngle(myTrans.forward, myDir, myTrans.right, out ROTDATA myRotData); // ���� ��� -> �Ź� ���־�� ��

            Quaternion dirQuat = Quaternion.LookRotation(myDir * 360.0f * Time.deltaTime); // ȸ���ؾ��ϴ� ���� ����
            Quaternion moveQuat = Quaternion.Slerp(myRigid.rotation, dirQuat, 360.0f); // ���� ȸ������ �ٲ� ȸ������ ����
            myRigid.MoveRotation(moveQuat);

            yield return null;
        }
    }

}
