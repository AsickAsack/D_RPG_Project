using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAutoDetection : MonoBehaviour
{
    public LayerMask DetectLayer;
    public GameObject Target;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어 감지
        if ((DetectLayer & (1 << other.gameObject.layer)) == 64)
        {
            if (this.transform.parent.GetComponent<cMonster>().myState == cMonster.STATE.ROAMING)
            {
                Target = other.gameObject;
                this.transform.parent.GetComponent<cMonster>().OnBattle(); // 몬스터를 배틀상태로 변경
            }
        }
        // 몬스터 감지
        else if ((DetectLayer & (1 << other.gameObject.layer)) == 256)
        {
            if (this.transform.parent.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
            {
                Target = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if((DetectLayer & (1 << other.gameObject.layer)) > 0)
        {
            Target = null; // 타겟 해제
        }
    }
}


