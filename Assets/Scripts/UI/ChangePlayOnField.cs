using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePlayOnField : MonoBehaviour
{

    public void SelectPlay()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RemovePlayers()
    {
        //make a function in gamemanager that is pulled into here
        GameObject GM = GameObject.Find("GameManager");
        GM.GetComponent<gamestart>().start_game = false;
        Destroy(GameObject.Find("landed ball"));
        Destroy(GameObject.Find("WR tackled"));
        Destroy(GameObject.Find("DB tackled"));
        Destroy(GameObject.Find("First Down Marker"));
        Destroy(GameObject.Find("ScrimageLine"));
        Destroy(GameObject.Find("EndZone"));

        //kill all objects
        Destroy(GameObject.Find("QB"));
        Destroy(GameObject.Find("RB"));
        Destroy(GameObject.Find("WR1"));
        Destroy(GameObject.Find("WR2"));
        Destroy(GameObject.Find("WR3"));
        Destroy(GameObject.Find("TE"));
        Destroy(GameObject.Find("LT"));
        Destroy(GameObject.Find("LG"));
        Destroy(GameObject.Find("C"));
        Destroy(GameObject.Find("RG"));
        Destroy(GameObject.Find("RT"));

        Destroy(GameObject.Find("LinemenBattle")); //not deleting all objects
        Destroy(GameObject.Find("LinemenBattle"));
        Destroy(GameObject.Find("LinemenBattle"));
        Destroy(GameObject.Find("LinemenBattle"));
        Destroy(GameObject.Find("LinemenBattle"));

        Destroy(GameObject.Find("WR with Ball"));
        Destroy(GameObject.Find("DB with Ball"));

        Destroy(GameObject.Find("DE1"));
        Destroy(GameObject.Find("DE2"));
        Destroy(GameObject.Find("DT1"));
        Destroy(GameObject.Find("DT2"));

        Destroy(GameObject.Find("LB1"));
        Destroy(GameObject.Find("LB2"));
        Destroy(GameObject.Find("LB3"));
        Destroy(GameObject.Find("LB Spy"));

        Destroy(GameObject.Find("CB1"));
        Destroy(GameObject.Find("CB2"));

        Destroy(GameObject.Find("SS"));
        Destroy(GameObject.Find("FS"));

        Destroy(GameObject.Find("LB Zone")); //not deleting all zones
        Destroy(GameObject.Find("CB Zone")); //not deleting all zones
        Destroy(GameObject.Find("SS Zone")); //not deleting all zones
        Destroy(GameObject.Find("FS Zone")); //not deleting all zones
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
