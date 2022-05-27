using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Mission : MonoBehaviour
{
    public static int CurClearMission=0;
    public int missionIndex=0;
    public MissionDB myDB;
    public Button mybutton;
    public GameObject[] Reward_frame;
    public Sprite[] Reward_Icon;
    public TMPro.TMP_Text MissionName;
    public TMPro.TMP_Text MissionGoal;
    public bool updateCheck = false;
    string curProcess;
    
    private UnityAction[] result = new UnityAction[2];
    // Start is called before the first frame update
    void Start()
    {
        startSettig(missionIndex);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateSettig(missionIndex);
    }



    void startSettig(int index)
    {
        MissionName.text = myDB.myData[index].Mission_Name;
        if (Reward_Icon[0] != null)
        {
            Reward_frame[0].SetActive(true);
            Reward_frame[0].transform.GetChild(0).GetComponent<Image>().sprite = Reward_Icon[0];
        }
        if (Reward_Icon[1] != null)
        {
            Reward_frame[1].SetActive(true);
            Reward_frame[1].transform.GetChild(0).GetComponent<Image>().sprite = Reward_Icon[1];
        }
        if (myDB.myData[index].Mission_GoalProcess < myDB.myData[index].Mission_Goal)
        {
            mybutton.GetComponentInChildren<TMPro.TMP_Text>().text = "바로가기";
            mybutton.onClick.AddListener(myDB.myData[index].rewardBt1);
            
        }


    }

    void UpdateSettig(int index)
    {

        

        MissionGoal.text = myDB.myData[index].Mission_GoalProcess.ToString() + " /\n" + myDB.myData[index].Mission_Goal.ToString();
        
        if (myDB.myData[index].Mission_GoalProcess >= myDB.myData[index].Mission_Goal && !updateCheck)
        {
            CurClearMission++;
            mybutton.onClick.RemoveAllListeners();
            mybutton.GetComponentInChildren<TMPro.TMP_Text>().text = "보상받기";

            mybutton.onClick.AddListener(myDB.myData[index].rewardBt2);
            mybutton.onClick.AddListener(() =>
            {
                myDB.myUI.UIClickAuidio();
                mybutton.interactable = false;
                mybutton.GetComponentInChildren<TMPro.TMP_Text>().text = "미션완료";
                CurClearMission--;
            });
               
            
            updateCheck = true;
        }
        
    }
}
