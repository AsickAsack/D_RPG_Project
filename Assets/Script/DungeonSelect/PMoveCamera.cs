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
        Vector2 clickPoint; //클릭시 클릭포지션 저장을위해선언
    public float dragSpeed = 10.0f; //스피드
    public bool Click= true;
    void Update()
    {
        
 
        

        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition; // 클릭시 클릭포인트받기

        if (Input.GetMouseButton(0) && Click==true)
        {
           



                Vector3 position
                    = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint); // 마우스포지션에서 클릭포인트를 빼줌



                Vector3 move = -position * (Time.deltaTime * dragSpeed); //포지션만큼 이동 

                float y = transform.position.y; //위치값 고정을위해 y선언
                float x = transform.position.x; //위치값 고정을위해 x선언
                float z = transform.position.z; //위치값 고정을위해 z선언
            transform.Translate(move); // 이동
                transform.transform.position 
                    = new Vector3(transform.position.x, transform.position.y, z); // 이동중에 y축고정을위해 
                
                if (this.transform.position.x <= 257 || this.transform.position.x >= 269 ) // x값이동을맊기위한  코드
                {
                    transform.transform.position
                   = new Vector3(x, transform.position.y, z);
                }
                if(this.transform.position.y <= 175 || this.transform.position.y >= 186) // y값이동을맊기위한 코드
                {
                    transform.transform.position
                  = new Vector3(transform.position.x, y, z);
                }
            
           
        }
    }
}
