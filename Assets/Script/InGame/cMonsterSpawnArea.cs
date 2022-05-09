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
    public GameObject Boss = null; // 보스
    public GameObject[] Monsters; // 잡몹들
    public Transform[] SpawnPoints; // 몬스터가 스폰될 지점


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

            // 보스가 있을 경우 첫번째 몬스터를 보스로 지정
            if (i == 0 && Boss != null)
            {
                spawnMonster = Boss;
            }

            // 몬스터 생성
            GameObject obj = Instantiate(spawnMonster, this.transform.parent);
            obj.transform.position = SpawnPoints[i].position;
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if((PlayerMask & (1 << other.gameObject.layer)) != 0)
        {
            // 플레이어가 구역에 들어온 경우 스폰
            ChangeState(STATE.SPAWN);
        }
    }
}
