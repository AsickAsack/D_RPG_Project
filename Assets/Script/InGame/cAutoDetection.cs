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
        // ���� ����
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
        // �÷��̾� ����
        if ((DetectLayer & (1 << other.gameObject.layer)) == 64)
        {
            if (this.transform.parent.GetComponent<cMonster>().myState == cMonster.STATE.ROAMING)
            {
                // �÷��̾ play���°� �ƴѰ�� ����x
                if (other.GetComponent<cCharacter>().myState != cCharacter.STATE.PLAY) return;

                Target = other.gameObject;
                this.transform.parent.GetComponent<cMonster>().OnBattle(); // ���͸� ��Ʋ���·� ����
            }
        }
        // ���� ����
        else if ((DetectLayer & (1 << other.gameObject.layer)) == 256)
        {
            if (this.transform.parent.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
            {
                if (other.GetComponent<cMonster>() != null) // Toledo
                {
                    // ���Ͱ� dead������ ��� Ÿ�� ����
                    if (other.GetComponent<cMonster>().myState == cMonster.STATE.DEAD)
                    {
                        Target = null;
                        DetectedTargets.Remove(other.gameObject);
                        return;
                    }
                }
                else // ���
                {
                    // ���Ͱ� dead������ ��� Ÿ�� ����
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
            Target = null; // Ÿ�� ����
            DetectedTargets.Remove(other.gameObject);
        }
    }
}


