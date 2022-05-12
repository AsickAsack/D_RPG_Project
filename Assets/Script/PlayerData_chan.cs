using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerData //Chan
{
    public PlayerStat playerStat;
    public MonsterStat monsterInitialStat;
}

[Serializable]
public class PlayerStat
{
    // 기본 캐릭터 스탯
    public float HP = 1000; // 체력

    public float ATK = 500; // 공격력 
    public float DEF = 500; // 방어력 
}

[Serializable]
public class MonsterStat
{
    // 기본 몬스터 스탯
    public float HP = 100; // 체력

    public float ATK = 500; // 공격력 
    public float DEF = 500; // 방어력 
}
