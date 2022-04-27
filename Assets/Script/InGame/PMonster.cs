using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class PMonster : MonoBehaviour
{
    
    public enum STATE { idle, trace, attack,dead};
    public STATE myState = STATE.idle; 
    public Transform Target; //�÷��̾�
    public Transform Monster; //���� 
    NavMeshAgent navAgent; // Ÿ���������� �׺�޽�������Ʈ
    public float traceDist = 4.0f; //���Ͱ����Ÿ�
    public float attackDist = 2.0f; //���ݻ�Ÿ�
    Animator myAnim; //�ִϸ��̼� ������
    private bool isDead = false; //����
    

    void Start()
    {
       navAgent = this.gameObject.GetComponent<NavMeshAgent>(); //�׺�Ž�������Ʈ ��������Ʈ���ְ�
        myAnim = GetComponentInChildren<Animator>(); //�ִϸ����� ����ְ�
        
        StartCoroutine(CheckState()); // ���Ϳ� �÷��̾��� �Ÿ� üũ
    }
    IEnumerator CheckState()
    {
        while(!isDead) //�����ʴµ��� ���ѹݺ� 
        {
            yield return new WaitForSeconds(0.2f);// 0.2 �� ��ٸ��� 

            float dist = Vector3.Distance(Target.position, Monster.position); // �÷��̾�� ���ͰŸ� ��� 

            if(dist<=attackDist) //�Ÿ��� ���û�Ÿ����� ª���� ���ý�����Ʈ�κ��� 
            {
                ChangeState(STATE.attack); 
            }
            else if (dist <= traceDist) //�Ÿ��� �Ѵ»�Ÿ����� ª���� �Ѵ°ŷκ��� 
            {
                ChangeState(STATE.trace);
            }
            else //�ƴѰ�� idle ���� 
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
              
                myAnim.SetTrigger("Attack");


                break;
            case STATE.dead:
                myAnim.SetTrigger("Die");
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
            if (myAnim.GetBool("IsAttacking") == false)
            {
                navAgent.SetDestination(Target.position);
            }
        }
    }
   
}