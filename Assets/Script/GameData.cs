using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public partial class GameData : MonoBehaviour
{

  
    #region Singleton Pattern / awake�Լ�
    private static GameData instance = null; //����ƽ�� 1�� // ��� �̱��� ��ü���� ������ // �ܺο��� ����X ���ϼ�Ȯ��

    public static GameData Instance
    {
        get
        { 
            if (instance == null)
            {
                instance = FindObjectOfType<GameData>();
                if (instance == null)
                {
                    GameObject obj = Instantiate(Resources.Load("GameData")) as GameObject;
                    obj.name = typeof(GameData).ToString();
                    instance = obj.GetComponent<GameData>();

                    if (File.Exists(Application.persistentDataPath + "/GameData.json"))
                     instance._Load();
                  
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    //public static GameData Instance
    //{
    //    get
    //    {
    //        if (instance == null) // �ν��Ͻ��� ����ִ��� �˻�
    //        {
    //            var obj = FindObjectOfType<GameData>(); //���ȿ� jGameManager ������Ʈ�� �������ִ� ������Ʈ�� �ִ��� �˻�
    //            if (obj != null) //���� jGameManager ������Ʈ�� �������ִ� ������Ʈ�� �����Ѵٸ�
    //            {
    //                instance = obj; // �ν��Ͻ��� �� ��ü�� �־��� 
    //            }
    //            else //�ν��Ͻ��� �������� �ʴ´ٸ�
    //            {
    //                var newObj = new GameObject().AddComponent<GameData>(); ; // �� ���ӿ�����Ʈ �����ϰ� jgamemanager�� �ٿ���
    //                newObj.name = "GameData";
    //                instance = newObj; // �� ���ӿ�����Ʈ�� �ν��Ͻ��� �־���

    //            }

    //        }
    //        return instance;
    //    }
    //}

    //private void Awake() //���ӿ�����Ʈ�� �����Ǹ� ������� ����
    //{
    //    var objs = FindObjectsOfType<GameData>();//���� ���� ������Ʈ�� ���� ������Ʈ�� ��� �ִ��� �˻�
    //    if (objs.Length != 1) // 1���� �ƴ϶�� = �ٸ� ������Ʈ�� �ִٴ� �ǹ� , �� ��ü���� ���� ������ ��ü�� Ȯ���� ����
    //    {
    //        Destroy(gameObject); //��� ������ ��ü�� �ı�
    //        return;
    //    }
    //    DontDestroyOnLoad(gameObject); //���� �ٲ� ���ӿ�����Ʈ�� �ı����� �ʵ�����


    //if (File.Exists(Application.persistentDataPath + "/GameData.json"))
    //{
    //    _Load();
    //}


    //}
    #endregion


    private void Start()
    {
        
    }
   

  [System.Serializable]
    public partial class PlayerData
    {
        public string Nickname = "";
        public int Level = 1;
        public int Gold = 0;
        public int Emerald = 0;
        public bool FirstGame = true;

    }

    public PlayerData playerdata;

 

    #region save and load
    public void _save() //���� �Լ�
    {
        string jdata = JsonConvert.SerializeObject(playerdata);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string format = System.Convert.ToBase64String(bytes);

       // File.WriteAllText(Application.dataPath + "/GameData.json", jdata); //������ ��ο� �����Ҷ�
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", format);  // ����Ͽ� �����Ҷ�
    }

    public void _Load() // �ҷ����� �Լ�
    {
        
        
        
            string jdata = File.ReadAllText(Application.persistentDataPath + "/GameData.json"); // ����Ͽ��� �ҷ��ö�
            byte[] bytes = System.Convert.FromBase64String(jdata);
            string reformat = System.Text.Encoding.UTF8.GetString(bytes);

        playerdata = JsonConvert.DeserializeObject<PlayerData>(reformat);
        


        // �����Ϳ��� ���� �����غ����� �̰ɷ� �ϼ���

        //if (File.Exists(Application.dataPath + "/GameData.json"))
        //{
        //    string jdata = File.ReadAllText(Application.dataPath + "/GameData.json");
        //    byte[] bytes = System.Convert.FromBase64String(jdata);
        //    string reformat = System.Text.Encoding.UTF8.GetString(bytes);

        //    itemdata = JsonConvert.DeserializeObject<ItemData>(reformat);
        //}
    }
    #endregion


}
