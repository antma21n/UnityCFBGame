using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinemenBattle : MonoBehaviour
{
    public GameObject Sack;
    public GameObject OLine;
    public GameObject DLine;
    public float speed;
    private List<string> collisions = new List<string>();
    private int battleCheck;
     
    // Start is called before the first frame update
    void Start()
    {
        //after x seconds, destory this object and respawn linemen. linemen have a delay before next battle or just dont battle again 

        //StartCoroutine(battle());

         
        
    }
    IEnumerator battle()
    {
        //print(Time.time);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        //print(Time.time);
        battleCheck = UnityEngine.Random.Range(1, 101);
        Destroy(gameObject);

        GameObject ol = Instantiate(OLine) as GameObject;
        ol.transform.position = transform.position;
        ol.name = this.name;
        
        GameObject dl = Instantiate(DLine) as GameObject;
        dl.transform.position = transform.position;
        dl.name = this.name;

        if (battleCheck > 50)
        {
            
            ol.GetComponent<Oline>().winner = true;
            dl.GetComponent<Dline>().winner = false;
        }
        else
        {

            ol.GetComponent<Oline>().winner = false;
            dl.GetComponent<Dline>().winner = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindObjectOfType<receiverwithball>() == true)
        {
            receiverwithball bh = GameObject.FindObjectOfType<receiverwithball>();
            transform.position = Vector3.MoveTowards(transform.position, bh.transform.position, Time.deltaTime * speed);
            Debug.DrawLine(this.transform.position, bh.transform.position);
        } else
        {
            if (GameObject.Find("QB"))
            {
                GameObject qb = GameObject.Find("QB");
                transform.position = Vector3.MoveTowards(transform.position, qb.transform.position, Time.deltaTime * speed);
            }
        }
    }

    //I dont like how it 2 offensive linemen could get deleted if the d line touches 2 of them at the same time.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("QB"))
        {
            GameObject qb = GameObject.Find("QB");
            //dont sack when ball is thrown or when ball is in another players hands (trying to figure out if or statments)
            //if (!GameObject.Find("Football")) //|| (!GameObject.Find("WR with Ball")) || (!GameObject.Find("DB with Ball")))
            if (GameObject.FindObjectOfType<nosack>() == false)
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                GameObject s = Instantiate(Sack) as GameObject;
                s.transform.position = transform.position;
                s.name = "sack";
            }
        }
    }
}


//NOTES
// During run play make offensive lines move at close to zero speed and away from QB
// After a couple of seconds delete battle and respawn linemen with one winning
