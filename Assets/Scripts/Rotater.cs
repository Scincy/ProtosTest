using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RotState { Idle, Stopped, RightRotation, LeftRotation }
public class Rotater : MonoBehaviour
{

    public float rotationSpeed;
    public RotState currentRotStste = RotState.Idle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentRotStste)
        {
            case RotState.Idle:
                break;
            case RotState.Stopped:
                break;
            case RotState.RightRotation:
                transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
                break;
            case RotState.LeftRotation:
                transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
                break;
            default:
                break;
        }
        
    }
}
