using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class DialogManager : MonoBehaviour
{
    public GameObject dialog;
    public Text textUI;

    public void Show(string text)
    {
        textUI.text = text;
        dialog.SetActive(true);
    }

    public void ToLobby()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }
}
