using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cCharacter : cCharacteristic, BattleSystem
{
    public enum STATE
    {
        CREAT, PLAY, DEAD
    }

 
    public STATE myState = STATE.CREAT;

    public PlayerStat myStats;
    public Slider HPbar;

    public float initialHP = 0.0f; // 풀 HP

    public void OnDamage(float damage)
    {
        if (myState == STATE.PLAY)
        {
            myStats.HP -= damage;

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP가 0이되면 사망
            }
            else
            {
                myAnim.SetTrigger("OnDamage");               
            }

            DisplayHP(); 
        }
    }

    void OnDie()
    {
        StopAllCoroutines();
        myAnim.SetTrigger("Die"); // 죽는 애니메이션 실행

        List<GameObject> curDetected = myDetection.DetectedTargets;

        // 플레이어 사망시 몬스터를 로밍상태로 바꿔줌
        for (int i = 0; i < curDetected.Count; i++)
        {
            if (curDetected[i].GetComponent<cMonster>() == null)
            {
                // 잡몹
                curDetected[i].GetComponent<cMonsterp>().StartRoaming();
            }
            else
            {
                // Toledo
                curDetected[i].GetComponent<cMonster>().StartRoaming();
            }
        }
    }

    private void Awake()
    {
        InitializeStats();
    }

    void InitializeStats()
    {
        // PlayerData의 정보를 가져옴
        myStats.HP = GameData.Instance.playerdata.playerStat.HP;
        myStats.ATK = GameData.Instance.playerdata.playerStat.ATK;
        myStats.DEF = GameData.Instance.playerdata.playerStat.DEF;

        initialHP = myStats.HP;
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
                this.GetComponentInParent<CPlayerMove>().OnPlayerMoveControl(); // 플레이 상태일 때만 움직일 수 있게함         
                break;
            case STATE.DEAD:
                break;
        }
    }

    void DisplayHP()
    {
        // 현재 캐릭터의 HP상태를 UI로 표시
        HPbar.value = myStats.HP / initialHP;
    }
}
