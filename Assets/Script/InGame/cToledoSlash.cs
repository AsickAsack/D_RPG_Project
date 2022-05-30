using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cToledoSlash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cMonster Toledo = this.GetComponentInParent<cMonster>(); // cMonster 컴포넌트를 가진 부모에서 cMonster 컴포넌트를 받아옴
            other.GetComponent<cCharacter>().OnDamage(Toledo.myStats.ATK * 0.1f);
        }
    }
}
