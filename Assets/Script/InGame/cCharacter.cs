using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCharacter : cCharacteristic, BattleSystem
{
    public enum STATE
    {
        CREAT, PLAY, DEAD
    }

    public STATE myState = STATE.CREAT;

    public Stats myStats;
    public CPlayerMove myPlayerMove;
        
    public void OnDamage(float damage)
    {
        if (myState == STATE.PLAY)
        {
            myStats.HP -= damage;
            print("A");

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP�� 0�̵Ǹ� ���
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(STATE.PLAY);
    }

    // Update is called once per frame
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
                break;
            case STATE.PLAY:
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
            case STATE.PLAY:
                myPlayerMove.OnPlayerMoveControl(); // �÷��� ������ ���� ������ �� �ְ���
                FindMonster(); // ���ݹ������� ������ ĳ���Ͱ� ���͸� �ٶ󺸷� ��
                break;
            case STATE.DEAD:
                break;
        }
    }

    void FindMonster()
    {
        Vector3 dir = Vector3.zero;

        if (myDetection.Target == null) return;

        print("a");
        // �Ź� Ÿ���� ��ġ�� ���� -> �÷��̾��� �������� �޾ƿ� 
        dir = myDetection.Target.transform.position - this.transform.position; // �̵� ����
        LookingTarget(myAnim.transform, dir);
    }
}
