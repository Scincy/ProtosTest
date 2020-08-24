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
    private string UUID = "";

    // Start is called before the first frame update
    void Start()
    {
        setUUID();
        Debug.Log(getUUID());
    }

    /// <summary>
    /// UUID를 설정합니다.
    /// </summary>
    public void setUUID()
    {

        string timeNow = System.DateTime.UtcNow.ToString("yyyyMMdd");

        int cputick = System.Environment.TickCount & System.Int32.MaxValue;
        cputick = cputick % 100000000;
        string tick = cputick.ToString();
        if (tick.Length <= 8)
        {
            int x = 8 - tick.Length;
            for (int i = 0; i < x; i++)
            {
                tick += "0";
            }
        }

        string charSeed = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        char[] randomID = new char[48];
        for (int i = 0; i < 48; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, charSeed.Length);
            randomID[i] = charSeed[randomIndex];
        }
        UUID = timeNow + tick + String.Concat(randomID);
    }
    /// <summary>
    /// UUID를 가져옵니다. UUID가 비어 있다면 새로 만들어서 가져 옵니다.
    /// </summary>
    /// <returns>string형의 64글자 길이의 UUID를 가져 옵니다.</returns>
    public string getUUID()
    {
        if (UUID == null || UUID == "")
        {
            setUUID();
        }
        return UUID;
    }
    public void flushUUID()
    {
        UUID = null;
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom(null, 0);
        Debug.Log("Master!");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Joined Room Failed!");
        Debug.Log(message);
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room!");
        if (PhotonNetwork.CountOfPlayersInRooms == 3)
        {
            SceneManager.LoadScene("GameScene_1");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Joined Player Entered Room!");
        if (PhotonNetwork.CountOfPlayersInRooms == 3)
        {
            SceneManager.LoadScene("GameScene_1");
        }
    }

    #endregion
}
