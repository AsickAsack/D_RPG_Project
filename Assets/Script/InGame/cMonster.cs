using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct ROTDATA
{
    // ȸ�� ������
    public float angle;
    public float rotDir;
}

public class cMonster : cCharacteristic, BattleSystem
{
    public enum STATE
    {
        CREAT, ROAMING, BATTLE, DEAD        
    }

    public STATE myState = STATE.CREAT;

    public Stats myStats;
    public ROTDATA myRotData;

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

    public void OnDamage(float damage)
    {
        if (myState == STATE.BATTLE || myState == STATE.ROAMING)
        {
            myStats.HP -= damage;

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP�� 0�̵Ǹ� ���
            }
        }
    }    

    void Start()
    {
        ChangeState(STATE.ROAMING); // �ιֻ��·� ����
    }

    void FixedUpdate()
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
                MonsterMove();
                break;
            case STATE.BATTLE:
                DetectMove();
                break;
            case STATE.DEAD:
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
        moveRoutine = StartCoroutine(Attacking());

        // ȸ��
        if (rotRoutine != null)
        {
            StopCoroutine(rotRoutine);
        }
        rotRoutine = StartCoroutine(LookingTarget());

    }

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

            if (dist > ATK_Range) // ���ݹ��� �ۿ� ���� ��� ����
            {
                myAnim.SetBool("IsWalk", true); // idle -> walk_front 

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

                    if(AttackTime >= ATK_WaitingTime)
                    {
                        // ����
                        myAnim.SetTrigger("Attack");
                        AttackTime = 0.0f;
                    }
                }
            }
            yield return null;
        }
    }

    IEnumerator LookingTarget()
    {
        while (true)
        {
            CalculateAngle(); // ���� ��� -> �Ź� ���־�� ��

            if (Vector3.Dot(myAnim.transform.right, dir) < 0.0f)
            {
                myRotData.rotDir = -1.0f; // ���ʹ���
            }

            if (!Mathf.Approximately(myRotData.angle, 0.0f))
            {
                float delta = RotSpeed * Time.deltaTime;

                delta = delta > myRotData.angle ? myRotData.angle : delta;

                myAnim.transform.Rotate(Vector3.up * delta * myRotData.rotDir);
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
        CalculateAngle(); // ���� ���

        while (!Mathf.Approximately(myRotData.angle, 0.0f))
        {
            float delta = RotSpeed * Time.deltaTime;

            delta = delta > myRotData.angle ? myRotData.angle : delta;

            myAnim.transform.Rotate(Vector3.up * delta * myRotData.rotDir);
            myRotData.angle -= delta;

            yield return null;
        }
    }

    void CalculateAngle()
    {
        // ���� ���
        float rad = Mathf.Acos(Vector3.Dot(myAnim.transform.forward, dir)); // �̵��� ���������� ������ ����
        myRotData.angle = 180 * (rad / Mathf.PI); // degree ������ �ٲ�
        myRotData.rotDir = 1.0f; // ȸ�� ���Ⱚ => ������

        if (Vector3.Dot(myAnim.transform.right, dir) < 0.0f)
        {
            myRotData.rotDir = -1.0f; // ���ʹ���
        }
    }
}
