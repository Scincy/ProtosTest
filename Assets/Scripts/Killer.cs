using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
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
            collision.GetComponent<CharactorContorller>().Kill();
        }
    }
}
