using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDamageText : MonoBehaviour
{
    public void Initialize(Transform Root)
    {
        StartCoroutine(Following(Root));
    }

    IEnumerator Following(Transform Root)
    {
        while (Root != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(Root.position); // 타겟의 위치를 스크린상의 좌표로 어디인지 알려줌
            pos.y += 50.0f;
            this.GetComponent<RectTransform>().anchoredPosition = pos; // UI는 RectTransform으로 접근해야함

            yield return null;
        }
    }

    public void TextAnimation(RectTransform rt)
    {
        StartCoroutine(TextUp(rt));
    }

    IEnumerator TextUp(RectTransform rt)
    {
        float dist = 300.0f;

        while (!Mathf.Approximately(dist, 0.0f))
        {
            // 데미지 텍스트 위로 상승
            float delta = Time.deltaTime * 100.0f;

            delta = delta > dist ? dist : delta;
            rt.Translate(Vector3.up * delta);

            dist -= delta;

            if (dist <= 0.0f)
            {
                Destroy(this.transform.parent.gameObject);
            }

            yield return null;
        }

        
    }
}
