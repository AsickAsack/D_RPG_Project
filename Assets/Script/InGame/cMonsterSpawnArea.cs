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
    public Transform MonsterParent; // 생성된 몬스터들의 인스턴스를 담을 부모

    public GameObject Boss = null; // 보스
    public GameObject[] Monsters; // 잡몹들
    public Transform[] SpawnPoints; // 몬스터가 스폰될 지점

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
                SpawnArea.DeActiveArrow(); // 방향 표시 끔
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
        
        // 보스가 죽을 경우 나머지 몬스터들도 모두 죽도록 설정
        if (BossColne.GetComponent<cMonster>().myState == cMonster.STATE.DEAD)
        {
            print(MonsterParent.childCount);
            for (int i = 0; i < MonsterParent.childCount; i++)
            {
                if (MonsterParent.GetComponentsInChildren<cMonsterp>()[i] != null)
                {
                    // 잡몹들만 죽음
                    MonsterParent.GetComponentsInChildren<cMonsterp>()[i].OnDead();
                }
            }
        }
    }

    void AreaClear()
    {
        // Area가 스폰된 이후에 몬스터가 모두 처리되면 END 상태로 바꿈
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

            // 보스가 있을 경우 첫번째 몬스터를 보스로 지정
            if (i == 0 && Boss != null)
            {
                spawnMonster = Boss;
            }

            // 몬스터 생성
            GameObject obj = Instantiate(spawnMonster, MonsterParent);
            obj.transform.position = SpawnPoints[i].position;

            // 생성된 몬스터가 보스인 경우 저장
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
            // 플레이어가 구역에 들어온 경우 스폰
            ChangeState(STATE.SPAWN);
        }
    }
}
