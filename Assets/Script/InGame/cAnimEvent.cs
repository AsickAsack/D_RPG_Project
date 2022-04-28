using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cAnimEvent : MonoBehaviour
{
    public UnityAction Attack = null;
    //public UnityAction MonsterAttack = null;

    public void OnAttack()
    {
        Attack?.Invoke();
    }

    //public void OnMonsterAttack()
    //{
    //    MonsterAttack?.Invoke();
    //}

}
