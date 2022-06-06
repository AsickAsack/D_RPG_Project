using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    Transform orgParent = null;
    Vector3 orgPos = Vector3.zero;
    public GameObject Explosion_Effect;
    public static bool IsHit = false;
    public Transform tiki;
    


    private void Awake()
    {
        orgParent = this.transform.parent;
        orgPos = this.transform.localPosition;
    }

    float playtime = 0.0f;

 


    void Update()
    {
      

        if(this.transform.parent == null)
        {
            this.transform.rotation = Quaternion.Euler(0, tiki.GetComponent<MTP>().orgRotation.eulerAngles.y, 0);
            IsHit = false;
            playtime += Time.deltaTime;
            this.transform.Translate(Vector3.forward * Time.deltaTime * 10.0f);
            
        }

        if(playtime > 2.0f)
        {
            playtime = 0.0f;
          
            this.gameObject.SetActive(false);
            this.transform.parent = orgParent;
            this.transform.localPosition = orgPos;

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        playtime = 0.0f;

        Explosion_Effect.transform.parent = null;
        Explosion_Effect.transform.position = this.transform.position;
        IsHit = true;
        if (collision.transform.GetComponent<multiPlayer>() != null)
        {
            collision.transform.GetComponent<multiPlayer>().hitProcess(30.0f);
        }
        this.gameObject.SetActive(false);
        this.transform.parent = orgParent;
        this.transform.localPosition = orgPos;
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    playtime = 0.0f;
        
    //    Explosion_Effect.transform.parent = null;
    //    Explosion_Effect.transform.position = this.transform.position;
    //    IsHit = true;
    //    if(other.GetComponent<multiPlayer>() != null)
    //    {
    //        other.GetComponent<multiPlayer>().hitProcess(30.0f);
    //    }
    //    this.gameObject.SetActive(false);
    //    this.transform.parent = orgParent;
    //    this.transform.localPosition = orgPos;

    //}


    
}
