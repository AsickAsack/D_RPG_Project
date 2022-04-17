using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JPopUpCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject UI_Canvas; // ���� ų UIĵ����(�ý��� �̱��� �˾�â)

    [SerializeField]
    private AudioClip Ui_Click; // UIŬ�������� ����� ȿ����

    [SerializeField]
    private AudioClip DunGeon_Click; // ���� ���� Ŭ�������� ����� ȿ����

    public bool IsUIopen = false;

    AudioSource audioSource; // ȿ���� ����� �����Ŭ��



    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void EnterPopUp() // �̱��� �����ܵ� ������ �Լ�
    {
        audioSource.PlayOneShot(Ui_Click);
        UI_Canvas.SetActive(true);
        IsUIopen = true;
    }

    public void ExitPopUp() // �̱��� �˾� Ȯ��â ������ �Լ�
    {
        audioSource.PlayOneShot(Ui_Click);
        UI_Canvas.SetActive(false);
        IsUIopen = false;
    }

    public void EnterDunGeon() // �������� ������ �������� �Լ�
    {
        audioSource.PlayOneShot(DunGeon_Click);
        StartCoroutine(Delay(1)); // ȿ���� ����� ���� 1�� ������ �ڷ�ƾ (���ϸ� ȿ���� �Ҹ��� �ȳ��� ���̵���)
    }

    public void Ui_isOpen() // ui�� �����ִ���
    {
        IsUIopen = true;
    }

    public void Ui_isClose() // ui�� �����ִ��� 
    {
        IsUIopen = false;
    }


    IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene("SelecteDungeon");
    }

    
        


}
