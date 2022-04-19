using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAutoDetection : MonoBehaviour
{
    public GameObject Target;
    public LayerMask DetectLayer;

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� ����
        if ((DetectLayer & (1 << other.gameObject.layer)) != 0)
        {
            Target = other.gameObject; // ����Ʈ�� ����
            this.transform.parent.GetComponent<cMonster>().OnBattle(); // ���͸� ��Ʋ���·� ����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Target = null; // Ÿ�� ����
    }
}


