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
            Mission_Name = "50,000 ��� ����ϱ�",
            Mission_GoalProcess = GameData.Instance.playerdata.SpendMoney,
            Mission_Goal = 50000,
            rewardBt1 = () => myUI.ShopPopup_Open(),
            rewardBt2 = () => GameData.Instance.playerdata.Gold += 50000,
            reward_tx = "5�� ��� ȹ�� �Ϸ�!"
        };
        myData[1] = new MissionData
        {
            Mission_Name = "100,000 ��� ���",
            Mission_GoalProcess = GameData.Instance.playerdata.EarnMoney,
            Mission_Goal = 100000,
            rewardBt1 = () => SceneLoader.Instance.Loading_LoadScene("pSelecteDungeon"),
            rewardBt2 = () => GameData.Instance.playerdata.Gold += 50000,
            reward_tx = "5�� ��� ȹ�� �Ϸ�!"
        };
        myData[2] = new MissionData
        {
            Mission_Name = "Stage 1-1 ����",
            Mission_GoalProcess = 0,//Ƚ�� �����ؾ���
            Mission_Goal = 1,
            rewardBt1 = () => SceneLoader.Instance.Loading_LoadScene("pSelecteDungeon"),
            rewardBt2 = () => {/*���� ���ؾ��� ����*/},
            reward_tx = ""//�����˾�����
        };
        myData[3] = new MissionData
        {
            Mission_Name = "���޶��� ȯ���غ���",
            Mission_GoalProcess = GameData.Instance.playerdata.FirstExchange,//Ƚ�� �����ؾ���
            Mission_Goal = 1,
            rewardBt1 = () => myUI.Open_CashShop(),
            rewardBt2 = () => {/*���� ���ؾ��� ����*/},
            reward_tx = ""//�����˾�����
        };
        myData[4] = new MissionData
        {
            Mission_Name = "�������� ������ 3�� �纸��",
            Mission_GoalProcess = GameData.Instance.playerdata.BuyShop,//Ƚ�� �����ؾ���
            Mission_Goal = 3,
            rewardBt1 = () => myUI.ShopPopup_Open(),
            rewardBt2 = () => {/*���� ���ؾ��� ����*/},
            reward_tx = ""//�����˾�����
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
