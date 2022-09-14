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

    private void Start()
    {
        // Starts the timer automatically
        snaptimerIsRunning = true;
        quartertimerIsRunning = false;
        //Quarter = 1;
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
            //TEMP FOR NOW. IF QUARTER TIME = 0 GAME ENDS.
            if (quartertimeRemaining <= 0) {
                endOfGame = true;
                quartertimerIsRunning = false;
                //quartertimeRemaining = 180;
                //Quarter = Quarter + 1;
            }

        }
        // else
        // {
        //     if (quartertimeRemaining <= 0)
        //     {
        //         //POP UP quarter END. 
        //         Debug.Log("End of Quarter");
        //         quartertimeRemaining = 180;

        //         if (Quarter == 1)
        //         {
        //             //End of Half
        //             halftime = true;
        //             Debug.Log("Halftime");

        //         }
        //     }
        // }

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
                quartertimeRemaining = 0;
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

}