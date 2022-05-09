using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveCamera : MonoBehaviour
{
    
    Vector2 clickPoint; //클릭시 클릭포지션 저장을위해선언
    public float dragSpeed = 10.0f; //스피드

    void Update()
    {
        
 
        

        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition; // 클릭시 클릭포인트받기

        if (Input.GetMouseButton(0))
        {
           



                Vector3 position
                    = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint); // 마우스포지션에서 클릭포인트를 빼줌



                Vector3 move = -position * (Time.deltaTime * dragSpeed); //포지션만큼 이동 

                float y = transform.position.y; //위치값 고정을위해 y선언
                float x = transform.position.x; //위치값 고정을위해 x선언
                float z = transform.position.z; //위치값 고정을위해 z선언
            transform.Translate(move); // 이동
                transform.transform.position 
                    = new Vector3(transform.position.x, y, transform.position.z); // 이동중에 y축고정을위해 
                
                if (this.transform.position.x <= 399 || this.transform.position.x >= 404  ) // x값이 399이하로내려가거나 404이상으로 올라가면 이동을맊기위한  코드
                {
                    transform.transform.position
                   = new Vector3(x, y, transform.position.z);
                }
                if(this.transform.position.z <= -2.3 || this.transform.position.z >= 2.7) // z값이 -2.3 이하로내려가거나 2.7 이상으로 올라가면 이동을맊기위한 코드
                {
                    transform.transform.position
                  = new Vector3(transform.position.x, y, z);
                }
            
           
        }
    }
}
