using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cDirectionArrow : MonoBehaviour
{
    public GameObject Arrow;
    public Transform ArrowParent;

    GameObject myArrow;

    //private void Start()
    //{
    //    StartCoroutine(Finding());
    //}

    //IEnumerator Finding()
    //{
    //    while(this.gameObject.activeSelf)
    //    {
    //        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // �� ��ġ�� ��ũ���� ��ǥ���� �޾ƿ�
    //        Arrow.anchoredPosition = pos;
    //        yield return null;
    //    }        
    //}

    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            //Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // �� ��ġ�� ��ũ���� ��ǥ���� �޾ƿ�
            Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position); // �� ��ġ�� ��ũ���� ��ǥ���� �޾ƿ�
            Arrow.GetComponent<RectTransform>().anchoredPosition = pos;
            if(myArrow == null) myArrow = Instantiate(Arrow, ArrowParent);
            print(pos);
        }
    }
}
