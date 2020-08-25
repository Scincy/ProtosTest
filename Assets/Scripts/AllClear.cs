using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllClear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            Debug.Log(collision.gameObject);
        }

        if (collision.tag == "Player")
        {
            enabled = false;
            GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>().ShowLobbyDialog("게임 클리어!");
        }
    }
}
