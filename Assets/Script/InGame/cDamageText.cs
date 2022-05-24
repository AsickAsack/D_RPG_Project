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
            Vector3 pos = Camera.main.WorldToScreenPoint(Root.position); // Ÿ���� ��ġ�� ��ũ������ ��ǥ�� ������� �˷���
            pos.y += 50.0f;
            this.GetComponent<RectTransform>().anchoredPosition = pos; // UI�� RectTransform���� �����ؾ���

            yield return null;
        }
    }

    public void TextAnimation(RectTransform rt)
    {
        StartCoroutine(TextUp(rt));
    }

    IEnumerator TextUp(RectTransform rt)
    {
        float dist = 400.0f;

        while (!Mathf.Approximately(dist, 0.0f))
        {
            // ������ �ؽ�Ʈ ���� ���
            float delta = Time.deltaTime * 100.0f;

            delta = delta > dist ? dist : delta;
            rt.Translate(Vector3.up * delta);

            dist -= delta;
            print(dist);

            if (dist <= 0.0f)
            {
                print("b");
                Destroy(this.transform.parent.gameObject);
            }

            yield return null;
        }

        
    }
}
