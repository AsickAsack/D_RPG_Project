using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PvPManager : MonoBehaviourPunCallbacks
{
    public GameObject[] RoomPanel;
    public GameObject[] Startpos;
    public List<RoomInfo> myRoomList = new List<RoomInfo>();
    RoomInfo room;
    public Canvas LobbyCanvas = null;
    public Canvas RoomCanvas = null;
    public Canvas MainCanvas = null;
    public Canvas ResultCanvas = null;
    int currentRoomIndex = -1;
    public Button CreateButton;
    public GameObject NoticePanel;
    public GameObject[] RoomPlayerName;
    public GameObject[] RoomPlayerImage;
    public GameObject[] RoomPlayerReady;
    public PhotonView myphotonview;
    public GameObject ReadyButton;
   
    
    IEnumerator Start()
    {

        PhotonNetwork.NickName = GameData.Instance.playerdata.Nickname;
        yield return PhotonNetwork.ConnectUsingSettings();
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("서버와 연결 성공!");
        }
        else
        {
            Debug.Log("서버와 연결 실패!");
        }


    }

    public void CreateRoom()
    {
        RoomOptions opt = new RoomOptions();
        opt.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName + " 님의 방", opt);

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("룸리스트 업뎃");
        for (int i = 0; i < roomList.Count; i++)
        {
            var room = myRoomList.Find(x => x.Name.Equals(roomList[i].Name));

            if (room != null)
            {
                int idx = myRoomList.FindIndex(x => x == room);
                myRoomList.RemoveAt(idx);

                if (roomList[i].RemovedFromList == false)
                {
                    myRoomList.Insert(0, roomList[i]);
                }
            }
            else
            {
                myRoomList.Add(roomList[i]);
            }
        }
    }

    public void BackToMain()
    {
        PhotonNetwork.Disconnect();
        SceneLoader.Instance.Loading_LoadScene(2);
    }


    public void ClickLeave()
    {
        MainCanvas.enabled = false;
        ResultCanvas.enabled = false;
        LobbyCanvas.enabled = true;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
    }

   
    public void joinRoom(int index)
    {
        PhotonNetwork.JoinRoom(myRoomList[index].Name);
        LobbyCanvas.enabled = false;
        currentRoomIndex = index;
    }


    private void Update()
    {

        if (PhotonNetwork.InLobby)
        {
            for (int i = 0; i < myRoomList.Count; i++)
            {
                RoomPanel[i].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = myRoomList[i].Name;
                RoomPanel[i].transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = myRoomList[i].PlayerCount + " / " + myRoomList[i].MaxPlayers;
                RoomPanel[i].SetActive(true);

            }
        }

        if (PhotonNetwork.InRoom)
        {
            if (RoomPlayerReady[0].activeSelf && RoomPlayerReady[1].activeSelf)
            {
                myphotonview.RPC("ActiveStartbutton", RpcTarget.AllBuffered, false);

            }
            else
            {
                //myphotonview.RPC("ActiveStartbutton", RpcTarget.AllBuffered, true);
            }
        }
    }

    [PunRPC]
    public void ActiveStartbutton(bool check)
    {
        if(PhotonNetwork.IsMasterClient)
        ReadyButton.SetActive(check);
    }


    public override void OnConnected()
    {
        NoticePanel.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "접속중..";
        NoticePanel.gameObject.SetActive(true);
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        NoticePanel.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "접속 성공!";
        NoticePanel.SetActive(false);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 들옴 ㅋ");
        NoticePanel.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "접속 성공!";
        NoticePanel.SetActive(false);
        CreateButton.interactable = true;
    }

    public override void OnJoinedRoom()
    {

        
        NoticePanel.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "접속 성공!";
        NoticePanel.SetActive(false);
        Debug.Log("방에 조인함 ㅋ");
        
       myphotonview.RPC("RoomSetting", RpcTarget.All);
        RoomCanvas.enabled = true;
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            PhotonNetwork.Instantiate("MTPlayer", Startpos[0].transform.position, Startpos[0].transform.rotation);
        }
        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            PhotonNetwork.Instantiate("MTPlayer", Startpos[1].transform.position, Quaternion.Euler(Startpos[1].transform.rotation.x, Startpos[1].transform.rotation.y - 180.0f, Startpos[1].transform.rotation.z));
        }

    }



    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }

    public void ClickReadyButton()
    {
        if (myphotonview.IsMine && !mybool)
        {
            photonView.RPC("Ready1", RpcTarget.AllBuffered, true);
            mybool = true;
        }
        else if (myphotonview.IsMine && mybool)
        {
            photonView.RPC("Ready1", RpcTarget.AllBuffered, false);
            mybool = false;
        }

        if (!myphotonview.IsMine && !mybool)
        {
            photonView.RPC("Ready2", RpcTarget.AllBuffered, true);
            mybool = true;
        }
        else if (!myphotonview.IsMine && mybool)
        {
            photonView.RPC("Ready2", RpcTarget.AllBuffered, false);
            mybool = false;
        }

    }

    bool mybool = false;

    [PunRPC]
    public void Ready1(bool check)
    {
        RoomPlayerReady[0].SetActive(check);
    }

    [PunRPC]
    public void Ready2(bool check)
    {
        RoomPlayerReady[1].SetActive(check);
    }

    [PunRPC]
    public void RoomSetting()
    {

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            RoomPlayerName[0].GetComponent<TMPro.TMP_Text>().text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
            RoomPlayerName[0].SetActive(true);
            RoomPlayerImage[0].SetActive(true);

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            RoomPlayerName[0].GetComponent<TMPro.TMP_Text>().text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
            RoomPlayerName[0].SetActive(true);
            RoomPlayerImage[0].SetActive(true);

            RoomPlayerName[1].GetComponent<TMPro.TMP_Text>().text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
            RoomPlayerName[1].SetActive(true);
            RoomPlayerImage[1].SetActive(true);
        }

    }

    [PunRPC]
    public void SetRoomleft()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 0)
        {
            RoomPlayerName[0].SetActive(false);
            RoomPlayerImage[0].SetActive(false);
            RoomPlayerReady[0].SetActive(false);
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {

            RoomPlayerName[1].SetActive(false);
            RoomPlayerImage[1].SetActive(false);
            RoomPlayerReady[1].SetActive(false);
        }
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        myphotonview.RPC("SetRoomleft", RpcTarget.AllBuffered);
    }

    public void StartButton()
    {
        
            myphotonview.RPC("StartMatch", RpcTarget.All);
            
        
    }


    [PunRPC]
    public void StartMatch()
    {
        RoomCanvas.enabled = false;
        LobbyCanvas.enabled = false;
        MainCanvas.enabled = true;
        
    }



 



    /*
       if(PhotonNetwork.LocalPlayer.ActorNumber==1)
       {
           PhotonNetwork.Instantiate("MTPlayer", Startpos[0].transform.position, Startpos[0].transform.rotation);
       }
       if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
       {
           PhotonNetwork.Instantiate("MTPlayer", Startpos[1].transform.position, Quaternion.Euler(Startpos[1].transform.rotation.x, Startpos[1].transform.rotation.y - 180.0f, Startpos[1].transform.rotation.z));
       }

       */
}
