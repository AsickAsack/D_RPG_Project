using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManger : MonoBehaviour
{
    public GameObject Mainpanel;
    public GameObject TeamMangementpanel;
    public GameObject Optionpanel;
    public GameObject Followerpanel;
    public GameObject Inventorypanel;
    public GameObject Belongingspanel;
    public GameObject Costumepanel;
  

    public void OnclickTeamMangement() //������ ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        TeamMangementpanel.SetActive(true);
    }
    public void BackMainteammange() //������ ���ʻ�� �ڷΰ��� �ڵ�
    {
        TeamMangementpanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickOption()// �ɼ� ��ư �ڵ� 
    {
        Optionpanel.SetActive(true);
    }
    public void CloseOption() //�ɼ� �ݱ��ư �ڵ�
    {
        Optionpanel.SetActive(false);
    }
    public void GodengeonScene() //����ȭ�� ��Ʋ ����ȯ �ڵ�
    {
        SceneManager.LoadScene(2);
    }
    public void OnclickInventory() //������ ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Inventorypanel.SetActive(true);
    }
    public void BackMainInventory() //��� ���ʻ�� �ڷΰ��� �ڵ�
    {
        Inventorypanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickFollower() //������ ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Followerpanel.SetActive(true);
    }
    public void BackMainFollower() //�ΰ� ���ʻ�� �ڷΰ��� �ڵ�
    {
        Followerpanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickBelongings() //������ ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Belongingspanel.SetActive(true);
    }
    public void BackMainBelongings() //����ǰ ���ʻ�� �ڷΰ��� �ڵ�
    {
        Belongingspanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickCostume() //������ ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Costumepanel.SetActive(true);
    }
    public void BackMainCostume() //�ڽ�Ƭ ���ʻ�� �ڷΰ��� �ڵ�
    {
        Costumepanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
}
