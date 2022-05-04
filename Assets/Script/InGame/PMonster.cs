using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class PMonster : MonoBehaviour
{
    
    public enum STATE { idle, trace, attack,dead};
    public STATE myState = STATE.idle; 
    public Transform Target; //플레이어
    public Transform Monster; //몬스터 
    NavMeshAgent navAgent; // 타겟팅을위한 네브메쉬에이전트
    public float traceDist = 4.0f; //몬스터감지거리
    public float attackDist = 2.0f; //공격사거리
    Animator myAnim; //애니메이션 설정용
    private bool isDead = false; //죽음
    public Stats Skel01Stat;
    

    void Start()
    {
       navAgent = this.gameObject.GetComponent<NavMeshAgent>(); //네브매쉬에이전트 겟컴포넌트해주고
        myAnim = GetComponentInChildren<Animator>(); //애니메이터 잡아주고
        
        StartCoroutine(CheckState()); // 몬스터와 플레이어의 거리 체크
    }
    IEnumerator CheckState()
    {
        while(!isDead) //죽지않는동안 무한반복 
        {
            yield return new WaitForSeconds(0.2f);// 0.2 초 기다린후 

            float dist = Vector3.Distance(Target.position, Monster.position); // 플레이어와 몬스터거리 계산 

            if(dist<=attackDist) //거리가 어택사거리보다 짧을때 어택스테이트로변경 
            {
                ChangeState(STATE.attack); 
            }
            else if (dist <= traceDist) //거리가 쫓는사거리보다 짧을때 쫓는거로변경 
            {
                ChangeState(STATE.trace);
            }
            else //아닌경우 idle 상태 
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
                myAnim.SetBool("IsMoving", false);
              
                break;
                case STATE.trace:
                myAnim.SetBool("IsMoving", true);
                
                break;
            case STATE.attack:
                myAnim.SetBool("IsMoving", false);
                StartCoroutine(Attacking());

                
                



                break;
            case STATE.dead:
                myAnim.SetTrigger("Die");
                break;
        }
    }
    IEnumerator Attacking()
    {
       
        while(true)
        {
            if(myState == STATE.attack)
            {
                if (myAnim.GetBool("IsAttack") == false)
                {
                    myAnim.SetTrigger("Attack");
                }

            }
            else
            {
                StopCoroutine(Attacking());
            }
            yield return new WaitForSeconds(1.0f);

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
            if (myAnim.GetBool("IsAttack") == false)
            {
                navAgent.SetDestination(Target.position);
            }
        }
    }
   public void OnDamaged(float Damage)
    {
        if(myState!=STATE.dead)
        {

            Skel01Stat.HP -= Damage;
            print("A");
        }

        if(Skel01Stat.HP <=0.0f)
        {
            ChangeState(STATE.dead);
        }
        else
        {
            myAnim.SetTrigger("Hit");
        }
    }
}
