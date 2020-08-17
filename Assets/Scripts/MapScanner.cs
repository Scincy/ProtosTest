using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MapScanner : MonoBehaviour
{
    private BoxCollider2D scanArea;
    // Start is called before the first frame update
    void Start()
    {
        scanArea = GetComponent<BoxCollider2D>();
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 오브젝트의 태그가 문이면...
        if (collision.gameObject.tag == "door")
        {
            // 문 입장 여부 투표 가능하도록
        }

        if (collision.gameObject.tag == "fire")
        {
            // 
        }
    }

}
