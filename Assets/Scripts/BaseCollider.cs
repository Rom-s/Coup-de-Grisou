using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollider : MonoBehaviour
{
    public TimerManager manager;

    public GameObject player;

    public PlayerController controller;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            manager.CheckWinOrLoose();
            controller.oxygenChange = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("bla");
        if (collision.gameObject == player)
        {
            controller.oxygenChange = true;
        }
    }
}
