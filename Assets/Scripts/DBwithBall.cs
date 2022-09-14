using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DBwithBall : MonoBehaviour
{
    public GameObject TackleMade;
    public float speed;
    public int tackleDelay;
    public bool runner;
    public GameObject JukedPlayer;

    private bool tackleme = false;
    private Vector3 endZonePos;

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
        //if (GameObject.Find("WR with Ball"))
        //{
        //    Destroy(gameObject);
        //}
        endZonePos = new Vector3(-100, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, endZonePos, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WR"))
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
                //caught ball
                Destroy(gameObject);
                Destroy(other.gameObject);
                GameObject tm = Instantiate(TackleMade) as GameObject;
                tm.transform.position = transform.position;
                tm.name = "DB tackled";
                //reset scene
                //ResetScene(); 
            }
   
        }

        if (other.gameObject.CompareTag("Left Border"))
        {
            //caught ball
            Destroy(gameObject);
            GameObject tm = Instantiate(TackleMade) as GameObject;
            tm.transform.position = transform.position;
            tm.name = "DB tackled"; 
            //if colision with left wall. it is a pick 6
        }
    }
}
