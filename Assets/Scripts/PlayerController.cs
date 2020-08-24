using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    CharactorContorller charactor;
    MultiplayManager multiplay;
    PhotonView photonView;

    protected private string[] actions = { "right", "left", "jump", "down" };

    public string action;

    private void Awake()
    {
        charactor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharactorContorller>();
    }

    void Start()
    {
        photonView = PhotonView.Get(this);
    }
    
    void Update()
    {
        charactor.Move(Input.GetKey(KeyCode.Space), Input.GetKeyUp(KeyCode.Space), Input.GetKeyDown(KeyCode.Space), action);
    }
}
