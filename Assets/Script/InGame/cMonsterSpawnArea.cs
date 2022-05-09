using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterSpawnArea : MonoBehaviour
{
    public enum STATE
    {
        WAIT, SPAWN
    }
    public STATE myState = STATE.WAIT;

    public LayerMask PlayerMask;
    public GameObject Boss = null; // ����
    public GameObject[] Monsters; // �����
    public Transform[] SpawnPoints; // ���Ͱ� ������ ����


    private void Start()
    {
        
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
                MonsterSpawn();
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
                break;
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
            GameObject obj = Instantiate(spawnMonster, this.transform.parent);
            obj.transform.position = SpawnPoints[i].position;
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
