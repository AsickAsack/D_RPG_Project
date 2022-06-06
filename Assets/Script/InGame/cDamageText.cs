using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDamageText : MonoBehaviour
{
    public void Initialize(Transform Root)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(Root.position); // Ÿ���� ��ġ�� ��ũ������ ��ǥ�� ������� �˷���
        this.transform.position = pos;
        //StartCoroutine(Following(Root));
    }

    IEnumerator Following(Transform Root)
    {
        while (Root != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(Root.position); // Ÿ���� ��ġ�� ��ũ������ ��ǥ�� ������� �˷���
            this.transform.position = pos;
            

            yield return null;
        }
    }

    public void TextAnimation(RectTransform rt)
    {
        StartCoroutine(TextUp(rt));
    }

    IEnumerator TextUp(RectTransform rt)
    {
        float dist = 100.0f;

        while (!Mathf.Approximately(dist, 0.0f))
        {
            Debug.Log(rt.position);
            // ������ �ؽ�Ʈ ���� ���
            float delta = Time.deltaTime * 100.0f;

            delta = delta > dist ? dist : delta;
            rt.transform.Translate(Vector3.up * delta);



            dist -= delta;

            if (dist <= 0.0f)
            {
                Destroy(this.transform.parent.gameObject);
            }

            yield return null;
        }

        
    }
}
