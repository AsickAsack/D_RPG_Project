using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveCamera : MonoBehaviour
{
    static PMoveCamera _ins;
    public static PMoveCamera Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = FindObjectOfType<PMoveCamera>();
            }
            return _ins;
        }
    }
        Vector2 clickPoint; //Ŭ���� Ŭ�������� ���������ؼ���
    public float dragSpeed = 10.0f; //���ǵ�
    public bool Click= true;
    void Update()
    {
        
 
        

        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition; // Ŭ���� Ŭ������Ʈ�ޱ�

        if (Input.GetMouseButton(0) && Click==true)
        {
           



                Vector3 position
                    = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint); // ���콺�����ǿ��� Ŭ������Ʈ�� ����



                Vector3 move = -position * (Time.deltaTime * dragSpeed); //�����Ǹ�ŭ �̵� 

                float y = transform.position.y; //��ġ�� ���������� y����
                float x = transform.position.x; //��ġ�� ���������� x����
                float z = transform.position.z; //��ġ�� ���������� z����
            transform.Translate(move); // �̵�
                transform.transform.position 
                    = new Vector3(transform.position.x, transform.position.y, z); // �̵��߿� y����������� 
                
                if (this.transform.position.x <= 257 || this.transform.position.x >= 269 ) // x���̵�����������  �ڵ�
                {
                    transform.transform.position
                   = new Vector3(x, transform.position.y, z);
                }
                if(this.transform.position.y <= 175 || this.transform.position.y >= 186) // y���̵����������� �ڵ�
                {
                    transform.transform.position
                  = new Vector3(transform.position.x, y, z);
                }
            
           
        }
    }
}
