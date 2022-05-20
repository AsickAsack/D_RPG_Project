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
            Vector3 pos = Camera.main.WorldToScreenPoint(Root.position); // 타겟의 위치를 스크린상의 좌표로 어디인지 알려줌
            pos.y += Height; // Height만큼 위로 올림
            this.GetComponent<RectTransform>().anchoredPosition = pos; // UI는 RectTransform으로 접근해야함

            yield return null;
        }
    }
}
