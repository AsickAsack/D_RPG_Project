using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public partial class GameData : MonoBehaviour
{

  
    #region Singleton Pattern / awake함수
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
    public void _save() //저장 함수
    {
        string jdata = JsonConvert.SerializeObject(playerdata);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string format = System.Convert.ToBase64String(bytes);

       // File.WriteAllText(Application.dataPath + "/GameData.json", jdata); //에디터 경로에 저장할때
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", format);  // 모바일에 저장할때
    }

    public void _Load() // 불러오기 함수
    {
        
        
        
            string jdata = File.ReadAllText(Application.persistentDataPath + "/GameData.json"); // 모바일에서 불러올때
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
