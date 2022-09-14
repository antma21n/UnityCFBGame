using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClock : MonoBehaviour
{
    public float snaptimeRemaining = 40;
    public float quartertimeRemaining = 180;
    public bool snaptimerIsRunning = false;
    public bool quartertimerIsRunning = false;
    public Text gametimeText;
    public Text snaptimeText;
    public int Quarter;
    public bool halftime = false;
    public bool endOfGame = false;
    public Text timeoutText;
    public int timeoutsRemaining = 3;
    public GameObject EndOfQuarterCanvas;
    public GameObject HalfTimeCanvas;

    private void Start()
    {
        // Starts the timer automatically
        timeoutText.text = "Time Out: "+timeoutsRemaining+"/3";
        snaptimerIsRunning = true;
        quartertimerIsRunning = false;
        Quarter = 1;
    }

    void Update()
    {
        DisplayTime(snaptimeRemaining,true);
        DisplayTime(quartertimeRemaining,false);
        GameObject GM = GameObject.Find("GameManager");
        if (GM.GetComponent<gamestart>().start_game)
        {
            quartertimerIsRunning = true;
        } else 
        {
            if (quartertimeRemaining <= 0) {
                quartertimerIsRunning = false;
                quartertimeRemaining = 180; //temp
                Quarter = Quarter + 1;
                if (Quarter > 4)
                {
                    endOfGame = true;
                }
                if (Quarter == 3)
                {
                    //
                    GM.GetComponent<GameManager>().down = 1;
                    GM.GetComponent<GameManager>().yardsToGo = 10;
                    GM.GetComponent<GameManager>().yardLine = 25;
                    //GM.GetComponent<GameManager>().opponentYardLine = 75;
                    HalfTimeCanvas.SetActive(true);
                }
                else
                {
                    EndOfQuarterCanvas.SetActive(true);
                }
                snaptimerIsRunning = false;
                
                snaptimeRemaining = 40;
            }

        }

        if (snaptimerIsRunning)
        {
            if (snaptimeRemaining > 0) {
                snaptimeRemaining -= Time.deltaTime;
            } else {
                //Debug.Log("Time has run out!");
                snaptimeRemaining = 0;
            }
        }
        if (quartertimerIsRunning)
        {
            if (quartertimeRemaining > 0) {
                quartertimeRemaining -= Time.deltaTime;
            } else {
                //GM.GetComponent<GameManager>().nextplay();
                quartertimeRemaining = 0;
                quartertimerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay, bool T)
    {
        float Minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float Seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (T==true)
        {
            snaptimeText.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
        } else
        {
            gametimeText.text = "Q" + Quarter + " " + string.Format("{0:00}:{1:00}", Minutes, Seconds);
        }
    }

    public void TimeOutButton()
    {
        if (timeoutsRemaining > 0)
        {
            timeoutsRemaining = timeoutsRemaining - 1;
            //update text
            timeoutText.text = "Time Out: "+timeoutsRemaining+"/3";
            snaptimeRemaining = 150;
            quartertimerIsRunning = false;
        }
    }

    public void EndofQuarterButton()
    {
        snaptimerIsRunning = true;
    }

}