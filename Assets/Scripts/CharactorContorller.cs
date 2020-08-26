using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharactorContorller : MonoBehaviour, IPunObservable
{
    private Vector3 regenPoint;

	public float jumpForce=300f;
	public float moveSpeed=5f;

    public bool isMapDebugMode = false;

    private Dictionary<string, bool> keyState;
    private Dictionary<string, bool> lastKeyState;
    protected Dictionary<int, string> idToAction;

    private Rigidbody2D charactorBody;
    private Animator charactorAnim;

	protected private string[] actions  = { "right", "left", "jump" };

	void Start()
	{
		charactorBody = GetComponent<Rigidbody2D>();
        charactorAnim = GetComponent<Animator>();

        keyState = new Dictionary<string, bool>();
        lastKeyState = new Dictionary<string, bool>();
        idToAction = new Dictionary<int, string>();

        regenPoint = GameObject.FindGameObjectWithTag("RegenPoint").transform.position;

        foreach (string action in actions)
        {
            keyState.Add(action, false);
            lastKeyState.Add(action, false);
        }
        
        ShuffleIdToAction();

        // Init - Random direction and key number
        //SetRandomKeySelection();
        //SetRandomAction();

        //Debug.Log(actions[randomKeyAction] + " is On " + keyboard[randomKey]);
    }

	// Update is called once per frame
	void FixedUpdate()
	{
        // 이 부분의 조건문을 죽었을 때로 바꾸면 새롭게 키를 배정할 수 있습니다.
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
			ShuffleIdToAction();
		}*/
        if (isMapDebugMode)
		{
			Move();
		}
        else
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            if (GetKey("right"))
            {
                charactorAnim.SetBool("Move",true);
                charactorBody.AddForce(new Vector2(moveSpeed, 0));
            }
            else if (GetKeyUp("right"))
            {
                charactorAnim.SetBool("Move", false);
                charactorBody.velocity = Vector2.zero;
            }

            if (GetKey("left"))
            {
                charactorAnim.SetBool("Move", true);
                charactorBody.AddForce(new Vector2(-moveSpeed, 0));
            }
            else if (GetKeyUp("left"))
            {
                charactorAnim.SetBool("Move", false);
                charactorBody.velocity = Vector2.zero;
            }

            if (GetKeyDown("jump") && charactorBody.velocity.y == 0)
            {
                charactorAnim.SetBool("Move", true);
                charactorBody.AddForce(new Vector2(0, jumpForce));
            }
            else if (GetKeyUp("jump") && charactorBody.velocity.y > 0)
            {
                charactorAnim.SetBool("Move", false);
                // 점프 키에서 손을 때고, 현재 점프중이라면 점프 이동 속도 절반 감소
                charactorBody.velocity = charactorBody.velocity * 0.5f;
            }



            foreach (string action in actions)
            {
                lastKeyState[action] = keyState[action];
            }
        }
	}

    private bool GetKey(string key)
    {
        return keyState[key];
    }

    private bool GetKeyDown(string key)
    {
        return keyState[key] && !lastKeyState[key];
    }

    private bool GetKeyUp(string key)
    {
        return !keyState[key] && lastKeyState[key];
    }

    /// <summary>
    /// 랜덤으로 키 하나를 지정받아 랜덤한 하나의 활동을 수행하도록 합니다.
    /// </summary>
    /// <param name="keySelection">선택된 키입니다.</param>
    /// <param name="keyAction">입력 받은 키가 수행할 활동입니다.</param>

    [PunRPC]
    public void SetKey(int id, bool v)
    {
        keyState[idToAction[id]] = v;
    }
    
    public void ShuffleIdToAction()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        idToAction.Clear();

        List<string> actionsAvailable = new List<string>();
        actionsAvailable.AddRange(actions);
        Dictionary<int, Player> players = PhotonNetwork.CurrentRoom.Players;
        List<int> idsAvailable = new List<int>();
        foreach (Player p in players.Values)
            idsAvailable.Add(p.ActorNumber);

        for (int i = 0; i < players.Count; i++)
        {
            int idSelected = idsAvailable[Random.Range(0, idsAvailable.Count)];
            string actionSelected = actionsAvailable[Random.Range(0, actionsAvailable.Count)];
            idsAvailable.Remove(idSelected);
            actionsAvailable.Remove(actionSelected);

            idToAction.Add(idSelected, actionSelected);
        }

        PhotonView.Get(this).RPC("SetIdToAction", RpcTarget.All, idToAction);
    }
    
    [PunRPC]
    public void SetIdToAction(Dictionary<int, string> dict)
    {
        idToAction = dict;
        PlayerController player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        player.SetAction(idToAction[PhotonNetwork.LocalPlayer.ActorNumber]);
    }

    public string GetAction(int id)
    {
        return idToAction[id];
    }

    public void ChangeRegenPoint(Vector3 p)
    {
        regenPoint = p;
    }

	/// <summary>
	/// 맵을 정상적으로 이동할 수 있는지 여부를 확인할 용도로 남겨 두었습니다.
	/// </summary>
	void Move()
	{
		// Left Move
		if (Input.GetKey(KeyCode.A))
		{
			charactorBody.AddForce(new Vector2(-moveSpeed, 0));
		}
		else if (Input.GetKeyUp(KeyCode.A))
		{
			charactorBody.velocity = Vector2.zero;
		}

		// Right Move
		if (Input.GetKey(KeyCode.D))
		{
			charactorBody.AddForce(new Vector2(moveSpeed, 0));
		}
		else if (Input.GetKeyUp(KeyCode.D))
		{
			charactorBody.velocity = Vector2.zero;
		}

		// JUMP
		if (Input.GetKeyDown(KeyCode.W))
		{
			charactorBody.AddForce(new Vector2(0, jumpForce));
		}
		else if (Input.GetKeyUp(KeyCode.W) && charactorBody.velocity.y > 0)
		{   // 점프 키에서 손을 때고, 현재 점프중이라면 점프 이동 속도 절반 감소
			charactorBody.velocity = charactorBody.velocity * 0.5f;
		}
	}

    public void Kill()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = regenPoint;
        //ShuffleIdToAction();
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag=="Platform")
        {
			isGround = true;
        }
    }
	private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag=="Platform")
        {
			isGround = false;
        }
    }*/

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
