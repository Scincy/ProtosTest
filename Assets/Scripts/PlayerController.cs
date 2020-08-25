using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    CharactorContorller charactor;
    public GameObject playerPrefab;
    
    public Image selectedKeyUI;
    public Image selectedActionUI;
    public Sprite[] keyUI;
    public string[] actions;
    public Sprite[] actionUI;
    protected Dictionary<string, Sprite> actionUIDict;
    protected private string[] keyboard = { "q", "w", "e", "r", "a", "s", "d", "f" };
    protected private KeyCode[] keycodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };
    protected private KeyCode keycode;

    private void Awake()
    {
        actionUIDict = new Dictionary<string, Sprite>();
        for (int i = 0; i < actions.Length; i++)
            actionUIDict.Add(actions[i], actionUI[i]);
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, GameObject.FindGameObjectWithTag("RegenPoint").transform.position, Quaternion.identity);
        }
    }
    
    void Update()
    {
        if (charactor == null)
        {
            GameObject objectFound = GameObject.FindGameObjectWithTag("Player");
            if (objectFound != null)
            {
                charactor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharactorContorller>();
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                charactor.SetKey(PhotonNetwork.LocalPlayer.ActorNumber, Input.GetKey(keycode));
            }
            else
            {
                PhotonView.Get(charactor).RPC("SetKey", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber, Input.GetKey(keycode));
            }
        }
    }

    public void SetRandomKeySelection()
    {
        int randomKey = Random.Range(0, keyboard.Length);
        selectedKeyUI.sprite = keyUI[randomKey];
        keycode = keycodes[randomKey];
    }

    public void SetAction(string action)
    {
        selectedActionUI.sprite = actionUIDict[action];
    }
}
