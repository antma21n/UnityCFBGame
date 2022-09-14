using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runningback : MonoBehaviour
{

    public bool isClicked = false;
    public GameObject RB_withball;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<gamestart>().start_game == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().runplay == true)
            {
                
                GameObject rbrun = Instantiate(RB_withball) as GameObject;
                rbrun.name = "WR with Ball";
                rbrun.transform.position = transform.position;
                rbrun.GetComponent<WRwithBallScript>().routeType = this.GetComponent<WRScript>().routeType;
                rbrun.GetComponent<WRwithBallScript>().speed = this.GetComponent<WRScript>().speed;
                rbrun.GetComponent<WRwithBallScript>().runner = true;
                Destroy(GameObject.Find("RB"));
            }
        }

        }
}
