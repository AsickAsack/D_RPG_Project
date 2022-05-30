using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttackManager : cCharacteristic
{
    public LayerMask AttackMask;
    public Transform[] myAttackPoints; // 타격지점
    public Collider[] colPoints; // 부딫힌 지점
    public GameObject damageText; // 데미지 텍스트
    public Transform Canvas; // 캔버스위치

    public int Combo = 0;
    public float ComboLimitTime = 2.5f;

    public Coroutine myCoroutine = null;

    float playTime = 0.0f;
    float curTime = 0.0f;

    private void Start()
    {
        this.GetComponentInChildren<cAnimEvent>().Attack += OnAttack;
    }

    private void Update()
    {
        playTime += Time.deltaTime;
    }

    void OnAttack()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
        {
            colPoints = null; // 타격 후 다시 비움

            // 타격 지점을 기준으로 그 콜라이더에 부딫힌 콜라이더들을 담음
            foreach (Transform trans in myAttackPoints)
            {
                colPoints = Physics.OverlapSphere(trans.position, 1.0f, AttackMask);
            }

            foreach (Collider col in colPoints)
            {
                BattleSystem bs = col.gameObject.GetComponent<BattleSystem>();

                if (bs != null)
                {
                    float randomDamage = this.GetComponent<cCharacter>().myStats.ATK * Random.Range(0.05f, 0.3f);

                    bs.OnDamage(randomDamage);

                    // 보스
                    if (col.GetComponent<cMonster>() != null)
                    {
                        cMonster boss = col.GetComponent<cMonster>();

                        // 로밍상태 혹은 배틀상태인 경우에만 데미지 텍스트 생성
                        if (boss.myState == cMonster.STATE.ROAMING || boss.myState == cMonster.STATE.BATTLE)
                        {
                            // 데미지 텍스트 생성
                            //CreateDamageText(col, randomDamage);
                        }
                    }

                    // 잡몹
                    if (col.GetComponent<cNormalMonster>() != null)
                    {
                        cNormalMonster monster = col.GetComponent<cNormalMonster>();

                        // 로밍상태 혹은 배틀상태인 경우에만 데미지 텍스트 생성
                        if (monster.myState == cNormalMonster.STATE.ROAMING || monster.myState == cNormalMonster.STATE.BATTLE)
                        {
                            // 데미지 텍스트 생성
                            //CreateDamageText(col, randomDamage);
                        }
                    }

                    // 보스나 잡몬스터들이 로밍이나 배틀 상태인 경우에만 실행
                    //if (boss.myState == cMonster.STATE.ROAMING || boss.myState == cMonster.STATE.BATTLE || monster.myState == cNormalMonster.STATE.ROAMING || monster.myState == cNormalMonster.STATE.BATTLE)
                    //{
                    //    
                    //    CreateDamageText(col, randomDamage);
                    //}

                }
            }
        }

    }

    void CreateDamageText(Collider col, float damage)
    {
        // 데미지 텍스트 생성
        GameObject obj = Instantiate(damageText, Canvas); // 캔버스에 ui로 생성
        cDamageText curDamageText = obj.GetComponentInChildren<cDamageText>();

        curDamageText.Initialize(col.transform);
        curDamageText.GetComponent<TMPro.TMP_Text>().text = "" + (int)damage;
        curDamageText.TextAnimation(obj.GetComponent<RectTransform>());
    }

    public void BasicAttack()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
        {
            FindMonster();

            StartCoroutine(BasicAttacking());
        }
    }

    IEnumerator BasicAttacking()
    {
        // 타겟을 찾은 경우에만 대쉬
        if (myDetection.Target != null) 
        {
            // 대쉬가 끝난 이후에 공격을 하도록 함
            yield return StartCoroutine(Dash(myDetection.Target.transform.position));
        }

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
            }
        }
    }

    public void Skill_1()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
        {
            if (!myAnim.GetBool("IsDoing")) // 스킬이나 공격이나 구르기 중에 스킬x
            {
                myAnim.SetTrigger("Skill");
            }
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
        myCoroutine = StartCoroutine(Targeting(myAnim.transform, dir)); // 몬스터를 바라보도록 함
    }

    IEnumerator Targeting(Transform myTrans, Vector3 myDir)
    {
        while (true)
        {
            CalculateAngle(myTrans.forward, myDir, myTrans.right, out ROTDATA myRotData); // 각도 계산 -> 매번 해주어야 함

            Quaternion dirQuat = Quaternion.LookRotation(myDir * 360.0f * Time.deltaTime); // 회전해야하는 값을 저장
            Quaternion moveQuat = Quaternion.Slerp(myRigid.rotation, dirQuat, 1.0f); // 현재 회전값과 바뀔 회전값을 보간
            myRigid.MoveRotation(moveQuat); // 회전

            if (this.GetComponent<CPlayerMove>().isMove == true) break; // 플레이어 이동시 반복문 빠져나감

            yield return null;
        }
    }

    public void OnDash(Vector3 MonsterPos)
    {
        // 플레이어의 일정 범위내에 몬스터가 있으면 대쉬공격
        StartCoroutine(Dash(MonsterPos));
    }

    IEnumerator Dash(Vector3 MonsterPos)
    {
        while (true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, MonsterPos, 0.1f);
            if ((this.transform.position - MonsterPos).magnitude < 1.5f) break;
            yield return null;
        }
    }

}
