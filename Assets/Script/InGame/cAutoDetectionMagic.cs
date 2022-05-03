using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAutoDetectionMagic : MonoBehaviour
{
    public LayerMask DetectLayer;
    public GameObject Target;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 감지
        if ((DetectLayer & (1 << other.gameObject.layer)) == 64)
        {
            if (this.transform.parent.GetComponent<cMonsterMagic>().myState == cMonsterMagic.STATE.ROAMING)
            {
                Target = other.gameObject; // 리스트에 넣음
                this.transform.parent.GetComponent<cMonsterMagic>().OnBattle(); // 몬스터를 배틀상태로 변경
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
        Target = null; // 타겟 해제
    }
}


