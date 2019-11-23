using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{

    private int seuilScore = 2;

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
        timeLeft = 90;
    }


    void Update()
    {
        timeLeft -= Time.deltaTime;
        minutes = Mathf.Floor(timeLeft / 60);   
        seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;


        if (timeLeft > 30)
        {
            text.text = "Time left : " + minutes + '"' + Mathf.Round(seconds);
        }
        

        if (timeLeft < 60)
        {
            text.color = Color.red;
        }

        if (timeLeft < 30)
        {
            StartCoroutine(BlinkText());
        }

        if (timeLeft < 0)
        {
            CheckWinOrLoose();
        }
    }

    //function to blink the text
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            text.text = "";
            yield return new WaitForSeconds(.5f);
            text.text = "Time left : " + minutes + '"' + Mathf.Round(seconds);
            yield return new WaitForSeconds(.5f);
        }
    }

    void CheckWinOrLoose()
    {
        /////////////  TODO il faudra vérifier la zone dans lequel est le joueur, il doit être en haut
        if (ScoreManager.Instance.score > seuilScore)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    void clignotementText()
    {
        //iTween.ColorTo(text.gameObject, iTween.Hash("a", 0, "time", 1.5f, "looptype", iTween.LoopType.pingPong));

    }


}
