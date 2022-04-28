using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttackManager : cCharacteristic
{
    public LayerMask AttackMask;
    public Transform[] myAttackPoints; // 타격지점
    public Collider[] colPoints; // 부딫힌 지점

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

    }

    public void BasicAttack()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
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
                    StopAllCoroutines();
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
    }

    public void Skill_1()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
        {
            //FindMonster(); // 스킬은 아예 논타겟?

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
        float dist = dir.magnitude; // 몬스터와 플레이어 사이의 거리

        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        myCoroutine = StartCoroutine(Targeting(myAnim.transform, dir)); // 몬스터를 바라보도록 함

        print(dist);
        if (dist > 1.5f)
        {
            OnDash(myDetection.Target.transform.position); // 공격 사정거리 내의 몬스터가 일정거리 밖에 멀리 있을경우 대쉬로 몬스터 앞으로 이동 후 공격
        }
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
            if ((this.transform.position - MonsterPos).magnitude < 0.5f) break;
            yield return null;
        }
    }
}
