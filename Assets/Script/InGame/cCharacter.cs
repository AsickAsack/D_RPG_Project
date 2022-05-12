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

    public float initialHP = 0.0f; // Ǯ HP

    public void OnDamage(float damage)
    {
        if (myState == STATE.PLAY)
        {
            myStats.HP -= damage;

            if (myStats.HP <= 0.0f)
            {
                ChangeState(STATE.DEAD); // HP�� 0�̵Ǹ� ���
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
        myAnim.SetTrigger("Die"); // �״� �ִϸ��̼� ����

        List<GameObject> curDetected = myDetection.DetectedTargets;

        // �÷��̾� ����� ���͸� �ιֻ��·� �ٲ���
        for (int i = 0; i < curDetected.Count; i++)
        {
            if (curDetected[i].GetComponent<cMonster>() == null)
            {
                // ���
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
        // PlayerData�� ������ ������
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
                this.GetComponentInParent<CPlayerMove>().OnPlayerMoveControl(); // �÷��� ������ ���� ������ �� �ְ���         
                break;
            case STATE.DEAD:
                break;
        }
    }

    void DisplayHP()
    {
        // ���� ĳ������ HP���¸� UI�� ǥ��
        HPbar.value = myStats.HP / initialHP;
    }
}
