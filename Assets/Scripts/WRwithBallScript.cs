using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class WRwithBallScript : MonoBehaviour
{
    public bool WRtackled = false;
    public GameObject TackleMade;
    public float speed; 
    public int tackleDelay;
    public string routeType;
    public bool runner;

    private bool routecompleted = false;
    private bool tackleme = false;
    private bool collisionWithPlayer = false;
    private Vector3 endZonePos;
    private Vector3 endRoutePos;
    private bool startRoute1 = true;
    private bool startRoute2;

    public GameObject JukedPlayer;

    Vector2 DragStartPos;
    Vector2 DragEndPos; 
    public Vector3 jukePos;
    bool startJuke;


    // Start is called before the first frame update
    async void Start()
    {
        //routecompleted = false;

        if (runner == true)
        {
            tackleDelay = UnityEngine.Random.Range(1000, 2500);
        }
        else
        {
            tackleDelay = UnityEngine.Random.Range(0, 500);
        }
        await Task.Delay(tackleDelay);
        tackleme = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameObject.Find("GameManager").GetComponent<gamestart>().start_game == true)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    }
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        startJuke = true;
        //        if (DragStartPos.y < DragEndPos.y)
        //        {
        //             jukePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        //        }
        //        else
        //        {
        //             jukePos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        //        }
        //    }
        //    if (startJuke == true)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, jukePos, Time.deltaTime * speed * 2f);
        //        if (Mathf.Abs(jukePos.y - transform.position.y) < 0.15)
        //        { 
        //            startJuke = false;
        //        }
        //    }
        //}
        RunToEndZone();
        if (tackleme == true)
        {
            if (collisionWithPlayer == true)
            {
                WRtackled = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DB"))
        {
            if (tackleme == false)
            {
                Debug.Log("JUKE");
                GameObject juke = Instantiate(JukedPlayer) as GameObject;
                juke.transform.position = other.transform.position;
                juke.name = other.name;
                Destroy(other.gameObject);
            }
            else
            {
                collisionWithPlayer = true;
            }
        }
        if (other.gameObject.CompareTag("DL"))
        {
            if (tackleme == false)
            {
                Debug.Log("JUKE");
                //GameObject juke = Instantiate(JukedPlayer) as GameObject;
                //juke.transform.position = other.transform.position;
                //juke.name = other.name;
                //Destroy(other.gameObject);
            }
            else
            {
                collisionWithPlayer = true;
            }
        }
        //pop up stiff arm, trucked, juke, missed
    }
    void RunToEndZone()
    {
        if (routecompleted == false)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().runplay == true)
            {
                if (startRoute1 == true)
                {
                    Vector3 qbPos = new Vector3(-9, 0, 0); //position of QB
                    transform.position = Vector3.MoveTowards(transform.position, qbPos, Time.deltaTime * speed * 1.25f); //run to QB for ball
                    if (transform.position.x == qbPos.x)
                    {
                        if (transform.position.y == qbPos.y)
                        {
                            startRoute2 = true;
                            startRoute1 = false;
                        }
                    }
                }

                if (routeType == "stretch")
                {
                    if (startRoute2 == true)
                    {
                        endRoutePos = new Vector3(-8, 3, 0); //position of QB
                        transform.position = Vector3.MoveTowards(transform.position, endRoutePos, Time.deltaTime * speed * 1.25f); //run to QB for ball
                        if (transform.position.x == endRoutePos.x)
                        {
                            if (transform.position.y == endRoutePos.y)
                            {
                                Debug.Log("route completed");
                                routecompleted = true;
                                startRoute2 = false;
                            }
                        }
                    }
                }
                if (routeType == "dive")
                {
                    if (startRoute2 == true)
                    {
                        endRoutePos = new Vector3(-8.25f, 0, 0); //position of QB
                        transform.position = Vector3.MoveTowards(transform.position, endRoutePos, Time.deltaTime * speed * 1.25f); //run to QB for ball
                        if (transform.position.x == endRoutePos.x)
                        {
                            if (transform.position.y == endRoutePos.y)
                            {
                                Debug.Log("route completed");
                                routecompleted = true;
                                startRoute2 = false;
                            }
                        }
                    }
                }
                if (routeType == "counter")
                {
                    if (startRoute2 == true)
                    {
                        endRoutePos = new Vector3(-8.75f, -0.5f, 0); //position of QB
                        transform.position = Vector3.MoveTowards(transform.position, endRoutePos, Time.deltaTime * speed * 1.25f); //run to QB for ball
                        if (transform.position.x == endRoutePos.x)
                        {
                            if (transform.position.y == endRoutePos.y)
                            {
                                Debug.Log("route completed");
                                routecompleted = true;
                                startRoute2 = false;
                            }
                        }
                    }
                }

                if (routeType == "offTackle")
                {
                    if (startRoute2 == true)
                    {
                        endRoutePos = new Vector3(-8.5f, 1f, 0); //position of QB
                        transform.position = Vector3.MoveTowards(transform.position, endRoutePos, Time.deltaTime * speed * 1.25f); //run to QB for ball
                        if (transform.position.x == endRoutePos.x)
                        {
                            if (transform.position.y == endRoutePos.y)
                            {
                                Debug.Log("route completed");
                                routecompleted = true;
                                startRoute2 = false;
                            }
                        }
                    }
                }
                if (routeType == "insideTackle")
                {
                    if (startRoute2 == true)
                    {
                        endRoutePos = new Vector3(-8.25f, -0.25f, 0); //position of QB
                        transform.position = Vector3.MoveTowards(transform.position, endRoutePos, Time.deltaTime * speed * 1.25f); //run to QB for ball
                        if (transform.position.x == endRoutePos.x)
                        {
                            if (transform.position.y == endRoutePos.y)
                            {
                                Debug.Log("route completed");
                                routecompleted = true;
                                startRoute2 = false;
                            }
                        }
                    }
                }
            }
            else
            {
                routecompleted = true;
            }
        } else
        {
            endZonePos = new Vector3(100, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, endZonePos, Time.deltaTime * speed);
        }
    }
}