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

    // HP��
    public GameObject HP_Prefab;
    public GameObject myHPBar;
    float initialHP;

    public GameObject damageText; // ������ �ؽ�Ʈ

    protected Coroutine moveRoutine = null;
    protected Coroutine rotRoutine = null;

    protected Vector3 RoamingArea = Vector3.zero; // �ι��� ����
    protected Vector3 StartPos = Vector3.zero; // ���Ͱ� ������ ��ġ
    protected Vector3 Dir = Vector3.zero; // ���Ͱ� �̵��� ����
    protected float Dist = 0.0f; // ���Ͱ� �̵��� �Ÿ�

    protected float MoveSpeed = 3.0f; // ���� �̵� �ӵ�
    protected float RotSpeed = 360.0f; // ���� ȸ�� �ӵ�
    protected float RoamingWaitTime = 3.0f; // ���� �ιְ� ���ð�

    protected float ATK_Range = 1.0f; // ���ݹ���
    //protected float ATK_WaitingTime; // ���� ���ð�
    protected float convertDamage = 0.1f; // ������ ȯ�� ���� => ������ = ���ݷ�(ATK) * ����������(convertDamage)

    //public bool isDying = false; // ������ �״� �ִϸ��̼��� �������� ����
    protected bool isAttacking = false; // ���Ͱ� ���������� ����    

    public void OnDamage(float damage)
    {
        if (myState == STATE.BATTLE || myState == STATE.ROAMING)
        {
            myStats.HP -= damage;
            CreateDamageText(this.transform, damage); // ������ �ؽ�Ʈ ����

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP�� 0�̵Ǹ� ���
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
        // PlayerData�� ������ ������
        myStats.HP = GameData.Instance.playerdata.monsterInitialStat.HP;
        myStats.ATK = GameData.Instance.playerdata.monsterInitialStat.ATK;
        myStats.DEF = GameData.Instance.playerdata.monsterInitialStat.DEF;

        initialHP = myStats.HP;
    }

    void Start()
    {
        this.GetComponentInChildren<cAnimEvent>().Attack += OnAttack;
        StartPos = this.transform.position; // �ʱ� ��ġ ����

        // HP�� 
        myHPBar = Instantiate(HP_Prefab, GameObject.Find("HPBarParent").transform);

        // ����� ���
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
                    EndTimer(); // ������ �׾��� ��쿡�� Ÿ�̸� ����
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
        Transform spawnPos = GameObject.Find("InGame_Canvas").GetComponent<Transform>();  // ĵ������ ��ġ�� ã��

        // ������ �ؽ�Ʈ ����
        GameObject obj = Instantiate(damageText, spawnPos); // ĵ������ ui�� ����
        cDamageText curDamageText = obj.GetComponentInChildren<cDamageText>();

        curDamageText.Initialize(tr);
        curDamageText.GetComponent<TMPro.TMP_Text>().text = "" + (int)damage;
        curDamageText.TextAnimation(obj.GetComponent<RectTransform>());
    }

    protected void DisplayHP()
    {
        if (myHPBar == null) return;

        // ���� ������ HP���¸� UI�� ǥ��
        myHPBar.GetComponentInChildren<Slider>().value = myStats.HP / initialHP;
    }
        
    IEnumerator GotoResultScene()
    {
        // 3�� ���
        yield return new WaitForSeconds(3.0f);
        // ��� ���� �ҷ���
        
        ClickCanvas.Instance.Click_Canvas.enabled = true;
        SceneLoader.Instance.LoadScene(6);
    }

    public void EndTimer()
    {
        FindObjectOfType<cTimeManager>().TimerAvailable = false; // �ð� ����
        FindObjectOfType<cTimeManager>().SaveTime(); // �ð� ����
    }     

    void OnDie()
    {
        StopAllCoroutines(); 
        myAnim.SetTrigger("Die"); // �״� �ִϸ��̼� ����
        myAnim.SetBool("OnDie", true);

        StopCoroutine(Attacking());
        //myAnim.SetBool("IsAttack",false); // ������̴� ��ų ����

        // ����� �״� ���
        if (this.GetComponent<cNormalMonster>() != null)
        {
            OnDisappear(); // 5�� �ڿ� �����
            return;
        }

        Transform MonsterParent = GameObject.Find("MonsterParent").transform;

        // ������ ���� ��� ������ ���͵鵵 ��� �׵��� ����
        for (int i = 0; i < MonsterParent.childCount; i++)
        {
            if (MonsterParent.GetChild(i).GetComponent<cNormalMonster>() == null) continue;

            // ����鸸 ����
            MonsterParent.GetChild(i).GetComponent<cNormalMonster>().OnDead();

            // ������� hp�� ����
            //GameObject.Find("HPBarParent").SetActive(false);
            Destroy(MonsterParent.GetChild(i).GetComponent<cNormalMonster>().myHPBar.gameObject);
        }
    }

    void OnDisappear()
    {
        StartCoroutine(Disappearing()); // �Ʒ��� �������
    }

    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject); // ���� ������Ʈ ����
    }

    protected void DetectMove(Vector3 dir, float dist)
    {
        // �̵� - ����� ����ٴϵ���
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

            // �Ź� Ÿ���� ��ġ�� ���� -> �÷��̾��� �������� �޾ƿ� 
            Vector3 dir = myDetection.Target.transform.position - this.transform.position; // �̵� ����
            float dist = dir.magnitude; // ��ǥ���������� �Ÿ�
            dir.Normalize();

            CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // ���� ��� -> �Ź� ���־�� ��

            // ȸ��
            myAnim.transform.rotation = Quaternion.Slerp(myAnim.transform.rotation, Quaternion.LookRotation(dir),Time.deltaTime * 10.0f);

            // �̵�
            myAnim.SetBool("IsWalk", true); // idle -> walk_front 

            float delta = MoveSpeed * Time.deltaTime; // �̵� �Ÿ�

            delta = delta > dist ? dist : delta; // �̵� �Ÿ��� ���� �Ÿ����� Ŭ ��� ���� �Ÿ� ��ŭ�� �̵�

            this.transform.Translate(dir * delta);

            if (dist < 1.0f && myState != STATE.DEAD)
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

        // ������ ���
        if (this.GetComponent<cNormalMonster>() == null)
        {
            // ��ų��ȣ ���� ����
            int RandomSkill_num = Random.Range(0, 3);
            myAnim.SetInteger("Skill", RandomSkill_num);
        }

        myAnim.SetTrigger("Attack");

        isAttacking = true;

        while (isAttacking && myState != STATE.DEAD)
        {
            // ���ݾִϸ��̼��� ������ �ڷ�ƾ ���� 
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
        // �̵����� ����
        DirectionSetting(ref startPos, ref roamingArea, ref dir, ref dist); 

        // ȸ��
        if (rotRoutine != null)
        {
            StopCoroutine(rotRoutine);
        }
        rotRoutine = StartCoroutine(Rotate(dir));

        // �̵�
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(Roaming(startPos, roamingArea, dir, dist));

    }   

    void DirectionSetting(ref Vector3 startPos, ref Vector3 roamingArea, ref Vector3 dir, ref float dist)
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

    IEnumerator Roaming(Vector3 startPos, Vector3 roamingArea, Vector3 dir, float dist)
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

        StartCoroutine(RoamingWait(RoamingWaitTime, () => MonsterMove(startPos, roamingArea, dir, dist))); // �ٽ� �ٸ������� �ιֽ���        
    }

    IEnumerator Rotate(Vector3 dir)
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
