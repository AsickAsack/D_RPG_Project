using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CPlayerMove : MonoBehaviour, IDragHandler
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get // ����� �� �ٷ� ����ǵ��� property ���
        {
            if (_anim == null) _anim = this.GetComponent<Animator>(); // _anim�� ȣ���ϸ� �׻� ���� ������ �ִٴ� Ȯ���� ���� 

            if (_anim == null) _anim = this.GetComponentInChildren<Animator>(); // �ڽĿ� �������� ȣ��

            return _anim;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //myAnim.SetFloat("x", Input.GetAxis("Horizontal")); // Ű���� ���Ϲ�ư�� �޾ƿ�
        //myAnim.SetFloat("y", Input.GetAxis("Vertical")); // Ű���� �¿��ư�� �޾ƿ�
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("a");
    }
}
