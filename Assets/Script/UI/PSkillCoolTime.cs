using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PSkillCoolTime : MonoBehaviour
{
    public Image skillbg;// fillaumout ������ �̹��� 
    public Image skillicon; // fillaumout ������ �̹��� 
    public float coolTime; // ��Ÿ��
    public TextMeshProUGUI coolTimeCounter; //���� �lŸ�� ǥ���� �ؽ�Ʈ 
    private bool canUseSkill = true; // skill�� ����������ϰ� ���� bool�� 
    private float currentCoolTime; //���� ��Ÿ�� �������� 
     void Start()
    {
        skillbg.fillAmount = 1;
        skillicon.fillAmount = 1;
        GetComponent<Button>().interactable = true;
    }
    public void UseSkill()
    {
        if (canUseSkill)
        {
            skillbg.fillAmount = 0;
            skillicon.fillAmount = 0;
            StartCoroutine("CoolTime");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;
            StartCoroutine("CoolTimeCounter");
            coolTimeCounter.alpha = 1.0f;
            canUseSkill = false;
            GetComponent<Button>().interactable = false;
        }

    }
    IEnumerator CoolTime()
    {
        while(skillbg.fillAmount <1 )
        {
            
            skillbg.fillAmount += 1 * Time.smoothDeltaTime / coolTime;
           skillicon.fillAmount += 1* Time.smoothDeltaTime / coolTime; 
            yield return null;  
        }
        canUseSkill = true;
        GetComponent<Button>().interactable = true;
        yield break;
    }

    IEnumerator CoolTimeCounter()
    {
        while(currentCoolTime >0)
        {
            yield return new WaitForSeconds(1.0f);
            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
        }
        coolTimeCounter.alpha = 0.0f;
        yield break;
    }
}
