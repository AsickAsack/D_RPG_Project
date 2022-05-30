using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class cNormalMonster : cMonster, BattleSystem
{
    // ¿Ã∆Â∆Æ
    public GameObject HitEffect;
    public Transform HitPointPosition;  
    
    private void Awake()
    {
        base.InitializeStats();
    }       

    void Update()
    {
        base.StateProcess();
        base.DisplayHP();
    }
}
