using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterHPBar : MonoBehaviour
{
    public void Initialize(Transform Root, float Height)
    {
        StartCoroutine(Following(Root, Height));
    }

    IEnumerator Following(Transform Root, float Height)
    {
        while (Root != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(Root.position); // Ÿ���� ��ġ�� ��ũ������ ��ǥ�� ������� �˷���
            pos.y += Height; // Height��ŭ ���� �ø�
            this.GetComponent<RectTransform>().anchoredPosition = pos; // UI�� RectTransform���� �����ؾ���

            yield return null;
        }
    }
}
