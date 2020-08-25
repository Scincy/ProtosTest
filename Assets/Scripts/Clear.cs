using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public Transform newRegenPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            Debug.Log(collision.gameObject);
        }

        if (collision.tag == "Player")
        {
            enabled = false;
            collision.GetComponent<CharactorContorller>().ChangeRegenPoint(newRegenPoint.position);
            GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>().ShowRegenDialog("스테이지 클리어!");
        }
    }
}
