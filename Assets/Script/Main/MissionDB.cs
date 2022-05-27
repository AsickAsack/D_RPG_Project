using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct MissionData
{
    
    public string Mission_Name;
    public int Mission_GoalProcess;
    public int Mission_Goal;
    public UnityAction rewardBt1;
    public UnityAction rewardBt2;
    public int reward_tx1;
    public int reward_tx2;

}
[System.Serializable]
public class MissionDB : MonoBehaviour
{

    public MissionData[] myData = new MissionData[5];
    public JPopUpCanvas myUI;

    
    private void Awake()
    {
        myData[0] = new MissionData
        {
            Mission_Name = "50,000 골드 사용하기",
            Mission_GoalProcess = GameData.Instance.playerdata.SpendMoney,
            Mission_Goal = 50000,
            rewardBt1 = () => {
                myUI.ExitPopUp(true);
                myUI.ShopPopup_Open();
            },
            rewardBt2 = () =>
            {
                GameData.Instance.playerdata.Gold += 50000;
                GameData.Instance.playerdata.CurEXP += 500;
            },
            reward_tx1 = 50000,
            reward_tx2 = 500,
        };
        myData[1] = new MissionData
        {
            Mission_Name = "100,000 골드 얻기",
            Mission_GoalProcess = GameData.Instance.playerdata.EarnMoney,
            Mission_Goal = 100000,
            rewardBt1 = () => SceneLoader.Instance.Loading_LoadScene("pSelecteDungeon"),
            rewardBt2 = () =>
            {
                GameData.Instance.playerdata.Gold += 50000;
                GameData.Instance.playerdata.CurEXP += 500;
            },
            reward_tx1 = 50000,
            reward_tx2 = 500,
        };
        myData[2] = new MissionData
        {
            Mission_Name = "Stage 1-1 깨기",
            Mission_GoalProcess = 0,//횟수 연동해야함
            Mission_Goal = 1,
            rewardBt1 = () => SceneLoader.Instance.Loading_LoadScene("pSelecteDungeon"),
            rewardBt2 = () =>
            {
                GameData.Instance.playerdata.Player_inventory2.Add(GameData.Instance.playerdata.Itemdata2[2]);
                GameData.Instance.playerdata.CurEXP += 1500;
            },
            reward_tx1 = 50000,
            reward_tx2 = 1500,
        };
        myData[3] = new MissionData
        {
            Mission_Name = "에메랄드 환전해보기",
            Mission_GoalProcess = GameData.Instance.playerdata.FirstExchange,//횟수 연동해야함
            Mission_Goal = 1,
            rewardBt1 = () => {
                myUI.ExitPopUp(true);
                myUI.Open_CashShop();
            },
            rewardBt2 = () =>
            {
                GameData.Instance.playerdata.Emerald += 5000;
                GameData.Instance.playerdata.CurEXP += 500;
            },
            reward_tx1 = 5000,
            reward_tx2 = 500,
        };
        myData[4] = new MissionData
        {
            Mission_Name = "상점아이템 3개 구매",
            Mission_GoalProcess = GameData.Instance.playerdata.BuyShop,//횟수 연동해야함
            Mission_Goal = 3,
            rewardBt1 = () => {
                myUI.ExitPopUp(true);
                myUI.ShopPopup_Open(); },
            rewardBt2 = () =>
            {
                GameData.Instance.playerdata.Gold += 70000;
                GameData.Instance.playerdata.CurEXP += 900;
            },
            reward_tx1 = 70000,
            reward_tx2 = 900,
        };
    }

    
    private void Update()
    {
        myData[0].Mission_GoalProcess = GameData.Instance.playerdata.SpendMoney;
        myData[1].Mission_GoalProcess = GameData.Instance.playerdata.EarnMoney;
        //myData[2].Mission_GoalProcess = GameData.Instance.playerdata.SpendMoney;
        myData[3].Mission_GoalProcess = GameData.Instance.playerdata.FirstExchange;
        myData[4].Mission_GoalProcess = GameData.Instance.playerdata.BuyShop;

    }



}
