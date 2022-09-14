using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WRScript : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 route_p1;
    private Vector3 route_p2;
    private Vector3 route_p3;
    private Vector3 startPos;
    private bool transition_point;
    private Vector3 enemyPos;
    private bool begin;

    private int routeLines;
    public GameObject WRwithBall;
    public string routeType;
    public string routeDirection;
    public float speed;

    public bool covered = false;
    public bool inSSzone = false;
    public bool inCBzone = false;
    public bool inLBzone = false;
    public bool allowCatch = false;

    private bool transition_point_0 = true;
    private bool transition_point_1;
    private bool transition_point_2;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;

        //DIFFERENT TYPES OF ROUTES Vectors
        if (routeType == "corner")
        {
            routeLines = 2;
            route_p1 = new Vector3(transform.position[0] + 3, transform.position[1], 0);
            if (routeDirection == "+")
            {
                route_p2 = new Vector3(transform.position[0] + 5, transform.position[1] + 3, 0);
            } else
            {
                route_p2 = new Vector3(transform.position[0] + 5, transform.position[1] - 3, 0);
            }
        }

        if (routeType == "deep corner")
        {
            routeLines = 2;
            
            if (routeDirection == "+")
            {
                route_p1 = new Vector3(transform.position[0] + 4, transform.position[1] + 1, 0);
                route_p2 = new Vector3(transform.position[0] + 9, transform.position[1] - 2, 0);
            }
            else
            {
                route_p1 = new Vector3(transform.position[0] + 4, transform.position[1] - 1, 0);
                route_p2 = new Vector3(transform.position[0] + 9, transform.position[1] + 2, 0);
            }
        }


        if (routeType == "curl")
        {
            routeLines = 2;
            route_p1 = new Vector3(transform.position[0] + 2, transform.position[1], 0);
            if (routeDirection == "+")
            {
                route_p2 = new Vector3(transform.position[0] + 1.5f, transform.position[1] + 1, 0);
            }
            else
            {
                route_p2 = new Vector3(transform.position[0] + 1.5f, transform.position[1] - 1, 0);
            }
        }

        if (routeType == "dig")
        {
            routeLines = 2;
            route_p1 = new Vector3(transform.position[0] + 2, transform.position[1], 0);
            if (routeDirection == "+")
            {
                route_p2 = new Vector3(transform.position[0] + 2, transform.position[1] + 3, 0);
            }
            else
            {
                route_p2 = new Vector3(transform.position[0] + 2, transform.position[1] - 3, 0);
            }
        }

        if (routeType == "flat")
        {
            routeLines = 2;
            route_p1 = new Vector3(transform.position[0] + 1, transform.position[1], 0);
            if (routeDirection == "+")
            {
                route_p2 = new Vector3(transform.position[0] + 1, transform.position[1] + 8, 0);
            }
            else
            {
                route_p2 = new Vector3(transform.position[0] + 1, transform.position[1] - 8, 0);
            }
        }

        if (routeType == "go")
        {
            routeLines = 1;
            route_p1 = new Vector3(transform.position[0] + 12, transform.position[1], 0);
        }

        if (routeType == "slant")
        {
            routeLines = 2;
            route_p1 = new Vector3(transform.position[0] + 1, transform.position[1], 0);
            if (routeDirection == "+")
            {
                route_p2 = new Vector3(transform.position[0] + 3, transform.position[1] + 6, 0);
            } else
            {
                route_p2 = new Vector3(transform.position[0] + 3, transform.position[1] - 6, 0);
            }
        }

        if (routeType == "angle")
        {
            routeLines = 2;
            route_p1 = new Vector3(transform.position[0] + 2, transform.position[1] -2, 0);
            if (routeDirection == "+")
            {
                route_p2 = new Vector3(transform.position[0] + 4, transform.position[1] + 4, 0);
            }
            else
            {
                route_p2 = new Vector3(transform.position[0] + 4, transform.position[1] + 4, 0);
            }
        }

        if (routeType == "out")
        {
            routeLines = 1;
            if (routeDirection == "+")
            {
                route_p1 = new Vector3(transform.position[0], transform.position[1] + 2, 0);
            }
            else
            {
                route_p1 = new Vector3(transform.position[0], transform.position[1] - 2, 0);
            }
        }

        if (routeType == "wheel")
        {
            routeLines = 2;
            if (routeDirection == "+")
            {
                route_p1 = new Vector3(transform.position[0] + 2, transform.position[1] + 2, 0);
                route_p2 = new Vector3(transform.position[0] + 12, transform.position[1] + 3, 0);
            }
            else
            {
                route_p1 = new Vector3(transform.position[0] + 2, transform.position[1] - 2, 0);
                route_p2 = new Vector3(transform.position[0] + 12, transform.position[1] - 3, 0);
            }
        }

        if (routeType == "post stop")
        {
            routeLines = 2;
            if (routeDirection == "+")
            {
                route_p1 = new Vector3(transform.position[0] + 2, transform.position[1] + 2.5f, 0);
                route_p2 = new Vector3(transform.position[0] + 1.75f, transform.position[1] + 2.5f, 0);
            }
            else
            {
                route_p1 = new Vector3(transform.position[0] + 2, transform.position[1] - 2.5f, 0);
                route_p2 = new Vector3(transform.position[0] + 1.75f, transform.position[1] - 2.5f, 0);
            }
        }

        if (routeType == "PCP")
        {
            routeLines = 3;
            if (routeDirection == "+")
            {
                route_p1 = new Vector3(transform.position[0] + 2, transform.position[1] + 2, 0);
                route_p2 = new Vector3(transform.position[0] + 3, transform.position[1] + 2, 0);
                route_p3 = new Vector3(transform.position[0] + 5, transform.position[1] + 6, 0);
            }
            else
            {
                route_p1 = new Vector3(transform.position[0] + 2, transform.position[1] - 2, 0);
                route_p2 = new Vector3(transform.position[0] + 3, transform.position[1] - 2, 0);
                route_p3 = new Vector3(transform.position[0] + 5, transform.position[1] - 6, 0);
            }
        }
    }

    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<gamestart>().start_game == true)
        {
            if (GameObject.Find("DB with Ball"))
            {
                GameObject db = GameObject.Find("DB with Ball");
                transform.position = Vector3.MoveTowards(transform.position, db.transform.position, Time.deltaTime * speed);
                Debug.DrawLine(this.transform.position, db.transform.position);
            }
            else
            {
                if (GameObject.FindWithTag("ball"))
                {
                    GameObject qb = GameObject.Find("QB");
                    //BallControl2 bC = qb.GetComponent<BallControl2>();
                    Dude bC = qb.GetComponent<Dude>();
                    if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 1.5)
                    {
                        if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 1.5)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, bC.EndTrajectory, Time.deltaTime * speed);
                        }
                        else
                        {
                            RouteSelection();
                        }
                    }
                    else
                    {
                        RouteSelection();
                    }
                }
                else
                {
                    RouteSelection();
                }
                //RouteSelection();
            }
        }
    }
    //catch ball
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (allowCatch == true)
        {
            GameObject qb = GameObject.Find("QB");
            //BallControl2 bC = qb.GetComponent<BallControl2>();
            Dude bC = qb.GetComponent<Dude>();
            if (Math.Abs(rb.position[0] - bC.EndTrajectory[0]) < 0.5)
            {
                if (Math.Abs(rb.position[1] - bC.EndTrajectory[1]) < 0.5)
                {
                    if (other.gameObject.CompareTag("ball"))
                    {
                        //caught ball
                        Destroy(gameObject);
                        Destroy(other.gameObject);
                        Destroy(GameObject.FindWithTag("ballObject"));
                        GameObject e = Instantiate(WRwithBall) as GameObject;
                        e.transform.position = transform.position;
                        e.name = "WR with Ball";
                        //e.GetComponent<WRwithBallScript>().routeType = routeType;
                        //speed should be determined and delay 
                        e.GetComponent<WRwithBallScript>().runner = false; 
                    }
                }
            }
        }
        //stops tracking once they touch. not once WR leaves. 
        //way we dont need to use tags?
        if (other.gameObject.CompareTag("FS Zone"))
        {
            covered = true;
        }
        if (other.gameObject.CompareTag("SS Zone"))
        {
            covered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        covered = false;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //DIFFERENT TYPES OF ROUTES
    void RouteSelection()
    {
        if (routeLines == 1)
        {
            //run Bench Route
            if (transition_point == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p1, Time.deltaTime * speed); //forward part of bench route
            }
            //run towards endzone
        }
        if (routeLines == 2)
        {
            //run Slant Route
            if (transition_point == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p1, Time.deltaTime * speed); //forward part of bench route
            }
            if (Math.Abs(route_p1[0] - rb.position[0]) < 0.01) 
            {
                transition_point = true;
            }
            if (transition_point == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p2, Time.deltaTime * speed); //slant part of bench route
            }
            //run towards endzone
        }
        if (routeLines == 3)
        {
            //run Slant Route
            if (transition_point_0 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p1, Time.deltaTime * speed); //forward part of bench route
            }
            if (Math.Abs(route_p1[0] - rb.position[0]) < 0.01)
            {
                transition_point_0 = false;
                transition_point_1 = true;
                transition_point_2 = false;
            }
            if (Math.Abs(route_p2[0] - rb.position[0]) < 0.01)
            {
                transition_point_0 = false;
                transition_point_1 = false;
                transition_point_2 = true;
            }
            if (transition_point_1 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p2, Time.deltaTime * speed); //slant part of bench route
            }
            if (transition_point_2 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_p3, Time.deltaTime * speed); //slant part of bench route
            }
            //run towards endzone
        }
    }





}
