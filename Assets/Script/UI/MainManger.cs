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
    public GameObject Shoppanel;
    public GameObject Cashshoppanel;
    public GameObject Alarmpanel;
    public GameObject Mailpanel;

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
    public void OnclickAlarm()// �˶� ��ư �ڵ� 
    {
       Alarmpanel.SetActive(true);
    }
    public void CloseAlarm() //�˶� �ݱ��ư �ڵ�
    {
        Alarmpanel.SetActive(false);
    }
    public void GodengeonScene() //����ȭ�� ��Ʋ ����ȯ �ڵ�
    {
        SceneManager.LoadScene(2);
    }
    public void OnclickInventory() //��� ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Inventorypanel.SetActive(true);
    }
    public void BackMainInventory() //��� ���ʻ�� �ڷΰ��� �ڵ�
    {
        Inventorypanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickFollower() //�ΰ� ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Followerpanel.SetActive(true);
    }
    public void BackMainFollower() //�ΰ� ���ʻ�� �ڷΰ��� �ڵ�
    {
        Followerpanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickBelongings() //����ǰ ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Belongingspanel.SetActive(true);
    }
    public void BackMainBelongings() //����ǰ ���ʻ�� �ڷΰ��� �ڵ�
    {
        Belongingspanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickCostume() //�ڽ�Ƭ ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Costumepanel.SetActive(true);
    }
    public void BackMainCostume() //�ڽ�Ƭ ���ʻ�� �ڷΰ��� �ڵ�
    {
        Costumepanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickShop() //���� ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Shoppanel.SetActive(true);
    }
    public void BackMainShop() //���� ���ʻ�� �ڷΰ��� �ڵ�
    {
        Shoppanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickCashshop() //ĳ�û��� ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
        Cashshoppanel.SetActive(true);
    }
    public void BackMainCashshop() //ĳ�û��� ���ʻ�� �ڷΰ��� �ڵ�
    {
       Cashshoppanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
    public void OnclickMail() //���� ��ư �ڵ�
    {
        Mainpanel.SetActive(false);
       Mailpanel.SetActive(true);
    }
    public void BackMainMail() //���� ���ʻ�� �ڷΰ��� �ڵ�
    {
        Mailpanel.SetActive(false);
        Mainpanel.SetActive(true);
    }
}