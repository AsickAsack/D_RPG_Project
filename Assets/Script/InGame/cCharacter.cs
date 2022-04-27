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
            else
            {
                myAnim.SetTrigger("OnDamage");
            }
        }
    }

    void OnDie()
    {
        StopAllCoroutines();
        myAnim.SetTrigger("Die"); // �״� �ִϸ��̼� ����
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
            case STATE.PLAY:
                this.GetComponentInParent<CPlayerMove>().OnPlayerMoveControl(); // �÷��� ������ ���� ������ �� �ְ���                
                break;
            case STATE.DEAD:
                break;
        }
    }
}
