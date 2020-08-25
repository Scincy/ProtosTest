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
    
    public Image selectedActionUI;
    public string[] actions;
    public Sprite[] actionUI;
    protected Dictionary<string, Sprite> actionUIDict;

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
                charactor.SetKey(PhotonNetwork.LocalPlayer.ActorNumber, Input.GetKey(KeyCode.Space));
            }
            else
            {
                PhotonView.Get(charactor).RPC("SetKey", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber, Input.GetKey(KeyCode.Space));
            }
        }
    }

    public void SetAction(string action)
    {
        selectedActionUI.sprite = actionUIDict[action];
    }
}
