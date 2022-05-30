using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAutoDetectionp : MonoBehaviour
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
            if (this.transform.parent.GetComponent<cNormalMonster>().myState == cNormalMonster.STATE.ROAMING)
            {
                // 플레이어가 play상태가 아닌경우 감지x
                if (other.GetComponent<cCharacter>().myState != cCharacter.STATE.PLAY) return;

                Target = other.gameObject; // 리스트에 넣음
                this.transform.parent.GetComponent<cNormalMonster>().OnBattle(); // 몬스터를 배틀상태로 변경
            }
        }
        // 몬스터 감지
        else if ((DetectLayer & (1 << other.gameObject.layer)) == 256)
        {
            if (this.transform.parent.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
            {
                Target = other.gameObject; // 리스트에 넣음
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


