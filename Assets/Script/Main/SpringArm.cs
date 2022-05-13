using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public Vector3 Rot = Vector3.zero;
    public float RotSpeed = 3.0f;
    public Vector2 HorizontalRotRange;
    public Vector2 VerticalRotRange;
    public float SmoothRotSpeed = 5.0f;
    public CharacterSpin characterspin;
    

    void Start()
    {
        Rot = this.transform.localRotation.eulerAngles;
        //characterspin = GetComponent<CharacterSpin>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!JPopUpCanvas.IsUIopen)
        { 
        if (!characterspin.Is_Character_DragON && Input.GetMouseButton(0)) 
        { 
        
            float X = Input.GetAxis("Mouse X");
            float Y = Input.GetAxis("Mouse Y");
            Rot.y += X * RotSpeed;
            Rot.x += Y * RotSpeed;

           
            Rot.y = Mathf.Clamp(Rot.y, HorizontalRotRange.x, HorizontalRotRange.y);
            Rot.x = Mathf.Clamp(Rot.x, VerticalRotRange.x, VerticalRotRange.y);

            
          
        }
        }
        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(Rot), Time.deltaTime * SmoothRotSpeed);
        

    }
}