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
    enum ButtonType { Regen, Lobby }
    ButtonType type;

    public void ShowRegenDialog(string text)
    {
        textUI.text = text;
        dialog.SetActive(true);
        type = ButtonType.Regen;
    }
    
    public void ShowLobbyDialog(string text)
    {
        textUI.text = text;
        dialog.SetActive(true);
        type = ButtonType.Lobby;
    }

    public void ToLobby()
    {
        if (type == ButtonType.Regen)
        {
            dialog.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharactorContorller>().Kill();
        }
        else if (type == ButtonType.Lobby)
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("Lobby");
        }
    }
}
