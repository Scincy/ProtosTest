using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    public bool needDestroy = false;
    public Transform regenPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject!=null)
        {
            Debug.Log(collision.gameObject);
        }
        
        if (collision.tag=="Player")
        {
            Debug.Log("DIE!!!!!");
            if (needDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.gameObject.transform.position = regenPoint.position;
            }
        }
    }
}
