using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollider : MonoBehaviour
{
    public TimerManager manager;

    public GameObject player;

    public PlayerController controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            manager.CheckWinOrLoose();
            controller.oxygenChange = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("bla");
        if (other.gameObject == player)
        {
            controller.oxygenChange = true;
        }
    }
}
