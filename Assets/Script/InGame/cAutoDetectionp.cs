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
        // �÷��̾� ����
        if ((DetectLayer & (1 << other.gameObject.layer)) == 64)
        {
            if (this.transform.parent.GetComponent<cMonsterp>().myState == cMonsterp.STATE.ROAMING)
            {
                Target = other.gameObject; // ����Ʈ�� ����
                this.transform.parent.GetComponent<cMonsterp>().OnBattle(); // ���͸� ��Ʋ���·� ����
            }
        }
        // ���� ����
        else if ((DetectLayer & (1 << other.gameObject.layer)) == 256)
        {
            if (this.transform.parent.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
            {
                Target = other.gameObject; // ����Ʈ�� ����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if((DetectLayer & (1 << other.gameObject.layer)) > 0)
        {
            Target = null; // Ÿ�� ����
        }
    }
}

