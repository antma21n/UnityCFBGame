using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dline : MonoBehaviour
{
    public GameObject Sack;
    public GameObject linemenStruggle;
    public float speed;
    private List<string> collisions = new List<string>();
    public bool unblockable;
    public bool winner;

    // Start is called before the first frame update
    void Start()
    {
        UnblockableChance();
        //need a pause to make first block or slow speed to make first block
    }

    // Update is called once per frame
    void Update()
    {
        if (winner == true)
        {
            unblockable = true;
        }
        
        if (GameObject.Find("GameManager").GetComponent<gamestart>().start_game == true)
        {
            if (GameObject.FindObjectOfType<receiverwithball>())
            {
                receiverwithball closestEnemy = GameObject.FindObjectOfType<receiverwithball>();
                transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, Time.deltaTime * speed);
                Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
            } else
            {
                if (GameObject.Find("QB"))
                {
                    GameObject qb = GameObject.Find("QB");
                    transform.position = Vector3.MoveTowards(transform.position, qb.transform.position, Time.deltaTime * speed);
                }
            }

        }
    }
    
    //I dont like how it 2 offensive linemen could get deleted if the d line touches 2 of them at the same time.
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.CompareTag("QB"))
        {
            GameObject qb = GameObject.Find("QB");
            if (GameObject.FindObjectOfType<nosack>() == false)
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                GameObject s = Instantiate(Sack) as GameObject;
                s.transform.position = transform.position;
                s.name = "sack";
            }
        }
        //again I wanna stop using tags but idk how
        if (unblockable == false) {
            if (other.gameObject.CompareTag("OLine"))
            {
                if (other.gameObject.GetComponent<Oline>().winner == false)
                {
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                    GameObject lS = Instantiate(linemenStruggle) as GameObject;
                    lS.transform.position = transform.position;
                    lS.name = this.name;
                }

            }
        }
    }

    private void UnblockableChance()
    {
        float chance = UnityEngine.Random.Range(0F, 100F);
        if (chance > 90)
        {
            unblockable = true;
        }
    }
}