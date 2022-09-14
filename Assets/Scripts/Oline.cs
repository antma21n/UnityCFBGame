using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oline : MonoBehaviour
{
    public float speed;
    public bool winner;

    // Start is called before the first frame update
    void Start()
    {
        //need a pause to make first block or slow speed to make first block
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<gamestart>().start_game == true)
        {
            if (GameObject.FindObjectOfType<receiverwithball>())
            {
                receiverwithball closestEnemy = GameObject.FindObjectOfType<receiverwithball>();
                transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, Time.deltaTime * speed);
                Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
            }
            else
            {
                if (GameObject.Find("QB"))
                {
                    GameObject qb = GameObject.Find("QB");
                    //dont move if within 0.5 units
                    if (Math.Abs(qb.transform.position[1] - this.transform.position[1]) > 0.5)
                    {
                        if (Math.Abs(qb.transform.position[0] - this.transform.position[0]) > 0.5)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, qb.transform.position, Time.deltaTime * speed);
                        }
                    }
                }
            }
        }
    }
}
