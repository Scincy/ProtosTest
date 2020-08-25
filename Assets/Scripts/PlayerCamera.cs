using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
        {
            GameObject objectFound = GameObject.FindGameObjectWithTag("Player");
            if (objectFound != null)
                target = objectFound.transform;
        }
        else
            transform.position = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z - 10);// *Time.deltaTime;
    }
}
