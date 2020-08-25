using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ServerConnect : MonoBehaviourPunCallbacks
{
    public byte MaxPlayersPerRoom;
    private LobbyManager lobby;

    private void Awake()
    {
        lobby = GetComponent<LobbyManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        string nickname = lobby.GetNickname();
        if (nickname == "")
        {
            lobby.Log("닉네임을 입력해주세요!");
        }
        else
        {
            lobby.Log(nickname + "님으로 접속 중...");
            lobby.SetUIActive(false);
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NickName = nickname;
        }
    }
    
    public override void OnConnectedToMaster()
    {
        lobby.Log("서버 연결 성공. 아무 방이나 들어가는 중...");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        lobby.Log("들어갈 수 있는 방이 없습니다. 방 생성 중...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }
    
    public override void OnJoinedRoom()
    {
        lobby.Log("방에 들어왔습니다! (#" + PhotonNetwork.CurrentRoom.masterClientId + ") 전체 인원 " + MaxPlayersPerRoom + "명이 모이면 게임이 시작됩니다. (" + PhotonNetwork.CurrentRoom.PlayerCount + "/" + MaxPlayersPerRoom + ")");
        CheckPlayers();
    }
    
    public override void OnCreatedRoom()
    {
        lobby.Log("방을 생성했습니다!");
        CheckPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        lobby.Log(newPlayer.NickName + "님이 들어왔습니다! (" + PhotonNetwork.CurrentRoom.PlayerCount + "/" + MaxPlayersPerRoom + ")");
        CheckPlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        lobby.Log(otherPlayer.NickName + "님이 나갔습니다! (" + PhotonNetwork.CurrentRoom.PlayerCount + "/" + MaxPlayersPerRoom + ")");
        CheckPlayers();
    }

    private void CheckPlayers()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            SceneManager.LoadScene("GameScene_1");
        }
    }
}
