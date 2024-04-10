using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //ºô½ÐÊÂ¼þ
    public VoidEventSO gameOverEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverEvent.RaiseEvent();
        }
    }
}
