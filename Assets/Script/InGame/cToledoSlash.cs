using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cToledoSlash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cMonster Toledo = this.GetComponentInParent<cMonster>(); // cMonster ������Ʈ�� ���� �θ𿡼� cMonster ������Ʈ�� �޾ƿ�
            other.GetComponent<cCharacter>().OnDamage(Toledo.myStats.ATK * 0.1f);
        }
    }
}
