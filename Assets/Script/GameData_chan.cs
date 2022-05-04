using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData //Chan
{


    //예시입니다
    //public partial class playerdata
    //{
    //    public int HP = 0;
    //    public int MP = 0;
    //    public Player player;
    //    public Monster monster;
    //}

    public partial class PlayerData
    {
        [System.Serializable]
        public struct Stats
        {
            public float HP; // 체력

            public float ATK; // 공격력 
            public float DEF; // 방어력 
        }

    }

}


//public struct Player
//{

//    public string name;
//    public int dex;

//}

//public struct Monster
//{

//    public string name;
//    public int dex;

//}