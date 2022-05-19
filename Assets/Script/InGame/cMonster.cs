using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class cMonster : cCharacteristic, BattleSystem
{
    public enum STATE
    {
        CREAT, ROAMING, BATTLE, DEAD, ENDGAME      
    }

    public STATE myState = STATE.CREAT;

    public MonsterStat myStats;

    public LayerMask AttackMask; // 공격목표
    public Transform mySword; // 검

    public GameObject HP_Prefab;
    GameObject myHPBar;
    float initialHP;

    Coroutine moveRoutine = null;
    Coroutine rotRoutine = null;
   
    [SerializeField]Vector3 roamingArea = Vector3.zero; // 로밍할 구역
    [SerializeField] Vector3 startPos = Vector3.zero; // 몬스터가 등장한 위치
    Vector3 dir = Vector3.zero; // 몬스터가 이동할 방향
    float dist = 0.0f; // 몬스터가 이동할 거리


    public float MoveSpeed = 3.0f; // 몬스터 이동 속도
    public float RotSpeed = 360.0f; // 몬스터 회전 속도
    public float RoamingWaitTime = 3.0f; // 몬스터 로밍간 대기시간

    public float ATK_Range; // 공격범위
    public float ATK_WaitingTime; // 공격 대기시간
    public float convertDamage = 0.1f; // 데미지 환산 비율 => 데미지 = 공격력(ATK) * 데미지비율(convertDamage)

    public bool isDying = false; // 몬스터의 죽는 애니메이션이 끝났는지 여부
    public bool isAttacking = false; // 몬스터가 공격중인지 여부    

    public void OnDamage(float damage)
    {
        if (myState == STATE.BATTLE || myState == STATE.ROAMING)
        {
            myStats.HP -= damage;

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP가 0이되면 사망
            }
            else
            {
                isAttacking = false;
                myAnim.SetTrigger("OnDamage");
            }
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(mySword.position, 1.0f, AttackMask);

        foreach (Collider col in list)
        {
            BattleSystem bs = col.gameObject.GetComponent<BattleSystem>();

            if (bs != null)
            {
                bs.OnDamage(myStats.ATK * convertDamage);
            }
        }
    }

    private void Awake()
    {
        InitializeStats();
    }

    void InitializeStats()
    {
        // PlayerData의 정보를 가져옴
        myStats.HP = GameData.Instance.playerdata.monsterInitialStat.HP;
        myStats.ATK = GameData.Instance.playerdata.monsterInitialStat.ATK;
        myStats.DEF = GameData.Instance.playerdata.monsterInitialStat.DEF;

        initialHP = myStats.HP;
    }


    void Start()
    {
        this.GetComponentInChildren<cAnimEvent>().Attack += OnAttack;
        startPos = this.transform.position; // 초기 위치 저장

        // 보스 HP바 
        myHPBar = Instantiate(HP_Prefab, GameObject.Find("HPBarParent").transform);
    }

    void Update()
    {
        StateProcess();
        DisplayHP();
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.CREAT:
                break;
            case STATE.ROAMING:
                StopAllCoroutines();
                MonsterMove();
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                DetectMove();
                break;
            case STATE.DEAD:
                OnDie();
                EndTimer();
                break;
            case STATE.ENDGAME:
                StartCoroutine(GotoResultScene());
                break;
        }
    }
       
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREAT:
                break;
            case STATE.ROAMING:
                break;
            case STATE.BATTLE:
                break;
            case STATE.DEAD:
                break;
            case STATE.ENDGAME:
                break;
        }
    }
    void DisplayHP()
    {
        if (myHPBar == null) return;

        // 현재 몬스터의 HP상태를 UI로 표시
        myHPBar.GetComponentInChildren<Slider>().value = myStats.HP / initialHP;
    }

    public void EndGame()
    {
        ChangeState(STATE.ENDGAME);
    }

    IEnumerator GotoResultScene()
    {
        // 3초 대기
        yield return new WaitForSeconds(3.0f);
        // 결과 씬을 불러옴
        SceneLoader.Instance.LoadScene(6);
    }

    void EndTimer()
    {
        FindObjectOfType<cTimeManager>().TimerAvailable = false; // 시간 멈춤
        FindObjectOfType<cTimeManager>().SaveTime(); // 시간 저장
    }

    public void StartRoaming()
    {
        ChangeState(STATE.ROAMING);
    }

    void OnDie()
    {
        StopAllCoroutines();
        myAnim.SetTrigger("Die"); // 죽는 애니메이션 실행

        Transform MonsterParent = GameObject.Find("MonsterParent").transform;

        // 보스가 죽을 경우 나머지 몬스터들도 모두 죽도록 설정
        for (int i = 0; i < MonsterParent.childCount; i++)
        {
            if (MonsterParent.GetChild(i).GetComponent<cMonsterp>() == null) continue;

            // 잡몹들만 죽음
            MonsterParent.GetChild(i).GetComponent<cMonsterp>().OnDead();
        }
    }
        
    public void OnBattle()
    {
        StopAllCoroutines();
        ChangeState(STATE.BATTLE);
    }

    void DetectMove()
    {
        // 이동 - 대상을 따라다니도록
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(Targeting());
    }

    IEnumerator Targeting()
    {
        while (true)
        {
            if (myDetection.Target == null)
            {
                ChangeState(STATE.ROAMING);
                break;
            }

            // 매번 타겟의 위치를 갱신 -> 플레이어의 움직임을 받아옴 
            dir = myDetection.Target.transform.position - this.transform.position; // 이동 방향
            dist = dir.magnitude; // 목표지점까지의 거리
            dir.Normalize();

            CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // 각도 계산 -> 매번 해주어야 함

            // 회전
            myAnim.transform.rotation = Quaternion.Slerp(myAnim.transform.rotation, Quaternion.LookRotation(dir),Time.deltaTime * 10.0f);

            // 이동
            myAnim.SetBool("IsWalk", true); // idle -> walk_front 

            float delta = MoveSpeed * Time.deltaTime; // 이동 거리

            delta = delta > dist ? dist : delta; // 이동 거리가 남은 거리보다 클 경우 남은 거리 만큼만 이동

            this.transform.Translate(dir * delta);

            if (dist < ATK_Range)
            {
                // 공격범위 내에 플레이어가 들어온 경우 공격
                yield return StartCoroutine(Attacking());
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator Attacking()
    {
        myAnim.SetBool("IsWalk", false); // walk_front -> idle

        while (true)
        {
            if (isAttacking == false)
            { 
                // 공격
                int RandomSkill_num = Random.Range(0, 3);
                myAnim.SetInteger("Skill", RandomSkill_num);
                myAnim.SetTrigger("Attack");
                isAttacking = true;
            }

            // 공격애니메이션이 끝나면 코루틴 종료 
            if (myAnim.GetBool("IsAttack") == false)
            {
                isAttacking = false;
                break;
            }

            yield return null;
        }
    }

    void MonsterMove()
    {
        // 이동방향 설정
        DirectionSetting(); 

        // 회전
        if (rotRoutine != null)
        {
            StopCoroutine(rotRoutine);
        }
        rotRoutine = StartCoroutine(Rotate());

        // 이동
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(Roaming());

    }   

    void DirectionSetting()
    {
        // 이동할 지점을 랜덤으로 설정
        roamingArea.x = startPos.x + Random.Range(-5.0f, 5.0f);
        roamingArea.z = startPos.z + Random.Range(-5.0f, 5.0f);

        dir = roamingArea - this.transform.position; // 이동 방향
        dist = dir.magnitude; // 목표지점까지의 거리
        dir.Normalize();
    }

    IEnumerator RoamingWait(float t, UnityAction done)
    {
        myAnim.SetBool("IsWalk", false); // walk_front -> idle
        yield return new WaitForSeconds(t); // t초만큼 기다림
        done?.Invoke(); // delegate에 저장된 함수 실행
    }

    IEnumerator Roaming()
    {
        myAnim.SetBool("IsWalk", true); // idle -> walk_front                

        while (!Mathf.Approximately(dist, 0.0f))
        {
            float delta = MoveSpeed * Time.deltaTime; // 이동 거리

            delta = delta > dist ? dist : delta; // 이동 거리가 남은 거리보다 클 경우 남은 거리 만큼만 이동

            this.transform.Translate(dir * delta);
            dist -= delta;

            yield return null;
        }

        StartCoroutine(RoamingWait(RoamingWaitTime, () => MonsterMove())); // 다시 다른곳으로 로밍시작        
    }

    IEnumerator Rotate()
    {
        CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // 각도 계산

        while (!Mathf.Approximately(myRotData.angle, 0.0f))
        {
            float delta = RotSpeed * Time.deltaTime;

            delta = delta > myRotData.angle ? myRotData.angle : delta;

            myAnim.transform.Rotate(Vector3.up * delta * myRotData.rotDir);
            myRotData.angle -= delta;

            yield return null;
        }
    }
}
