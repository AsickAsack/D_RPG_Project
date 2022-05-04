using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAutoDetectionMagic : MonoBehaviour
{
    public LayerMask DetectLayer;
    public GameObject Target;

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� ����
        if ((DetectLayer & (1 << other.gameObject.layer)) == 64)
        {
            if (this.transform.parent.GetComponent<cMonsterMagic>().myState == cMonsterMagic.STATE.ROAMING)
            {
                Target = other.gameObject; // ����Ʈ�� ����
                this.transform.parent.GetComponent<cMonsterMagic>().OnBattle(); // ���͸� ��Ʋ���·� ����
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
        Target = null; // Ÿ�� ����
    }
}


