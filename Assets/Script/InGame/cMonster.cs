using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cMonster : cCharacteristic
{
    public enum STATE
    {
        CREAT, ROAMING, BATTLE, DEAD        
    }

    public STATE myState = STATE.CREAT;

    Coroutine moveRoutine = null;
    Coroutine rotRoutine = null;

    Vector3 roamingArea = Vector3.zero; // �ι��� ����
    Vector3 startPos = Vector3.zero; // ���Ͱ� ������ ��ġ
    Vector3 dir = Vector3.zero; // ���Ͱ� �̵��� ����
    float dist = 0.0f; // ���Ͱ� �̵��� �Ÿ�

    public float MoveSpeed = 3.0f; // ���� �̵� �ӵ�
    public float RotSpeed = 360.0f; // ���� ȸ�� �ӵ�
    public float RoamingWaitTime = 3.0f; // ���� �ιְ� ���ð�

    void Start()
    {
        ChangeState(STATE.ROAMING); // �ιֻ��·� ����
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
                MonsterMove();
                break;
            case STATE.BATTLE:
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

    void MonsterMove()
    {
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

    IEnumerator RoamingWait(float t, UnityAction done)
    {
        myAnim.SetBool("IsWalk", false); // walk_front -> idle
        yield return new WaitForSeconds(t); // t�ʸ�ŭ ��ٸ�
        done?.Invoke(); // delegate�� ����� �Լ� ����
    }

    IEnumerator Roaming()
    {
        DirectionSetting(); // �̵����� ����
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
        DirectionSetting(); // �̵����� ����

        float rad = Mathf.Acos(Vector3.Dot(myAnim.transform.forward, dir)); // �̵��� ���������� ������ ����
        float angle = 180 * (rad / Mathf.PI); // degree ������ �ٲ�
        float rotDir = 1.0f; // ȸ�� ���Ⱚ => ������

        if (Vector3.Dot(myAnim.transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f; // ���ʹ���
        }

        while (!Mathf.Approximately(angle, 0.0f))
        {
            float delta = RotSpeed * Time.deltaTime;

            delta = delta > angle ? angle : delta;

            myAnim.transform.Rotate(Vector3.up * delta * rotDir);
            angle -= delta;

            yield return null;
        }
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
}
