﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public Transform newRegenPoint;
    public bool isFinalStage = false;

    private DialogManager dialogManager;
    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
    }

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
            dialogManager.ShowRegenDialog("스테이지 클리어!");
        }
        if (isFinalStage)
        {
            dialogManager.ShowRegenDialog("마지막스테이지 클리어하셨습니다! 게임을 종료해주세요!");
        }
    }
}
