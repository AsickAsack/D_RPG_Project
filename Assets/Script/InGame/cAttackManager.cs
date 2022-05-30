using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttackManager : cCharacteristic
{
    public LayerMask AttackMask;
    public Transform[] myAttackPoints; // Ÿ������
    public Collider[] colPoints; // �΋H�� ����
    public GameObject damageText; // ������ �ؽ�Ʈ
    public Transform Canvas; // ĵ������ġ

    public int Combo = 0;
    public float ComboLimitTime = 2.5f;

    public Coroutine myCoroutine = null;

    float playTime = 0.0f;
    float curTime = 0.0f;

    private void Start()
    {
        this.GetComponentInChildren<cAnimEvent>().Attack += OnAttack;
    }

    private void Update()
    {
        playTime += Time.deltaTime;
    }

    void OnAttack()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
        {
            colPoints = null; // Ÿ�� �� �ٽ� ���

            // Ÿ�� ������ �������� �� �ݶ��̴��� �΋H�� �ݶ��̴����� ����
            foreach (Transform trans in myAttackPoints)
            {
                colPoints = Physics.OverlapSphere(trans.position, 1.0f, AttackMask);
            }

            foreach (Collider col in colPoints)
            {
                BattleSystem bs = col.gameObject.GetComponent<BattleSystem>();

                if (bs != null)
                {
                    float randomDamage = this.GetComponent<cCharacter>().myStats.ATK * Random.Range(0.05f, 0.3f);

                    bs.OnDamage(randomDamage);

                    // ����
                    if (col.GetComponent<cMonster>() != null)
                    {
                        cMonster boss = col.GetComponent<cMonster>();

                        // �ιֻ��� Ȥ�� ��Ʋ������ ��쿡�� ������ �ؽ�Ʈ ����
                        if (boss.myState == cMonster.STATE.ROAMING || boss.myState == cMonster.STATE.BATTLE)
                        {
                            // ������ �ؽ�Ʈ ����
                            //CreateDamageText(col, randomDamage);
                        }
                    }

                    // ���
                    if (col.GetComponent<cNormalMonster>() != null)
                    {
                        cNormalMonster monster = col.GetComponent<cNormalMonster>();

                        // �ιֻ��� Ȥ�� ��Ʋ������ ��쿡�� ������ �ؽ�Ʈ ����
                        if (monster.myState == cNormalMonster.STATE.ROAMING || monster.myState == cNormalMonster.STATE.BATTLE)
                        {
                            // ������ �ؽ�Ʈ ����
                            //CreateDamageText(col, randomDamage);
                        }
                    }

                    // ������ ����͵��� �ι��̳� ��Ʋ ������ ��쿡�� ����
                    //if (boss.myState == cMonster.STATE.ROAMING || boss.myState == cMonster.STATE.BATTLE || monster.myState == cNormalMonster.STATE.ROAMING || monster.myState == cNormalMonster.STATE.BATTLE)
                    //{
                    //    
                    //    CreateDamageText(col, randomDamage);
                    //}

                }
            }
        }

    }

    void CreateDamageText(Collider col, float damage)
    {
        // ������ �ؽ�Ʈ ����
        GameObject obj = Instantiate(damageText, Canvas); // ĵ������ ui�� ����
        cDamageText curDamageText = obj.GetComponentInChildren<cDamageText>();

        curDamageText.Initialize(col.transform);
        curDamageText.GetComponent<TMPro.TMP_Text>().text = "" + (int)damage;
        curDamageText.TextAnimation(obj.GetComponent<RectTransform>());
    }

    public void BasicAttack()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
        {
            FindMonster();

            StartCoroutine(BasicAttacking());
        }
    }

    IEnumerator BasicAttacking()
    {
        // Ÿ���� ã�� ��쿡�� �뽬
        if (myDetection.Target != null) 
        {
            // �뽬�� ���� ���Ŀ� ������ �ϵ��� ��
            yield return StartCoroutine(Dash(myDetection.Target.transform.position));
        }

        float ComboDurationTime = playTime - curTime; // �޺����ӽð�
        curTime = playTime;

        if (ComboDurationTime > ComboLimitTime)
        {
            Combo = 0; // �޺� �ʱ�ȭ

            // ó�� �޺� ���
            if (!myAnim.GetBool("IsDoing")) // ��ų�̳� ���� ������ �߿� ����x
            {
                myAnim.SetFloat("Combo", Combo);
                myAnim.SetTrigger("Attack");
                Combo = (Combo++) % 3;
            }

            // �ð� �ʱ�ȭ
            playTime = 0.0f;
            curTime = 0.0f;
        }
        else
        {
            // ���� �޺� ���
            if (!myAnim.GetBool("IsDoing")) // ��ų�̳� ���� ������ �߿� ����x
            {
                myAnim.SetFloat("Combo", Combo);
                myAnim.SetTrigger("Attack");
                Combo = (Combo + 1) % 3;
            }
        }
    }

    public void Skill_1()
    {
        if (this.GetComponent<cCharacter>().myState == cCharacter.STATE.PLAY)
        {
            if (!myAnim.GetBool("IsDoing")) // ��ų�̳� �����̳� ������ �߿� ��ųx
            {
                myAnim.SetTrigger("Skill");
            }
        }
    }

    void FindMonster()
    {
        Vector3 dir = Vector3.zero;

        if (myDetection.Target == null) return;

        // ������ �������� �޾ƿ� 
        dir = myDetection.Target.transform.position - this.transform.position; // ���͸� �ٶ󺸴� ����

        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        myCoroutine = StartCoroutine(Targeting(myAnim.transform, dir)); // ���͸� �ٶ󺸵��� ��
    }

    IEnumerator Targeting(Transform myTrans, Vector3 myDir)
    {
        while (true)
        {
            CalculateAngle(myTrans.forward, myDir, myTrans.right, out ROTDATA myRotData); // ���� ��� -> �Ź� ���־�� ��

            Quaternion dirQuat = Quaternion.LookRotation(myDir * 360.0f * Time.deltaTime); // ȸ���ؾ��ϴ� ���� ����
            Quaternion moveQuat = Quaternion.Slerp(myRigid.rotation, dirQuat, 1.0f); // ���� ȸ������ �ٲ� ȸ������ ����
            myRigid.MoveRotation(moveQuat); // ȸ��

            if (this.GetComponent<CPlayerMove>().isMove == true) break; // �÷��̾� �̵��� �ݺ��� ��������

            yield return null;
        }
    }

    public void OnDash(Vector3 MonsterPos)
    {
        // �÷��̾��� ���� �������� ���Ͱ� ������ �뽬����
        StartCoroutine(Dash(MonsterPos));
    }

    IEnumerator Dash(Vector3 MonsterPos)
    {
        while (true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, MonsterPos, 0.1f);
            if ((this.transform.position - MonsterPos).magnitude < 1.5f) break;
            yield return null;
        }
    }

}
