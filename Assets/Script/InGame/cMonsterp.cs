using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cMonsterp : cCharacteristicp, BattleSystemp
{
    public enum STATE
    {
        CREAT, ROAMING, BATTLE, DEAD        
    }

    public STATE myState = STATE.CREAT;

    //public GameData.PlayerData.Stats myStats;
    //public ROTDATA myRotData;

    public LayerMask AttackMask; // ���ݸ�ǥ
    public Transform mySword; // ��

    Coroutine moveRoutine = null;
    Coroutine rotRoutine = null;

    Vector3 roamingArea = Vector3.zero; // �ι��� ����
    Vector3 startPos = Vector3.zero; // ���Ͱ� ������ ��ġ
    Vector3 dir = Vector3.zero; // ���Ͱ� �̵��� ����
    float dist = 0.0f; // ���Ͱ� �̵��� �Ÿ�

    public float MoveSpeed = 3.0f; // ���� �̵� �ӵ�
    public float RotSpeed = 360.0f; // ���� ȸ�� �ӵ�
    public float RoamingWaitTime = 3.0f; // ���� �ιְ� ���ð�

    public float ATK_Range; // ���ݹ���
    public float ATK_WaitingTime; // ���� ���ð�
    public float convertDamage = 0.1f; // ������ ȯ�� ���� => ������ = ���ݷ�(ATK) * ����������(convertDamage)

    public bool isdying = false; // ������ �״� �ִϸ��̼��� �������� ����

    public void OnDamage(float damage)
    {
        if (myState == STATE.BATTLE || myState == STATE.ROAMING)
        {
            //myStats.HP -= damage;

            //if (myStats.HP <= 0.0f)
            //{
            //    ChangeState(STATE.DEAD); // HP�� 0�̵Ǹ� ���
            //}
            //else
            //{
            //    myAnim.SetTrigger("OnDamage");
            //}
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
               // bs.OnDamage(myStats.ATK * convertDamage);
            }
        }
    }

    void Start()
    {
        ChangeState(STATE.ROAMING); // �ιֻ��·� ����
        this.GetComponentInChildren<cAnimEvent>().Attack += OnAttack;
    }

    void Update()
    {
        StateProcess();
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.CREAT:
                startPos = this.transform.position; // �ʱ� ��ġ ����
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
                //DetectMove();
                if (myDetection.Target.GetComponent<cCharacter>().myState != cCharacter.STATE.PLAY)
                {
                    // ĳ���Ͱ� ������ �ιֻ��·� �ٲ���
                    ChangeState(STATE.ROAMING);
                }
                break;
            case STATE.DEAD:
                //if (isdying)
                //{
                //    OnDisappear();
                //}
                break;
        }
    }

    public void StartRoaming()
    {
        ChangeState(STATE.ROAMING);
    }

    void OnDie()
    {
        StopAllCoroutines();
        myAnim.SetTrigger("Die"); // �״� �ִϸ��̼� ����
    }

    //void OnDisappear()
    //{
    //    StartCoroutine(Disappearing()); // �Ʒ��� �������
    //}

    //IEnumerator Disappearing()
    //{
    //    float dist = 2.0f; // ������ �Ÿ�

    //    while (!Mathf.Approximately(dist, 0.0f))
    //    {
    //        float delta = Time.deltaTime * 0.5f;

    //        delta = delta > dist ? dist : delta;

    //        this.transform.Translate(Vector3.down * delta);
    //        dist -= delta;

    //        yield return null;
    //    }

    //    Destroy(this.gameObject); // ���� ������Ʈ ����
    //}

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
        moveRoutine = StartCoroutine(Attacking());

        // ȸ�� -> ���ݿ��� ���� ����
        //if (rotRoutine != null)
        //{
        //    StopCoroutine(rotRoutine);
        //}
        //rotRoutine = StartCoroutine(LookingTarget(myAnim.transform, dir));

        //Attacking();

    }

    //void Attacking()
    //{
    //    float AttackTime = ATK_WaitingTime; // ù ���ݽ� ������ ���� �ٷ� ����

    //    if (myDetection.Target == null)
    //    {
    //        ChangeState(STATE.ROAMING);
    //        return;
    //    }

    //    // �Ź� Ÿ���� ��ġ�� ���� -> �÷��̾��� �������� �޾ƿ� 
    //    dir = myDetection.Target.transform.position - this.transform.position; // �̵� ����
    //    dist = dir.magnitude; // ��ǥ���������� �Ÿ�
    //    dir.Normalize();

    //    CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // ���� ��� -> �Ź� ���־�� ��

    //    if (Vector3.Dot(myAnim.transform.right, dir) < 0.0f)
    //    {
    //        myRotData.rotDir = -1.0f; // ���ʹ���
    //    }

    //    // ȸ��
    //    if (!Mathf.Approximately(myRotData.angle, 0.0f))
    //    {
    //        float delta = RotSpeed * Time.deltaTime;

    //        delta = delta > myRotData.angle ? myRotData.angle : delta;

    //        myAnim.transform.Rotate(Vector3.up * delta * myRotData.rotDir);
    //    }

    //    if (dist > ATK_Range) // ���ݹ��� �ۿ� ���� ��� ����
    //    {
    //        myAnim.SetTrigger("IsWalk"); // idle -> walk_front 

    //        float delta = MoveSpeed * Time.deltaTime; // �̵� �Ÿ�

    //        delta = delta > dist ? dist : delta; // �̵� �Ÿ��� ���� �Ÿ����� Ŭ ��� ���� �Ÿ� ��ŭ�� �̵�

    //        this.transform.Translate(dir * delta);
    //    }
    //    else // ���ݹ��� ���� ���� ��� ����
    //    {
    //        myAnim.SetBool("IsWalk", false); // walk_front -> idle

    //        if (myAnim.GetBool("IsAttack") == false)
    //        {
    //            AttackTime += Time.deltaTime;

    //            if (AttackTime >= ATK_WaitingTime)
    //            {
    //                // ����
    //                myAnim.SetTrigger("Attack");
    //                myAnim.SetFloat("skill_num", Random.Range(0, 6));
    //                AttackTime = 0.0f;
    //            }
    //        }
    //    }
    //}

    IEnumerator Attacking()
    {
        float AttackTime = ATK_WaitingTime; // ù ���ݽ� ������ ���� �ٷ� ����

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

            //if (Vector3.Dot(myAnim.transform.right, dir) < 0.0f)
            //{
            //    myRotData.rotDir = -1.0f; // ���ʹ���
            //}

            // ȸ��
            myAnim.transform.rotation = Quaternion.Slerp(myAnim.transform.rotation, Quaternion.LookRotation(dir),Time.deltaTime * 10.0f);
            /*
            if (myRotData.angle > Mathf.Epsilon && myAnim.GetBool("IsWalk") == true) // �̵����� ��쿡�� ȸ��
            {
                float delta = RotSpeed * Time.deltaTime;

                delta = delta > myRotData.angle ? myRotData.angle : delta;

                myAnim.transform.Rotate(Vector3.up * delta * myRotData.rotDir, Space.World);
            }
            */

            if (dist > ATK_Range && myAnim.GetBool("IsAttack") == false) // ���ݹ��� �ۿ� ���� ��� ����, �������� ��� ������ �ʵ���
            {
                myAnim.SetTrigger("IsWalk"); // idle -> walk_front 

                float delta = MoveSpeed * Time.deltaTime; // �̵� �Ÿ�

                delta = delta > dist ? dist : delta; // �̵� �Ÿ��� ���� �Ÿ����� Ŭ ��� ���� �Ÿ� ��ŭ�� �̵�

                this.transform.Translate(dir * delta);
            }
            else // ���ݹ��� ���� ���� ��� ����
            {
                myAnim.SetBool("IsWalk", false); // walk_front -> idle

                if (myAnim.GetBool("IsAttack") == false)
                {
                    AttackTime += Time.deltaTime;

                    if (AttackTime >= ATK_WaitingTime)
                    {
                        // ����
                        int RandomSkill_num = Random.Range(0, 3);
                        //int RandomSkill_num = 2;
                        myAnim.SetInteger("Skill", RandomSkill_num);
                        myAnim.SetTrigger("Attack");
                        AttackTime = 0.0f;
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    // cCharacteristic���� �̵�
    //IEnumerator LookingTarget()
    //{
    //    while (true)
    //    {
    //        CalculateAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTDATA myRotData); // ���� ��� -> �Ź� ���־�� ��

    //        if (Vector3.Dot(myAnim.transform.right, dir) < 0.0f)
    //        {
    //            myRotData.rotDir = -1.0f; // ���ʹ���
    //        }

    //        if (!Mathf.Approximately(myRotData.angle, 0.0f))
    //        {
    //            float delta = RotSpeed * Time.deltaTime;

    //            delta = delta > myRotData.angle ? myRotData.angle : delta;

    //            myAnim.transform.Rotate(Vector3.up * delta * myRotData.rotDir);
    //        }

    //        yield return null;
    //    }
    //}

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

    // cCharacteristic���� �̵�
    //void CalculateAngle()
    //{
    //    // ���� ���
    //    float rad = Mathf.Acos(Vector3.Dot(myAnim.transform.forward, dir)); // �̵��� ���������� ������ ����
    //    myRotData.angle = 180 * (rad / Mathf.PI); // degree ������ �ٲ�
    //    myRotData.rotDir = 1.0f; // ȸ�� ���Ⱚ => ������

    //    if (Vector3.Dot(myAnim.transform.right, dir) < 0.0f)
    //    {
    //        myRotData.rotDir = -1.0f; // ���ʹ���
    //    }
    //}
}