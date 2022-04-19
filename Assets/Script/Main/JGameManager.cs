using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGameManager : MonoBehaviour
{

    private static JGameManager instance; //����ƽ�� 1�� // ��� �̱��� ��ü���� ������ // �ܺο��� ����X ���ϼ�Ȯ��

    public static JGameManager Instance 
    {
        get
        {
            if(instance == null) // �ν��Ͻ��� ����ִ��� �˻�
            {
                var obj = FindObjectOfType<JGameManager>(); //���ȿ� jGameManager ������Ʈ�� �������ִ� ������Ʈ�� �ִ��� �˻�
                if(obj != null) //���� jGameManager ������Ʈ�� �������ִ� ������Ʈ�� �����Ѵٸ�
                {
                    instance = obj; // �ν��Ͻ��� �� ��ü�� �־��� 
                }
                else //�ν��Ͻ��� �������� �ʴ´ٸ�
                {
                    var newObj = new GameObject().AddComponent<JGameManager>(); // �� ���ӿ�����Ʈ �����ϰ� jgamemanager�� �ٿ���
                    instance = newObj; // �� ���ӿ�����Ʈ�� �ν��Ͻ��� �־���
                }
            }
            return instance;
        }
    }

    private void Awake() //���ӿ�����Ʈ�� �����Ǹ� ������� ����
    {
        var objs = FindObjectsOfType<JGameManager>();//���� ���� ������Ʈ�� ���� ������Ʈ�� ��� �ִ��� �˻�
        if(objs.Length != 1) // 1���� �ƴ϶�� = �ٸ� ������Ʈ�� �ִٴ� �ǹ� , �� ��ü���� ���� ������ ��ü�� Ȯ���� ����
        {
            Destroy(gameObject); //��� ������ ��ü�� �ı�
            return;
        }
        DontDestroyOnLoad(gameObject); //���� �ٲ� ���ӿ�����Ʈ�� �ı����� �ʵ�����
    }

    public struct Player_Stat
    {
        string Nickname;
        int Money;
        int Crystal;

    }

    Player_Stat player_stat;

    void Start()
    {
        player_stat = new Player_Stat();
        
    }

    
    void Update()
    {
        
    }
}
