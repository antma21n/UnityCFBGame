using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldWRScript : MonoBehaviour
{
    public GameObject Square;
    Rigidbody2D rb;
    public GameObject NegSquare;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //public GameObject WR;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //GameObject qb = GameObject.Find("QB");
    //BallControl2 bC = qb.GetComponent<BallControl2>();
    //if at EndTrajectory zone
    //Debug.Log(rb.position[0]);
    //Debug.Log(bC.EndTrajectory[0]);
    //if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 0.3)
    //{
    //    if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 0.3)
    //    {
    //Debug.Log("Catch Made");
    //if (other.gameObject.CompareTag("ball"))
    //{
    //    GameObject e = Instantiate(NegSquare) as GameObject;
    //    e.transform.position = transform.position;
    //    Destroy(other.gameObject);
    //    
    //}
    //    }
    //}
    //}
    void Update()
    {
        // we need input.getmousebuttonup to update the end trajectory but keep constant movement after button is released.

        //if (Input.GetMouseButtonUp(0))
        //{

        //if ball exists
        //Debug.Log(GameObject.FindGameObjectsWithTag("ball"));
        //Debug.Log(gameObject.CompareTag("QB"));
        if (GameObject.FindWithTag("ball"))
        {

            //}
            //    if (GameObject.FindGameObjectsWithTag("ball") != null) //it exists
            //{
            Debug.Log("found ball");
            GameObject qb = GameObject.Find("QB");
            BallControl2 bC = qb.GetComponent<BallControl2>();
            if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 2)
            {
                if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 2)
                {
                    //Debug.Log("WalkTowards");
                    transform.position = Vector3.MoveTowards(transform.position, bC.EndTrajectory, Time.deltaTime * 1);
                }
            }
        }


        //}

        //else (ball doenst exist)
        //keep going on route.
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
