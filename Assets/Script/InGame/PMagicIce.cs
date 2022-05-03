using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMagicIce : MonoBehaviour
{
    public LayerMask CrashMask;
    public float Speed = 10.0f;
    bool bMove = false;
    float dist = 0.0f;
    public GameObject fx;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (bMove)
        {
            Ray ray = new Ray();
            ray.direction = this.transform.forward;
            ray.origin = this.transform.position;
            float delta = Speed * Time.deltaTime;
            if (Physics.Raycast(ray, out RaycastHit hit, delta, CrashMask))
            {

                Instantiate(fx, hit.point, hit.transform.rotation);
                Destroy(this.gameObject);
            }
            else
            {

                dist += delta;
                if (dist > 100.0f)
                {
                    Destroy(this.gameObject);
                }
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }

        }



    }
    public void Fire()
    {
        bMove = true;
        this.transform.SetParent(null);
    }

}
