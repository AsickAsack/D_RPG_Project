using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAutoDetection : MonoBehaviour
{
    public LayerMask DetectLayer;
    public GameObject Target;
    public List<GameObject> DetectedTargets;
        
    private void OnTriggerEnter(Collider other)
    {
        // 몬스터 감지
        if ((DetectLayer & (1 << other.gameObject.layer)) == 256)
        {
            if (this.transform.parent.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
            {
                DetectedTargets.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어 감지
        if ((DetectLayer & (1 << other.gameObject.layer)) == 64)
        {
            if (this.transform.parent.GetComponent<cMonster>().myState == cMonster.STATE.ROAMING)
            {
                // 플레이어가 play상태가 아닌경우 감지x
                if (other.GetComponent<cCharacter>().myState != cCharacter.STATE.PLAY) return;

                Target = other.gameObject;
                this.transform.parent.GetComponent<cMonster>().OnBattle(); // 몬스터를 배틀상태로 변경
            }
        }
        // 몬스터 감지
        else if ((DetectLayer & (1 << other.gameObject.layer)) == 256)
        {
            if (this.transform.parent.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
            {
                if (other.GetComponent<cMonster>() != null) // Toledo
                {
                    // 몬스터가 dead상태인 경우 타겟 해제
                    if (other.GetComponent<cMonster>().myState == cMonster.STATE.DEAD)
                    {
                        Target = null;
                        DetectedTargets.Remove(other.gameObject);
                        return;
                    }
                }
                else // 잡몹
                {
                    // 몬스터가 dead상태인 경우 타겟 해제
                    if (other.GetComponent<cMonsterp>().myState == cMonsterp.STATE.DEAD)
                    {
                        Target = null;
                        DetectedTargets.Remove(other.gameObject);
                        return;
                    }
                }
                
                Target = other.gameObject;
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if((DetectLayer & (1 << other.gameObject.layer)) > 0)
        {
            Target = null; // 타겟 해제
            DetectedTargets.Remove(other.gameObject);
        }
    }
}


