using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    Vector3 Dir = Vector3.zero;


    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Dir.Set(x, 0, y);
        Dir = Dir.normalized * 020.0f * Time.deltaTime;
        

        this.transform.position += Dir * Time.deltaTime * 10.0f;


        Quaternion newRotation = Quaternion.LookRotation(Dir);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, 10.0f * Time.deltaTime);

    }
}
