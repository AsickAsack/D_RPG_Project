using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;


[System.Serializable]
public partial class GameData : MonoBehaviour
{
  
    #region Singleton Pattern 
    private static GameData instance = null; //스태틱은 1개 // 모든 싱글톤 개체들이 공유됨 // 외부에서 수정X 유일성확보

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

                    if (File.Exists(Application.persistentDataPath + "/GameData.json"))
                    { 
                       // instance._Load();
                    }

                    
                    
                }
            }
            return instance;
        }
    }
    

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
    public Sprite[] mySprite;

    #region save and load
    public void _save() //저장 함수
    {
        string jdata = JsonConvert.SerializeObject(playerdata);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string format = System.Convert.ToBase64String(bytes);

       //File.WriteAllText(Application.dataPath + "/GameData.json", jdata); //에디터 경로에 저장할때 //암호화 할거면 format
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", format);  // 모바일에 저장할때
    }

    public void _Load() // 불러오기 함수
    {
        
        
        
            string jdata = File.ReadAllText(Application.persistentDataPath + "/GameData.json"); // 모바일에서 불러올때
            //string jdata = File.ReadAllText(Application.dataPath + "/GameData.json");
            byte[] bytes = System.Convert.FromBase64String(jdata);
            string reformat = System.Text.Encoding.UTF8.GetString(bytes);

             playerdata = JsonConvert.DeserializeObject<PlayerData>(reformat);
        


        // 에디터에서 저장 실험해볼때는 이걸로 하세요

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
    public int _gold = 0;

    public bool desertclear = true;
    public int Gold
    {
        get => _gold;
        set
        {
            int OrgGold = _gold;                 
            _gold = value;

            if (OrgGold < _gold) // 돈 벌었을때 
                EarnMoney += ( _gold - OrgGold );
            else if (OrgGold > _gold) // 돈 썼을때
                SpendMoney += ( OrgGold - _gold );


        }
    }
    private int _curEXP = 0;
    public int CurEXP
    {
        get { return _curEXP; }
        set 
        {
            _curEXP = value;
            if (_curEXP >= MaxEXP)
            {
                Level += 1;
                _curEXP = 0;
                MaxEXP *= Level;
            }
        }
         
    }
    private int _MaxEXP = 1000;
    public int MaxEXP
    {
        get { return _MaxEXP; }
        set {_MaxEXP = value;}
    }
    public int Emerald = 0;
    public int Key = 0;
    public bool FirstGame = true;

    public int EarnMoney = 0;
    public int SpendMoney = 0;
    public int FirstExchange = 0;
    public int BuyShop = 0;

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
            ItemName = "HP포션",
            Description = "HP를 50 회복해준다.",
            Mysprite = 4,
            Price = 3500

        };

        Itemdata2[1] = new itemdata2
        {
            itemType2 = ItemType2.Use,
            ItemCode = 001,
            ItemName = "MP포션",
            Description = "MP를 50 회복해준다.",
            Mysprite = 5,
            Price = 4000
        };

        Itemdata2[2] = new itemdata2
        {
            itemType2 = ItemType2.Use,
            ItemCode = 002,
            ItemName = "행운의 두루마리",
            Description = "골드를 가져다주는 행운의 두루마리.",
            Mysprite = 6,
            Price = 50000
        };

        Itemdata2[3] = new itemdata2
        {
            itemType2 = ItemType2.Material,
            ItemCode = 003,
            ItemName = "강철주괴",
            Description = "강화에 필요한 강철주괴.",
            Mysprite = 7,
            Price = 7000
        };
        Itemdata2[4] = new itemdata2
        {
            itemType2 = ItemType2.Material,
            ItemCode = 004,
            ItemName = "토레도의 벨트",
            Description = "보스 토레도가 차고다니는 벨트.",
            Mysprite = 8,
            Price = 8000
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
            ItemName = "강철 건틀릿",
            ATK = 35,
            DEF = 0,
            Upgrade = 0,
            HP = 0,
            Critical = 3,
            Description = "수련자용 싸구려 건틀릿",
            Equipped = false,
            grade = Grade.common,
            Mysprite = 0
        };

        Itemdata[1] = new itemdata
        {
            ItemCode = 001,
            itemType = ItemType.Weapon,
            ItemName = "토끼 건틀릿",
            ATK = 1075,
            DEF = 0,
            Upgrade = 0,
            HP = 0,
            Critical = 25,
            Description = "<#FFC0CB>귀여워지고 싶은가요?</color>",
            Equipped = false,
            grade = Grade.rare,
            Mysprite = 1
        };

        Itemdata[2] = new itemdata
        {
            ItemCode = 002,
            itemType = ItemType.Armor,
            ItemName = "사자털 사파이어 갑옷",
            ATK = 0,
            DEF = 365,
            Upgrade = 0,
            HP = 550,
            Critical = 0,
            Description = "사자털과 <#489CF8>사파이어</color>를 박은 지갑전사의 필수품",
            Equipped = false,
            grade = Grade.legendary,
            Mysprite = 2
        };

        Itemdata[3] = new itemdata
        {
            ItemCode = 003,
            itemType = ItemType.Armor,
            ItemName = "찢어진 가죽옷",
            ATK = 0,
            DEF = 120,
            Upgrade = 0,
            HP = 350,
            Critical = 0,
            Description = "여행을 하느라 좀 찢어져있다.",
            Equipped = false,
            grade = Grade.common,
            Mysprite = 3
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
    //public Sprite Mysprite;
    public int Mysprite;
    public int Price;
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
    //public Sprite Mysprite;
    public int Mysprite;
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