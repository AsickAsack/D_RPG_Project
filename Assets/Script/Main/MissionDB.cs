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
    public string reward_tx;

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
            rewardBt1 = () => myUI.ShopPopup_Open(),
            rewardBt2 = () => GameData.Instance.playerdata.Gold += 50000,
            reward_tx = "5만 골드 획득 완료!"
        };
        myData[1] = new MissionData
        {
            Mission_Name = "100,000 골드 얻기",
            Mission_GoalProcess = GameData.Instance.playerdata.EarnMoney,
            Mission_Goal = 100000,
            rewardBt1 = () => SceneLoader.Instance.Loading_LoadScene("pSelecteDungeon"),
            rewardBt2 = () => GameData.Instance.playerdata.Gold += 50000,
            reward_tx = "5만 골드 획득 완료!"
        };
        myData[2] = new MissionData
        {
            Mission_Name = "Stage 1-1 깨기",
            Mission_GoalProcess = 0,//횟수 연동해야함
            Mission_Goal = 1,
            rewardBt1 = () => SceneLoader.Instance.Loading_LoadScene("pSelecteDungeon"),
            rewardBt2 = () => {/*보상 정해야함 ㅋㅋ*/},
            reward_tx = ""//보상팝업설명
        };
        myData[3] = new MissionData
        {
            Mission_Name = "에메랄드 환전해보기",
            Mission_GoalProcess = GameData.Instance.playerdata.FirstExchange,//횟수 연동해야함
            Mission_Goal = 1,
            rewardBt1 = () => myUI.Open_CashShop(),
            rewardBt2 = () => {/*보상 정해야함 ㅋㅋ*/},
            reward_tx = ""//보상팝업설명
        };
        myData[4] = new MissionData
        {
            Mission_Name = "상점에서 아이템 3개 사보기",
            Mission_GoalProcess = GameData.Instance.playerdata.BuyShop,//횟수 연동해야함
            Mission_Goal = 3,
            rewardBt1 = () => myUI.ShopPopup_Open(),
            rewardBt2 = () => {/*보상 정해야함 ㅋㅋ*/},
            reward_tx = ""//보상팝업설명
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
