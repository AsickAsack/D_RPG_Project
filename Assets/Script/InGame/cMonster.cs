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

    Vector3 startPos = Vector3.zero; // ���Ͱ� ������ ��ġ

    public float MoveSpeed = 3.0f; // ���� �̵� �ӵ�
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
                StartCoroutine(RoamingWait(RoamingWaitTime, () =>  StartCoroutine(Roaming()))); // �ι�, ���ٽ� �̿�
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

    IEnumerator RoamingWait(float t, UnityAction done)
    {
        yield return new WaitForSeconds(t); // t�ʸ�ŭ ��ٸ�
        done?.Invoke(); // delegate�� ����� �Լ� ����
    }

    IEnumerator Roaming()
    {
        // �̵��� ������ �������� ����
        Vector3 roamingArea = new Vector3();
        roamingArea.x = startPos.x + Random.Range(-5.0f, 5.0f);
        roamingArea.z = startPos.z + Random.Range(-5.0f, 5.0f);

        Vector3 dir = roamingArea - this.transform.position; // �̵� ����
        float dist = dir.magnitude; // ��ǥ���������� �Ÿ�
        dir.Normalize();             

        while (!Mathf.Approximately(dist, 0.0f))
        {
            float delta = MoveSpeed * Time.deltaTime; // �̵� �Ÿ�

            delta = delta > dist ? dist : delta; // �̵� �Ÿ��� ���� �Ÿ����� Ŭ ��� ���� �Ÿ� ��ŭ�� �̵�

            this.transform.Translate(dir * delta);
            dist -= delta;

            yield return null;
        }

        StartCoroutine(RoamingWait(RoamingWaitTime, () => StartCoroutine(Roaming()))); // �ٽ� �ٸ������� �ιֽ���
    }
}
