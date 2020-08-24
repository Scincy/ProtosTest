using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorContorller : MonoBehaviour
{
	public float jumpForce=300f;
	public float moveSpeed=5f;

	// UI바꿈 처리를 위해 필요한 부분들
	public Image selectedKeyUI;
	public Image selectedActionUI;
	public Sprite[] keyUI;
	public Sprite[] actionUI;

	public bool isMapDebugMode = false;

	//private int randomKey;
	//private int randomKeyAction;
	//private const int MAX_KEY_ACTION_COUNT= 3;
	private bool isGround = true;

	private Rigidbody2D charactorBody;

	//protected private string[] keyboard = { "q", "w", "e", "r", "a", "s", "d", "f" };
	//protected private string[] actions  = { "right", "left", "jump", "down" };


	void Start()
	{
		charactorBody = GetComponent<Rigidbody2D>();

		// Init - Random direction and key number
		//SetRandomKeySelection();
		//SetRandomAction();

		//Debug.Log(actions[randomKeyAction] + " is On " + keyboard[randomKey]);
	}

	// Update is called once per frame
	void Update()
	{
		// 이 부분의 조건문을 죽었을 때로 바꾸면 새롭게 키를 배정할 수 있습니다.
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
			SetRandomKeySelection();
			SetRandomAction();
			//Debug.Log(actions[randomKeyAction] + " is On " + keyboard[randomKey]);
		}*/
		if (isMapDebugMode)
		{
			Move();
		}
	}

	/// <summary>
	/// 랜덤으로 키 하나를 지정받아 랜덤한 하나의 활동을 수행하도록 합니다.
	/// </summary>
	/// <param name="keySelection">선택된 키입니다.</param>
	/// <param name="keyAction">입력 받은 키가 수행할 활동입니다.</param>
	public void Move(bool key, bool keyUp, bool keyDown, string keyAction)
	{
        switch (keyAction)
        {
			case "right":
				// Right Move
				if (key)
				{
					charactorBody.AddForce(new Vector2(moveSpeed, 0));
				}
				else if (keyUp)
				{
					charactorBody.velocity = Vector2.zero;
				}
				break;
			case "left":
				// Left Move
				if (key)
				{
					charactorBody.AddForce(new Vector2(-moveSpeed, 0));
				}
				else if (keyUp)
				{
					charactorBody.velocity = Vector2.zero;
				}
				break;
			case "jump":
				// JUMP
				if (keyDown)
				{
					charactorBody.AddForce(new Vector2(0, jumpForce));
				}
				else if (keyUp && charactorBody.velocity.y > 0)
				{   // 점프 키에서 손을 때고, 현재 점프중이라면 점프 이동 속도 절반 감소
					charactorBody.velocity = charactorBody.velocity * 0.5f;
				}
				break;
            default:
				break;
        }
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
    
	/*void SetRandomKeySelection()
    {
		randomKey = Random.Range(0, keyboard.Length);
		selectedKeyUI.sprite = keyUI[randomKey];
	}*/
    
	/*void SetRandomAction()
    {
		randomKeyAction = Random.Range(0, MAX_KEY_ACTION_COUNT);
		selectedActionUI.sprite = actionUI[randomKeyAction];

		//TODO photon으로 중복된 Action이 없는지 확인이 필요
	}*/

    private void OnCollisionStay2D(Collision2D collision)
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
    }
}
