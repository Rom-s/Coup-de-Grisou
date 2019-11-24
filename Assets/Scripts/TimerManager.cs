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
    
    public float _timeLeft;



    private float _minutes;
    private float _seconds;

    Text text;
  
    void Awake()
    {
        text = GetComponent<Text>();

    }


    void Update()
    {
        _timeLeft -= Time.deltaTime;
        _minutes = Mathf.Floor(_timeLeft / 60);   
        _seconds = _timeLeft % 60;
        if (_seconds > 59) _seconds = 59;


        if (_timeLeft > 30)
        {
            text.text = "Time left : " + _minutes + '"' + Mathf.Round(_seconds);
        }
        

        if (_timeLeft < 60)
        {
            text.color = Color.red;
        }

        if (_timeLeft < 30)
        {
            StartCoroutine(BlinkText());
        }

        if (_timeLeft < 0)
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
            text.text = "Time left : " + _minutes + '"' + Mathf.Round(_seconds);
            yield return new WaitForSeconds(.5f);
        }
    }

    public void CheckWinOrLoose()
    {
        /////////////  TODO il faudra vérifier la zone dans lequel est le joueur, il doit être en haut
        if (ScoreManager.Instance.score > seuilScore)
        {
            SceneManager.LoadScene("WinScene");
        }
        else if(_timeLeft < 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }



}
