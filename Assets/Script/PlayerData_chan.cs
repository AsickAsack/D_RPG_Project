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
    // �⺻ ĳ���� ����
    public float HP = 1000; // ü��

    public float ATK = 500; // ���ݷ� 
    public float DEF = 500; // ���� 
}

[Serializable]
public class MonsterStat
{
    // �⺻ ���� ����
    public float HP = 100; // ü��

    public float ATK = 500; // ���ݷ� 
    public float DEF = 500; // ���� 
}
