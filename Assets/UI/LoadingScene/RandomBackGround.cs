using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackGround : MonoBehaviour
{
    public TMPro.TMP_Text Loading_Text;
    int a;

    private void Awake()
    {
        a = Random.Range(0,4);

        this.transform.GetChild(a).GetComponent<UnityEngine.UI.Image>().enabled = true;

        switch(a)
        {
            case 0:
                Loading_Text.text = "�縷�� ���� '�䷹��' �Դϴ�.";
                break;
            case 1:
                Loading_Text.text = "���� ��ȣ�� '�����'�� ��븦 ������ ����µ� Ưȭ�� �ΰ��Դϴ�.";
                break;
            case 2:
                Loading_Text.text = "�ΰ��±����� �ΰ��� �ɷ�ġ�� ���� ��½�ų �� �ֽ��ϴ�.";
                break;
            case 3:
                Loading_Text.text = "'Ű�޶�'�� ���ä�� ���� ���� ���� �̻� ��������ϴ�.";
                break;
        }


    }

}
