using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Singleton
    private static TimerManager instance;
    public static TimerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TimerManager>();
            }

            return instance;
        }
    }

    #endregion
    public float timeLeft;

    private float minutes;
    private float seconds;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        timeLeft = 360;
    }


    void Update()
    {
        timeLeft -= Time.deltaTime;
        minutes = Mathf.Floor(timeLeft / 60);   
        seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;

        text.text = "Time left : " + minutes + '"' + Mathf.Round(seconds);

        if (timeLeft < 0)
        {
            Debug.Log("Fin de la partie");
        }
    }

     

}
