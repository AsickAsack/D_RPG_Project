using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterSpawnArea : MonoBehaviour
{
    public cSpawnArea SpawnArea;

    public enum STATE
    {
        WAIT, SPAWN, END
    }
    public STATE myState = STATE.WAIT;

    public LayerMask PlayerMask;
    public Transform MonsterParent; // ������ ���͵��� �ν��Ͻ��� ���� �θ�

    public GameObject Boss = null; // ����
    public GameObject[] Monsters; // �����
    public Transform[] SpawnPoints; // ���Ͱ� ������ ����

    GameObject BossColne = null;
    
    private void Awake()
    {
        SpawnArea = this.transform.parent.GetComponent<cSpawnArea>();
    }

    private void Update()
    {
        StateProcess();
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.WAIT:
                break;
            case STATE.SPAWN:
                SpawnArea.DeActiveArrow(); // ���� ǥ�� ��
                MonsterSpawn();
                break;
            case STATE.END:
                SpawnArea.nextArea.gameObject.SetActive(false);
                SpawnArea.NextArea(SpawnArea.curAreaNum);                
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.WAIT:
                break;
            case STATE.SPAWN:
                AreaClear();
                //BossKill();
                break;
            case STATE.END:
                break;
        }
    }

    void BossKill()
    {
        if (Boss == null) return;
        
        // ������ ���� ��� ������ ���͵鵵 ��� �׵��� ����
        if (BossColne.GetComponent<cMonster>().myState == cMonster.STATE.DEAD)
        {
            print(MonsterParent.childCount);
            for (int i = 0; i < MonsterParent.childCount; i++)
            {
                if (MonsterParent.GetComponentsInChildren<cMonsterp>()[i] != null)
                {
                    // ����鸸 ����
                    MonsterParent.GetComponentsInChildren<cMonsterp>()[i].OnDead();
                }
            }
        }
    }

    void AreaClear()
    {
        // Area�� ������ ���Ŀ� ���Ͱ� ��� ó���Ǹ� END ���·� �ٲ�
        if (MonsterParent.childCount == 0)
        {
            ChangeState(STATE.END);
        }
    }

    void MonsterSpawn()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameObject spawnMonster = Monsters[Random.Range(0, Monsters.Length)];

            // ������ ���� ��� ù��° ���͸� ������ ����
            if (i == 0 && Boss != null)
            {
                spawnMonster = Boss;
            }

            // ���� ����
            GameObject obj = Instantiate(spawnMonster, MonsterParent);
            obj.transform.position = SpawnPoints[i].position;

            // ������ ���Ͱ� ������ ��� ����
            if (spawnMonster == Boss)
            {
                BossColne = obj;
            }
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if((PlayerMask & (1 << other.gameObject.layer)) != 0)
        {
            // �÷��̾ ������ ���� ��� ����
            ChangeState(STATE.SPAWN);
        }
    }
}
