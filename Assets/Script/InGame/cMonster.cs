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

    // HP바
    public GameObject HP_Prefab;
    public GameObject myHPBar;
    float initialHP;

    public GameObject damageText; // 데미지 텍스트

    protected Coroutine moveRoutine = null;
    protected Coroutine rotRoutine = null;

    protected Vector3 RoamingArea = Vector3.zero; // 로밍할 구역
    protected Vector3 StartPos = Vector3.zero; // 몬스터가 등장한 위치
    protected Vector3 Dir = Vector3.zero; // 몬스터가 이동할 방향
    protected float Dist = 0.0f; // 몬스터가 이동할 거리

    protected float MoveSpeed = 3.0f; // 몬스터 이동 속도
    protected float RotSpeed = 360.0f; // 몬스터 회전 속도
    protected float RoamingWaitTime = 3.0f; // 몬스터 로밍간 대기시간

    protected float ATK_Range = 1.0f; // 공격범위
    //protected float ATK_WaitingTime; // 공격 대기시간
    protected float convertDamage = 0.1f; // 데미지 환산 비율 => 데미지 = 공격력(ATK) * 데미지비율(convertDamage)

    //public bool isDying = false; // 몬스터의 죽는 애니메이션이 끝났는지 여부
    protected bool isAttacking = false; // 몬스터가 공격중인지 여부    

    public void OnDamage(float damage)
    {
        if (myState == STATE.BATTLE || myState == STATE.ROAMING)
        {
            myStats.HP -= damage;
            CreateDamageText(this.transform, damage); // 데미지 텍스트 생성

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP가 0이되면 사망
                Destroy(myHPBar.gameObject);
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

    void Awake()
    {
        InitializeStats();
    }

    protected void InitializeStats()
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
        StartPos = this.transform.position; // 초기 위치 저장

        // HP바 
        myHPBar = Instantiate(HP_Prefab, GameObject.Find("HPBarParent").transform);

        // 잡몹인 경우
        if (this.GetComponent<cNormalMonster>() != null)
        {
            myHPBar.GetComponent<cMonsterHPBar>().Initialize(this.transform);
        }
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
                MonsterMove(StartPos, RoamingArea, Dir, Dist);
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                DetectMove(Dir, Dist);
                break;
            case STATE.DEAD:
                OnDie();
                if (this.GetComponent<cNormalMonster>() == null)
                {
                    EndTimer(); // 보스가 죽었을 경우에만 타이머 중지
                }
                break;
            case STATE.ENDGAME:
                StartCoroutine(GotoResultScene());
                break;
        }
    }
       
    protected void StateProcess()
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

    public void StartRoaming()
    {
        ChangeState(STATE.ROAMING);
    }

    public void OnBattle()
    {
        StopAllCoroutines();
        ChangeState(STATE.BATTLE);
    }

    public void OnDead()
    {
        ChangeState(STATE.DEAD);
    }

    public void EndGame()
    {
        ChangeState(STATE.ENDGAME);
    }

    void CreateDamageText(Transform tr, float damage)
    {
        Transform spawnPos = GameObject.Find("InGame_Canvas").GetComponent<Transform>();  // 캔버스의 위치를 찾음

        // 데미지 텍스트 생성
        GameObject obj = Instantiate(damageText, spawnPos); // 캔버스에 ui로 생성
        cDamageText curDamageText = obj.GetComponentInChildren<cDamageText>();

        curDamageText.Initialize(tr);
        curDamageText.GetComponent<TMPro.TMP_Text>().text = "" + (int)damage;
        curDamageText.TextAnimation(obj.GetComponent<RectTransform>());
    }

    protected void DisplayHP()
    {
        if (myHPBar == null) return;

        // 현재 몬스터의 HP상태를 UI로 표시
        myHPBar.GetComponentInChildren<Slider>().value = myStats.HP / initialHP;
    }
        
    IEnumerator GotoResultScene()
    {
        // 3초 대기
        yield return new WaitForSeconds(3.0f);
        // 결과 씬을 불러옴
        
        ClickCanvas.Instance.Click_Canvas.enabled = true;
        SceneLoader.Instance.LoadScene(6);
    }

    public void EndTimer()
    {
        FindObjectOfType<cTimeManager>().TimerAvailable = false; // 시간 멈춤
        FindObjectOfType<cTimeManager>().SaveTime(); // 시간 저장
    }     

    void OnDie()
    {
        StopAllCoroutines(); 
        myAnim.SetTrigger("Die"); // 죽는 애니메이션 실행
        myAnim.SetBool("OnDie", true);

        StopCoroutine(Attacking());
        //myAnim.SetBool("IsAttack",false); // 사용중이던 스킬 해제

        // 잡몹이 죽는 경우
        if (this.GetComponent<cNormalMonster>() != null)
        {
            OnDisappear(); // 5초 뒤에 사라짐
            return;
        }

        Transform MonsterParent = GameObject.Find("MonsterParent").transform;

        // 보스가 죽을 경우 나머지 몬스터들도 모두 죽도록 설정
        for (int i = 0; i < MonsterParent.childCount; i++)
        {
            if (MonsterParent.GetChild(i).GetComponent<cNormalMonster>() == null) continue;

            // 잡몹들만 죽음
            MonsterParent.GetChild(i).GetComponent<cNormalMonster>().OnDead();

            // 잡몹들의 hp바 제거
            //GameObject.Find("HPBarParent").SetActive(false);
            Destroy(MonsterParent.GetChild(i).GetComponent<cNormalMonster>().myHPBar.gameObject);
        }
    }

    void OnDisappear()
    {
        StartCoroutine(Disappearing()); // 아래로 가라앉음
    }

    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject); // 게임 오브젝트 삭제
    }

    protected void DetectMove(Vector3 dir, float dist)
    {
        // 이동 - 대상을 따라다니도록
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(Targeting(dir, dist));
    }

    IEnumerator Targeting(Vector3 dir2, float dist2)
    {
        while (true)
        {
            if (myDetection.Target == null)
            {
                ChangeState(STATE.ROAMING);
                break;
            }

            // 매번 타겟의 위치를 갱신 -> 플레이어의 움직임을 받아옴 
            Vector3 dir = myDetection.Target.transform.position - this.transform.position; // 이동 방향
            float dist = dir.magnitude; // 목표지점까지의 거리
            dir.Normalize();

            CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // 각도 계산 -> 매번 해주어야 함

            // 회전
            myAnim.transform.rotation = Quaternion.Slerp(myAnim.transform.rotation, Quaternion.LookRotation(dir),Time.deltaTime * 10.0f);

            // 이동
            myAnim.SetBool("IsWalk", true); // idle -> walk_front 

            float delta = MoveSpeed * Time.deltaTime; // 이동 거리

            delta = delta > dist ? dist : delta; // 이동 거리가 남은 거리보다 클 경우 남은 거리 만큼만 이동

            this.transform.Translate(dir * delta);

            if (dist < 1.0f && myState != STATE.DEAD)
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

        // 보스인 경우
        if (this.GetComponent<cNormalMonster>() == null)
        {
            // 스킬번호 랜덤 설정
            int RandomSkill_num = Random.Range(0, 3);
            myAnim.SetInteger("Skill", RandomSkill_num);
        }

        myAnim.SetTrigger("Attack");

        isAttacking = true;

        while (isAttacking && myState != STATE.DEAD)
        {
            // 공격애니메이션이 끝나면 코루틴 종료 
            if (myAnim.GetBool("IsAttack") == false)
            {
                isAttacking = false;
                break;
            }

            yield return null;
        }
    }

    protected void MonsterMove(Vector3 startPos, Vector3 roamingArea, Vector3 dir, float dist)
    {
        // 이동방향 설정
        DirectionSetting(ref startPos, ref roamingArea, ref dir, ref dist); 

        // 회전
        if (rotRoutine != null)
        {
            StopCoroutine(rotRoutine);
        }
        rotRoutine = StartCoroutine(Rotate(dir));

        // 이동
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(Roaming(startPos, roamingArea, dir, dist));

    }   

    void DirectionSetting(ref Vector3 startPos, ref Vector3 roamingArea, ref Vector3 dir, ref float dist)
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

    IEnumerator Roaming(Vector3 startPos, Vector3 roamingArea, Vector3 dir, float dist)
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

        StartCoroutine(RoamingWait(RoamingWaitTime, () => MonsterMove(startPos, roamingArea, dir, dist))); // 다시 다른곳으로 로밍시작        
    }

    IEnumerator Rotate(Vector3 dir)
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
