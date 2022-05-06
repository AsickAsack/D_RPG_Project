using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;


[System.Serializable]
public partial class GameData : MonoBehaviour
{
    public int a = 0;
  
    #region Singleton Pattern 
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
                    DontDestroyOnLoad(obj);

                    //if (File.Exists(Application.dataPath + "/GameData.json"))
                    //{ 
                    //    instance._Load();
                    //}

                    
                    
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

    private void Awake()
    {
        playerdata.Itemdata_Initialize();
        playerdata.Itemdata2_Initialize();
    }

    private void Start()
    {

      

    }

    private void Update()
    {
        
    }

    public int Upgrade_Money = 10000;
    public int Upgrade_chance = 80;


    public PlayerData playerdata;
   

    #region save and load
    public void _save() //���� �Լ�
    {
        string jdata = JsonConvert.SerializeObject(playerdata);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);

       File.WriteAllText(Application.dataPath + "/GameData.json", jdata); //������ ��ο� �����Ҷ� //��ȣȭ �ҰŸ� format
        //File.WriteAllText(Application.persistentDataPath + "/GameData.json", jdata);  // ����Ͽ� �����Ҷ�
    }

    public void _Load() // �ҷ����� �Լ�
    {
        
        
        
            //string jdata = File.ReadAllText(Application.persistentDataPath + "/GameData.json"); // ����Ͽ��� �ҷ��ö�
            string jdata = File.ReadAllText(Application.dataPath + "/GameData.json"); // ����Ͽ��� �ҷ��ö�
            //byte[] bytes = System.Convert.FromBase64String(jdata);
            //string reformat = System.Text.Encoding.UTF8.GetString(bytes);

             playerdata = JsonConvert.DeserializeObject<PlayerData>(jdata);
        


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


[System.Serializable]
public partial class PlayerData
{
    public string Nickname = "";
    public int Level = 1;
    public int Gold = 0;
    public int Emerald = 0;
    public bool FirstGame = true;
    public UnityEngine.U2D.SpriteAtlas ItemImage;
    public UnityEngine.U2D.SpriteAtlas ItemImage2;

    public List<itemdata> Player_inventory = new List<itemdata>();
    public List<itemdata2> Player_inventory2 = new List<itemdata2>();
    public itemdata[] Itemdata = new itemdata[4];
    public itemdata2[] Itemdata2 = new itemdata2[5];

    public void Itemdata2_Initialize()
    {

        Itemdata2[0] = new itemdata2
        {
            itemType2 = ItemType2.Use,
            ItemCode = 000,
            ItemName = "HP����",
            Description = "HP�� 50 ȸ�����ش�.",
            Mysprite = ItemImage2.GetSprite("hp"),

        };

        Itemdata2[1] = new itemdata2
        {
            itemType2 = ItemType2.Use,
            ItemCode = 001,
            ItemName = "MP����",
            Description = "MP�� 50 ȸ�����ش�.",
            Mysprite = ItemImage2.GetSprite("mp"),
        };

        Itemdata2[2] = new itemdata2
        {
            itemType2 = ItemType2.Use,
            ItemCode = 002,
            ItemName = "����� �η縶��",
            Description = "��带 �������ִ� ����� �η縶��.",
            Mysprite = ItemImage2.GetSprite("scroll"),
        };

        Itemdata2[3] = new itemdata2
        {
            itemType2 = ItemType2.Material,
            ItemCode = 003,
            ItemName = "��ö�ֱ�",
            Description = "��ȭ�� �ʿ��� ��ö�ֱ�.",
            Mysprite = ItemImage2.GetSprite("ingots"),
        };
        Itemdata2[4] = new itemdata2
        {
            itemType2 = ItemType2.Material,
            ItemCode = 004,
            ItemName = "�䷹���� ��Ʈ",
            Description = "���� �䷹���� ����ٴϴ� ��Ʈ.",
            Mysprite = ItemImage2.GetSprite("belts"),
        };

    }

        public void chanegeUpgrade(List<itemdata> itemdatas, int index, int value)
    {
        itemdata temp = itemdatas[index];
        temp.Upgrade += value;
        itemdatas[index] = temp;
    }

    public void UpgradeWeapon(List<itemdata> itemdatas, int index, int value1, int value2)
    {
        itemdata temp = itemdatas[index];
        temp.ATK += value1;
        temp.Critical += value2;
        itemdatas[index] = temp;
    }

    public void UpgradeArmor(List<itemdata> itemdatas, int index, int value1, int value2)
    {
        itemdata temp = itemdatas[index];
        temp.DEF += value1;
        temp.HP += value2;
        itemdatas[index] = temp;
    }

    public void Itemdata_Initialize()
    {

        Itemdata[0] = new itemdata
        {
            ItemCode = 000,
            itemType = ItemType.Weapon,
            ItemName = "��ö ��Ʋ��",
            ATK = 35,
            DEF = 0,
            Upgrade = 0,
            HP = 0,
            Critical = 3,
            Description = "�����ڿ� �α��� ��Ʋ��",
            Equipped = false,
            grade = Grade.common,
            Mysprite = ItemImage.GetSprite("Iron Gauntlet")
        };

        Itemdata[1] = new itemdata
        {
            ItemCode = 001,
            itemType = ItemType.Weapon,
            ItemName = "�䳢 ��Ʋ��",
            ATK = 1075,
            DEF = 0,
            Upgrade = 0,
            HP = 0,
            Critical = 25,
            Description = "<#FFC0CB>�Ϳ������� ��������?</color>",
            Equipped = false,
            grade = Grade.rare,
            Mysprite = ItemImage.GetSprite("BunnyGauntlet")
        };

        Itemdata[2] = new itemdata
        {
            ItemCode = 002,
            itemType = ItemType.Armor,
            ItemName = "������ �����̾� ����",
            ATK = 0,
            DEF = 365,
            Upgrade = 0,
            HP = 550,
            Critical = 0,
            Description = "�����а� <#489CF8>�����̾�</color>�� ���� ���������� �ʼ�ǰ",
            Equipped = false,
            grade = Grade.legendary,
            Mysprite = ItemImage.GetSprite("Armor1")
        };

        Itemdata[3] = new itemdata
        {
            ItemCode = 003,
            itemType = ItemType.Armor,
            ItemName = "������ ���׿�",
            ATK = 0,
            DEF = 120,
            Upgrade = 0,
            HP = 350,
            Critical = 0,
            Description = "������ �ϴ��� �� �������ִ�.",
            Equipped = false,
            grade = Grade.common,
            Mysprite = ItemImage.GetSprite("Armor3")
        };


    }

}

[System.Serializable]
public struct itemdata2
{

    public ItemType2 itemType2;
    public int ItemCode;
    public string ItemName;
    public string Description;
    public Sprite Mysprite;

}

[System.Serializable]
public enum ItemType2
{
    None=0,Use,Material
}


[System.Serializable]
public struct itemdata
{
    public int ItemCode;
    public ItemType itemType;
    public string ItemName;
    public int ATK;
    public int DEF;
    public int Upgrade;
    public int HP;
    public int Critical;
    public string Description;
    public bool Equipped;
    public Grade grade;
    public Sprite Mysprite;
}

[System.Serializable]
public enum ItemType
{
    Weapon = 0, Armor, etc,none
}


[System.Serializable]
public enum Grade
{
    common = 0, rare, epic, legendary
}