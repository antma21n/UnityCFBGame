using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRSlant : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 route_p1;
    private Vector3 route_p2;
    private Vector3 startPos;
    private bool transition_point;
    public GameObject NegSquare;
    //private Vector3 route;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        Debug.Log(startPos);
        route_p1 = new Vector3(transform.position[0] + 1, transform.position[1], 0);
        route_p2 = new Vector3(transform.position[0] + 3, transform.position[1] + 6, 0);
        Debug.Log(route_p1);
        Debug.Log(route_p2);
    }

    void Update()
    {
        if (GameObject.FindWithTag("ball"))
        {
            GameObject qb = GameObject.Find("QB");
            BallControl2 bC = qb.GetComponent<BallControl2>();
            if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 1)
            {
                if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, bC.EndTrajectory, Time.deltaTime * 2);
                }
            }
        }
        //else
        //{
            //run Bench Route: move right 3 units and at 45 degree out feild for 2 units.
            if (transition_point == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p1, Time.deltaTime * 2); //forward part of bench route
            }
            //Debug.Log(rb.position[0]);
            //Debug.Log(rb.position[1]);
            if (Math.Abs(route_p1[0] - rb.position[0]) < 0.01) //once player leaves position these arent equal and he doesnt move. 
            {
                transition_point = true;
            }
            if (transition_point == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p2, Time.deltaTime * 1); //slant part of bench route
            }


        //}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject qb = GameObject.Find("QB");
        BallControl2 bC = qb.GetComponent<BallControl2>();
        if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 1)
        {
            if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 1)
            {
                if (other.gameObject.CompareTag("ball"))
                {
                    Destroy(gameObject);
                    //GetComponent<Collider2D>().enabled = false;
                    Destroy(other.gameObject);
                    GameObject e = Instantiate(NegSquare) as GameObject;
                    e.transform.position = transform.position;

                }
            }
        }
    }
}