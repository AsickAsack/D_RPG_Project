using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData //Chan
{


    //�����Դϴ�
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
            public float HP; // ü��

            public float ATK; // ���ݷ� 
            public float DEF; // ���� 
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