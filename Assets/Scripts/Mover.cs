using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State { Idle, MovingA_B, MovingB_A, ArrivedA, ArrivedB, Stopped }

public class Mover : MonoBehaviour
{
	public Transform departure;
	public Transform destination;
	public float speed = 10;
	public bool isStopMoving = false;
	public State currentState = State.Idle;
	
	private Vector2 moveVector;
	private State tempState=State.Idle;
	void Start()
	{
		moveVector = Vector2.zero;
		transform.position = departure.position;
	}

	// Update is called once per frame
	void Update()
	{
        if (currentState == State.Idle && !isStopMoving)
        {
			currentState = State.MovingA_B;
        }
        switch (getMovingState())
        {
			case State.Stopped:
                if (!isStopMoving) //그만 움직이도록 설정되면...
                {
					currentState = tempState;
				}else
				{ moveVector = transform.position; }
				
				break;
			case State.MovingA_B:
				moveVector = destination.position;
                if (transform.position==destination.position)
                {
					currentState = State.ArrivedB;
                }
				break;
			case State.MovingB_A:
				moveVector = departure.position;
                if (transform.position==departure.position)
                {
					currentState = State.ArrivedA;
                }
				break;
			case State.ArrivedB:
                if (isStopMoving)
                {
					currentState = State.Stopped;
                }else currentState = State.MovingB_A;
				break;
			case State.ArrivedA:
				if (isStopMoving)
				{
					tempState = currentState;
					currentState = State.Stopped;

				}
				else currentState = State.MovingA_B;
				break;
            default:
				Debug.LogError("이동에 무언가 잘못됬는데 그게 뭔지 모르겠네... 코드 다시 써봐");
                break;
        }
		transform.position = Vector2.MoveTowards(transform.position, moveVector, speed * Time.deltaTime);

	}
	public void StopMove()
	{
		isStopMoving = true;
	}
	public State getMovingState()
    {
		return currentState;
    }
	public void setState(State state)
    {
		currentState = state;
    }
}
