using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject ball;
    private Vector3 T;
    //public GameObject Square;
    //public GameObject NegSquare;
    //public GameObject ballColide;

    // Start is called before the first frame update
    void Start()
    {

        //rb = GetComponent<Rigidbody2D>();
        GameObject qb = GameObject.Find("QB");
        Dude bC = qb.GetComponent<Dude>();
        Debug.Log(bC.EndTrajectory);
        FindClosestReceiver();
        FindClosestDefender();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject qb = GameObject.Find("QB");
        Dude bC = qb.GetComponent<Dude>();

        //if (Math.Abs(transform.position[0] - bC.EndTrajectory[0]) < 0.2)
        //{
        //    if (Math.Abs(transform.position[1] - bC.EndTrajectory[1]) < 0.2)
        //    {
        //        Destroy(gameObject);
        //        GameObject b = Instantiate(ball) as GameObject;
        //        b.transform.position = transform.position;
        //        b.name = "landed ball";
        //        Destroy(GameObject.Find("LandedFootball(Clone)"));
        //    }
        //}
    }

    void FindClosestReceiver()
    {
        GameObject qb = GameObject.Find("QB");
        Dude bC = qb.GetComponent<Dude>();
        T = new Vector3(bC.EndTrajectory[0], bC.EndTrajectory[1], 0);
        float distanceToClosestEnemy = Mathf.Infinity;
        receivers closestEnemy = null;
        receivers[] allEnemies = GameObject.FindObjectsOfType<receivers>();

        foreach (receivers currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - T).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        closestEnemy.GetComponent<WRScript>().allowCatch = true;
        Debug.Log(closestEnemy);
    }

    void FindClosestDefender()
    {
        GameObject qb = GameObject.Find("QB");
        Dude bC = qb.GetComponent<Dude>();
        T = new Vector3(bC.EndTrajectory[0], bC.EndTrajectory[1], 0);
        float distanceToClosestEnemy = Mathf.Infinity;
        defender closestEnemy = null;
        defender[] allEnemies = GameObject.FindObjectsOfType<defender>();

        foreach (defender currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - T).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        if (closestEnemy.GetComponent<DBScript>() == null)
        {
            closestEnemy.GetComponent<DBZoneScript>().allowCatch = true;
        }
        else
        {
            closestEnemy.GetComponent<DBScript>().allowCatch = true;
        }
        Debug.Log(closestEnemy);
    }
}