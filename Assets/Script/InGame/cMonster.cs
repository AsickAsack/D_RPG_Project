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

    public LayerMask AttackMask; // ���ݸ�ǥ
    public Transform mySword; // ��

    public GameObject HP_Prefab;
    GameObject myHPBar;
    float initialHP;

    Coroutine moveRoutine = null;
    Coroutine rotRoutine = null;
   
    [SerializeField]Vector3 roamingArea = Vector3.zero; // �ι��� ����
    [SerializeField] Vector3 startPos = Vector3.zero; // ���Ͱ� ������ ��ġ
    Vector3 dir = Vector3.zero; // ���Ͱ� �̵��� ����
    float dist = 0.0f; // ���Ͱ� �̵��� �Ÿ�


    public float MoveSpeed = 3.0f; // ���� �̵� �ӵ�
    public float RotSpeed = 360.0f; // ���� ȸ�� �ӵ�
    public float RoamingWaitTime = 3.0f; // ���� �ιְ� ���ð�

    public float ATK_Range; // ���ݹ���
    public float ATK_WaitingTime; // ���� ���ð�
    public float convertDamage = 0.1f; // ������ ȯ�� ���� => ������ = ���ݷ�(ATK) * ����������(convertDamage)

    public bool isDying = false; // ������ �״� �ִϸ��̼��� �������� ����
    public bool isAttacking = false; // ���Ͱ� ���������� ����    

    public void OnDamage(float damage)
    {
        if (myState == STATE.BATTLE || myState == STATE.ROAMING)
        {
            myStats.HP -= damage;

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP�� 0�̵Ǹ� ���
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
        // PlayerData�� ������ ������
        myStats.HP = GameData.Instance.playerdata.monsterInitialStat.HP;
        myStats.ATK = GameData.Instance.playerdata.monsterInitialStat.ATK;
        myStats.DEF = GameData.Instance.playerdata.monsterInitialStat.DEF;

        initialHP = myStats.HP;
    }


    void Start()
    {
        this.GetComponentInChildren<cAnimEvent>().Attack += OnAttack;
        startPos = this.transform.position; // �ʱ� ��ġ ����

        // ���� HP�� 
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

        // ���� ������ HP���¸� UI�� ǥ��
        myHPBar.GetComponentInChildren<Slider>().value = myStats.HP / initialHP;
    }

    public void EndGame()
    {
        ChangeState(STATE.ENDGAME);
    }

    IEnumerator GotoResultScene()
    {
        // 3�� ���
        yield return new WaitForSeconds(3.0f);
        // ��� ���� �ҷ���
        SceneLoader.Instance.LoadScene(6);
    }

    void EndTimer()
    {
        FindObjectOfType<cTimeManager>().TimerAvailable = false; // �ð� ����
        FindObjectOfType<cTimeManager>().SaveTime(); // �ð� ����
    }

    public void StartRoaming()
    {
        ChangeState(STATE.ROAMING);
    }

    void OnDie()
    {
        StopAllCoroutines();
        myAnim.SetTrigger("Die"); // �״� �ִϸ��̼� ����

        Transform MonsterParent = GameObject.Find("MonsterParent").transform;

        // ������ ���� ��� ������ ���͵鵵 ��� �׵��� ����
        for (int i = 0; i < MonsterParent.childCount; i++)
        {
            if (MonsterParent.GetChild(i).GetComponent<cMonsterp>() == null) continue;

            // ����鸸 ����
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
        // �̵� - ����� ����ٴϵ���
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

            // �Ź� Ÿ���� ��ġ�� ���� -> �÷��̾��� �������� �޾ƿ� 
            dir = myDetection.Target.transform.position - this.transform.position; // �̵� ����
            dist = dir.magnitude; // ��ǥ���������� �Ÿ�
            dir.Normalize();

            CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // ���� ��� -> �Ź� ���־�� ��

            // ȸ��
            myAnim.transform.rotation = Quaternion.Slerp(myAnim.transform.rotation, Quaternion.LookRotation(dir),Time.deltaTime * 10.0f);

            // �̵�
            myAnim.SetBool("IsWalk", true); // idle -> walk_front 

            float delta = MoveSpeed * Time.deltaTime; // �̵� �Ÿ�

            delta = delta > dist ? dist : delta; // �̵� �Ÿ��� ���� �Ÿ����� Ŭ ��� ���� �Ÿ� ��ŭ�� �̵�

            this.transform.Translate(dir * delta);

            if (dist < ATK_Range)
            {
                // ���ݹ��� ���� �÷��̾ ���� ��� ����
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
                // ����
                int RandomSkill_num = Random.Range(0, 3);
                myAnim.SetInteger("Skill", RandomSkill_num);
                myAnim.SetTrigger("Attack");
                isAttacking = true;
            }

            // ���ݾִϸ��̼��� ������ �ڷ�ƾ ���� 
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
        // �̵����� ����
        DirectionSetting(); 

        // ȸ��
        if (rotRoutine != null)
        {
            StopCoroutine(rotRoutine);
        }
        rotRoutine = StartCoroutine(Rotate());

        // �̵�
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(Roaming());

    }   

    void DirectionSetting()
    {
        // �̵��� ������ �������� ����
        roamingArea.x = startPos.x + Random.Range(-5.0f, 5.0f);
        roamingArea.z = startPos.z + Random.Range(-5.0f, 5.0f);

        dir = roamingArea - this.transform.position; // �̵� ����
        dist = dir.magnitude; // ��ǥ���������� �Ÿ�
        dir.Normalize();
    }

    IEnumerator RoamingWait(float t, UnityAction done)
    {
        myAnim.SetBool("IsWalk", false); // walk_front -> idle
        yield return new WaitForSeconds(t); // t�ʸ�ŭ ��ٸ�
        done?.Invoke(); // delegate�� ����� �Լ� ����
    }

    IEnumerator Roaming()
    {
        myAnim.SetBool("IsWalk", true); // idle -> walk_front                

        while (!Mathf.Approximately(dist, 0.0f))
        {
            float delta = MoveSpeed * Time.deltaTime; // �̵� �Ÿ�

            delta = delta > dist ? dist : delta; // �̵� �Ÿ��� ���� �Ÿ����� Ŭ ��� ���� �Ÿ� ��ŭ�� �̵�

            this.transform.Translate(dir * delta);
            dist -= delta;

            yield return null;
        }

        StartCoroutine(RoamingWait(RoamingWaitTime, () => MonsterMove())); // �ٽ� �ٸ������� �ιֽ���        
    }

    IEnumerator Rotate()
    {
        CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // ���� ���

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
