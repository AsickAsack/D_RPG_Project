using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveCamera : MonoBehaviour
{
    
    Vector2 clickPoint; //Ŭ���� Ŭ�������� ���������ؼ���
    public float dragSpeed = 10.0f; //���ǵ�

    void Update()
    {
        
 
        

        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition; // Ŭ���� Ŭ������Ʈ�ޱ�

        if (Input.GetMouseButton(0))
        {
           



                Vector3 position
                    = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint); // ���콺�����ǿ��� Ŭ������Ʈ�� ����



                Vector3 move = -position * (Time.deltaTime * dragSpeed); //�����Ǹ�ŭ �̵� 

                float y = transform.position.y; //��ġ�� ���������� y����
                float x = transform.position.x; //��ġ�� ���������� x����
                float z = transform.position.z; //��ġ�� ���������� z����
            transform.Translate(move); // �̵�
                transform.transform.position 
                    = new Vector3(transform.position.x, y, transform.position.z); // �̵��߿� y����������� 
                
                if (this.transform.position.x <= 399 || this.transform.position.x >= 404  ) // x���� 399���Ϸγ������ų� 404�̻����� �ö󰡸� �̵�����������  �ڵ�
                {
                    transform.transform.position
                   = new Vector3(x, y, transform.position.z);
                }
                if(this.transform.position.z <= -2.3 || this.transform.position.z >= 2.7) // z���� -2.3 ���Ϸγ������ų� 2.7 �̻����� �ö󰡸� �̵����������� �ڵ�
                {
                    transform.transform.position
                  = new Vector3(transform.position.x, y, z);
                }
            
           
        }
    }
}
