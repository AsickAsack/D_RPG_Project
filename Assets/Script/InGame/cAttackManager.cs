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
    }

    //IEnumerator ComboReset()
    //{
    //    while (isCombo)
    //    {
    //        if (!isCombo) break; // 공격버튼을 누르면 콤보 계속 진행

    //        // 일정 시간 안에 콤보가 갱신되지 않으면 콤보 리셋
    //        yield return new WaitForSeconds(1.5f);
    //        Combo = 0;
    //        isCombo = false;
    //    }
    //}

    public void Skill_1()
    {
        if (!myAnim.GetBool("IsDoing")) // 스킬이나 공격이나 구르기 중에 스킬x
        {
            myAnim.SetTrigger("Skill");
            OnAttack();
        }
    }
}
