using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterHPBar : MonoBehaviour
{
    public void Initialize(Transform Root)
    {
        StartCoroutine(Following(Root));
    }

    IEnumerator Following(Transform Root)
    {
        while (Root != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(Root.position + new Vector3(0,2.5f,0)); // 타겟의 위치를 스크린상의 좌표로 어디인지 알려줌

            this.transform.position = pos; 

            yield return null;
        }
    }
}
