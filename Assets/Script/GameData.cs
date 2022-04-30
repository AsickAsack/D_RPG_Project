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
                    

                    if(File.Exists(Application.dataPath + "/GameData.json"))
                    { 
                     instance._Load();
                    }
                   
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
    //        if (instance == null) // 인스턴스가 비어있는지 검사
    //        {
    //            var obj = FindObjectOfType<GameData>(); //씬안에 jGameManager 컴포넌트를 가지고있는 오브젝트가 있는지 검사
    //            if (obj != null) //만약 jGameManager 컴포넌트를 가지고있는 오브젝트가 존재한다면
    //            {
    //                instance = obj; // 인스턴스에 그 객체를 넣어줌 
    //            }
    //            else //인스턴스가 존재하지 않는다면
    //            {
    //                var newObj = new GameObject().AddComponent<GameData>(); ; // 새 게임오브젝트 생성하고 jgamemanager를 붙여줌
    //                newObj.name = "GameData";
    //                instance = newObj; // 그 게임오브젝트를 인스턴스에 넣어줌

    //            }

    //        }
    //        return instance;
    //    }
    //}

    //private void Awake() //게임오브젝트가 생성되면 가장먼저 실행
    //{
    //    var objs = FindObjectsOfType<GameData>();//씬에 같은 컴포넌트를 가진 오브젝트가 몇개가 있는지 검사
    //    if (objs.Length != 1) // 1개가 아니라면 = 다른 오브젝트가 있다는 의미 , 이 객체보다 먼저 생성된 객체일 확률이 높음
    //    {
    //        Destroy(gameObject); //방금 생성된 객체는 파괴
    //        return;
    //    }
    //    DontDestroyOnLoad(gameObject); //씬이 바뀌어도 게임오브젝트가 파괴되지 않도록함


    //if (File.Exists(Application.persistentDataPath + "/GameData.json"))
    //{
    //    _Load();
    //}


    //}
    #endregion

    private void Awake()
    {

        if(!File.Exists(Application.dataPath + "/GameData.json"))
        {
            playerdata.Itemdata_Initialize();
        }
        
        

       

    }

    private void Start()
    {  
     

        
    }

    private void Update()
    {

    }


    public PlayerData playerdata;
    
 

    #region save and load
    public void _save() //저장 함수
    {
        string jdata = JsonConvert.SerializeObject(playerdata);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);

       File.WriteAllText(Application.dataPath + "/GameData.json", jdata); //에디터 경로에 저장할때 //암호화 할거면 format
        //File.WriteAllText(Application.persistentDataPath + "/GameData.json", jdata);  // 모바일에 저장할때
    }

    public void _Load() // 불러오기 함수
    {
        
        
        
            //string jdata = File.ReadAllText(Application.persistentDataPath + "/GameData.json"); // 모바일에서 불러올때
            string jdata = File.ReadAllText(Application.dataPath + "/GameData.json"); // 모바일에서 불러올때
            //byte[] bytes = System.Convert.FromBase64String(jdata);
            //string reformat = System.Text.Encoding.UTF8.GetString(bytes);

             playerdata = JsonConvert.DeserializeObject<PlayerData>(jdata);
        


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
    public int Gold = 0;
    public int Emerald = 0;
    public bool FirstGame = true;
    public UnityEngine.U2D.SpriteAtlas ItemImage;

    public List<itemdata> Player_inventory = new List<itemdata>();
    public itemdata[] Itemdata = new itemdata[4];


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
            Mysprite = ItemImage.GetSprite("Iron Gauntlet")
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
            Description = "귀여워지고 싶은가요?",
            Equipped = false,
            grade = Grade.rare,
            Mysprite = ItemImage.GetSprite("BunnyGauntlet")
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
            Description = "사자 털에 에메랄드를 박은 지갑전사의 필수품",
            Equipped = false,
            grade = Grade.legendary,
            Mysprite = ItemImage.GetSprite("Armor1")
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
            Mysprite = ItemImage.GetSprite("Armor3")
        };




    }

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