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
    //        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // 이 위치의 스크린상 좌표값을 받아옴
    //        Arrow.anchoredPosition = pos;
    //        yield return null;
    //    }        
    //}

    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            //Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // 이 위치의 스크린상 좌표값을 받아옴
            Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position); // 이 위치의 스크린상 좌표값을 받아옴
            Arrow.GetComponent<RectTransform>().anchoredPosition = pos;
            if(myArrow == null) myArrow = Instantiate(Arrow, ArrowParent);
            print(pos);
        }
    }
}
