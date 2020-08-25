using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Text serverText;
    public Button startButton;
    public InputField nicknameField;

    public void SetUIActive(bool v)
    {
        startButton.interactable = v;
        nicknameField.interactable = v;
    }

    public string GetNickname()
    {
        return nicknameField.text;
    }

    public void Log(string line)
    {
        serverText.text += "\n" + line;
    }

    public void Connect()
    {
        GetComponent<ServerConnect>().Connect();
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
    }
}
