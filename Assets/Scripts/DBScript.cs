using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DBScript : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 enemyPos;
	private bool start_run;
    public receivers closestEnemy;
    public GameObject DBwithBall;
    public float delay; 
	public float speed;
	public float space;
    public int catchChance;
    public bool allowCatch = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        start_run = false;
        //FindClosestEnemy();
        StartCoroutine(Example());
    }
    IEnumerator Example()
    {
        //print(Time.time);
        yield return new WaitForSeconds(delay);
        //print(Time.time);
        start_run = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (allowCatch == true)
        {
            if (UnityEngine.Random.Range(1, 101) < catchChance)
            {
                GameObject qb = GameObject.Find("QB");
                //BallControl2 bC = qb.GetComponent<BallControl2>();
                Dude bC = qb.GetComponent<Dude>();
                if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 1)
                {
                    if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 1)
                    {
                        if (other.gameObject.CompareTag("ball"))
                        {
                            //caught ball
                            Destroy(gameObject);
                            Destroy(other.gameObject);
                            Destroy(GameObject.FindWithTag("ballObject"));
                            GameObject e = Instantiate(DBwithBall) as GameObject;
                            e.transform.position = transform.position;
                            e.name = "DB with Ball";
                        }
                    }
                }
            }
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // Update is called once per frame
    void LateUpdate()
    {
        FindClosestEnemy();
        if (GameObject.Find("GameManager").GetComponent<gamestart>().start_game == true)
        {
            if (GameObject.FindWithTag("ball"))
            {
                GameObject qb = GameObject.Find("QB");
                //BallControl2 bC = qb.GetComponent<BallControl2>();
                Dude bC = qb.GetComponent<Dude>();
                if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 0.5)
                {
                    if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 0.5)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, bC.EndTrajectory, Time.deltaTime * speed);
                    }
                    else
                    {
                        TrackClosestEnemy(closestEnemy);
                    }
                }
                else
                {
                    TrackClosestEnemy(closestEnemy);
                }
            }
            else
            {
                TrackClosestEnemy(closestEnemy);
            }
        }
    }

    void FindClosestEnemy()
	{
		if (start_run == true)
		{
            // below should run once 
            float distanceToClosestEnemy = Mathf.Infinity;
            //receivers closestEnemy = null;
            receivers[] allEnemies = GameObject.FindObjectsOfType<receivers>();

            foreach (receivers currentEnemy in allEnemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }
    }

    void TrackClosestEnemy(receivers closestEnemy)
    {
        if (start_run == true)
        {
            //change to with ball tag
            if (GameObject.FindObjectOfType<receiverwithball>())
            {
                receiverwithball newclosestEnemy = GameObject.FindObjectOfType<receiverwithball>();
                transform.position = Vector3.MoveTowards(transform.position, newclosestEnemy.transform.position, Time.deltaTime * speed);
                Debug.DrawLine(this.transform.position, newclosestEnemy.transform.position);
            }
            else
            {
                enemyPos = new Vector3(closestEnemy.transform.position.x + space, closestEnemy.transform.position.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, enemyPos, Time.deltaTime * speed);
                Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
            }
        }
    }
}

//