using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script keeps track of play calling, when players start running, 

public class gamestart : MonoBehaviour
{

    public bool start_game = false;
    public bool runplay = false;
    //private bool timeBool;
    public GameObject RB_withball;
    public bool changingPlay;
    public int startMask = 375;

    //public float time = 0;
    //private float timeDelay = 0.0f;

    // Update is called once per frame
    //void Update()
    //{

    //    if (GameObject.Find("GameManager").GetComponent<GameManager>().fourthdownBool == false)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            timeBool = true;
    //        }
    //        if (timeBool == true)
    //        {
    //            time = time + 1f * Time.deltaTime;
    //        }

    //        if (time >= timeDelay)
    //        {
    //            if (Input.GetMouseButton(0))
    //            {
    //                start_game = true;
    //            }
    //            else
    //            {
    //                time = 0f;
    //                timeBool = false;
    //            }
    //        }
    //    }
    //}

    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().fourthdownBool == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Debug.Log(Input.mousePosition.y);
                if (screenPosition[1] < startMask) //&& !changingPlay)
                {
                    start_game = true;
                    //hide buttons
                }
            }

        }
    }

    public void isChangingPlay()
    {
        startMask = 0;
    }
}
