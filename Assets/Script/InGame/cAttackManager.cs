using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttackManager : cCharacteristic
{
    public LayerMask AttackMask;
    public Transform[] myAttackPoints; // 타격지점
    public Collider[] colPoints; // 부딫힌 지점

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
        // 부딫힌 지점을 기준으로 그 콜라이더에 부딫힌 콜라이더들을 담음
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

        float ComboDurationTime = playTime - curTime; // 콤보지속시간
        curTime = playTime;

        if (ComboDurationTime > ComboLimitTime)
        {
            Combo = 0; // 콤보 초기화

            // 처음 콤보 사용
            if (!myAnim.GetBool("IsDoing")) // 스킬이나 공격 구르기 중에 공격x
            {
                myAnim.SetFloat("Combo", Combo);
                myAnim.SetTrigger("Attack");
                Combo = (Combo++) % 3;
                OnAttack();
            }

            // 시간 초기화
            playTime = 0.0f; 
            curTime = 0.0f;
        }
        else
        {
            // 다음 콤보 사용
            if (!myAnim.GetBool("IsDoing")) // 스킬이나 공격 구르기 중에 공격x
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
        if (!myAnim.GetBool("IsDoing")) // 스킬이나 공격이나 구르기 중에 스킬x
        {
            myAnim.SetTrigger("Skill");
            OnAttack();
        }
    }

    void FindMonster()
    {
        Vector3 dir = Vector3.zero;

        if (myDetection.Target == null) return;

        // 몬스터의 움직임을 받아옴 
        dir = myDetection.Target.transform.position - this.transform.position; // 몬스터를 바라보는 방향
        
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        myCoroutine =  StartCoroutine(LookingTarget2(myAnim.transform, dir)); // 몬스터를 바라보도록 함
        //StartCoroutine(LookingTarget(myAnim.transform, dir));
    }

    IEnumerator LookingTarget2(Transform myTrans, Vector3 myDir)
    {
        while (true)
        {
            CalculateAngle(myTrans.forward, myDir, myTrans.right, out ROTDATA myRotData); // 각도 계산 -> 매번 해주어야 함

            Quaternion dirQuat = Quaternion.LookRotation(myDir * 360.0f * Time.deltaTime); // 회전해야하는 값을 저장
            Quaternion moveQuat = Quaternion.Slerp(myRigid.rotation, dirQuat, 360.0f); // 현재 회전값과 바뀔 회전값을 보간
            myRigid.MoveRotation(moveQuat);

            yield return null;
        }
    }

}
