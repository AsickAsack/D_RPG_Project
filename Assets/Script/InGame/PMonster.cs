using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PMonster : MonoBehaviour
{
    public enum STATE { idle, trace, attack,dead};
    public STATE myState = STATE.idle;
    public Transform Target;
    public Transform Monster;
    Rigidbody rigid;
    CapsuleCollider capsuleCollider;
    NavMeshAgent navAgent;

    public float traceDist = 1.0f;

    public float attackDist = 3.0f;

    private bool isDead = false;
    void Start()
    {
       navAgent = this.gameObject.GetComponent<NavMeshAgent>();

        
        StartCoroutine(CheckState());
    }
    IEnumerator CheckState()
    {
        while(!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(Target.position, Monster.position);

            if(dist<=attackDist)
            {
                ChangeState(STATE.attack); 
            }
            else if (dist <= traceDist)
            {
                ChangeState(STATE.trace);
            }
            else
            {
                ChangeState(STATE.idle);
            }
        }
    }
    void ChangeState(STATE s )
    {
        if (myState == s) return;
        myState = s;

        switch(myState)
        {
            case STATE.idle:
               
                break;
                case STATE.trace:
                
                break;
            case STATE.attack:
                break;
            case STATE.dead:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.idle:
                break;
            case STATE.trace:
                break;
            case STATE.attack:
                break;
            case STATE.dead:
                break;
        }
    }
    void Update()
    {
        if(myState == STATE.trace)
        {
            navAgent.SetDestination(Target.position);
        }
    }
  
}
