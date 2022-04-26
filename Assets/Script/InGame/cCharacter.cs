using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCharacter : MonoBehaviour, BattleSystem
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

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP가 0이되면 사망
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
                myPlayerMove.OnPlayerMoveControl(); // 플레이 상태일 때만 움직일 수 있게함
                break;
            case STATE.DEAD:
                break;
        }
    }
}
