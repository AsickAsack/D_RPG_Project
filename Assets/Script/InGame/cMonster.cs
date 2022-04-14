using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonster : cCharacteristic
{
    public enum STATE
    {
        NONE, CREAT, ROAMING, BATTLE, DEAD        
    }

    public STATE myState = STATE.NONE;

    void Start()
    {
        ChangeState(STATE.CREAT);
    }

    void Update()
    {
        StateProcess();
    }

    void ChangeState(STATE s)
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

    void StateProcess()
    {
        // �� �����Ӹ��� �ؾ��� �ϵ��� ���۽����� -> update������ �� �����Ӹ��� ȣ���Ŵ
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
}
