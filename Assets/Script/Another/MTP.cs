using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MTP : MonoBehaviour
{
    public GameObject Effect;
    public GameObject BombEffect;
    Transform orgParent;
    public Quaternion orgRotation;
    public Transform tiki;

   


    private void Awake()
    {
        orgParent = BombEffect.transform.parent;
    }

    public void SetRotation()
    {
        orgRotation = tiki.transform.rotation;
    }


    public void Create_Effect()
    {
        Effect.SetActive(true);
        
    }

    public void Shoot_Effect()
    {
        
        Effect.transform.parent = null;
        
    }

    private void Update()
    {
        if(SkillEffect.IsHit)
        {
            BombEffect.SetActive(true);
            StartCoroutine(WaitForEffect());
            SkillEffect.IsHit = false;
        }
    }

    IEnumerator WaitForEffect()
    {
        yield return new WaitForSeconds(1.0f);
        BombEffect.transform.parent = orgParent;
        BombEffect.SetActive(false);
        

    }
}
