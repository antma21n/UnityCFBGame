using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using TMPro;


//this script keeps track of reset for next play, points, downs, yards, and tds, interceptions, completions.
//restarting the scene isn't quite what we want. we just want to reset objects but keep the data.
//      aka: delete all objects, select new play, spawn objects on right yard lines
public class GameManager : MonoBehaviour
{
    //UI
    public Text DownYardStats;
    public Text Score;
    public Text announcerText;
    public TextMeshProUGUI  FieldGoalChance;
    public GameObject FourthDownCanvas;
    public bool fourthdownBool;
    public bool cancel4thdownPopUp;

    //private Game Vars
    private float playYards;
    private float scrimageLinePos;
    private bool WRtackled = false;
    private bool TouchDown;
    private double opponentSuccessChance;
    private double FGsuccessChance;

    //public Game Vars
    public int down;
    public float yardsToGo;
    public float yardLine;
    public float opponentYardLine;
    public int playerScore;
    public int enemyScore;

    //Game Objects
    public GameObject TackleMade;
    public GameObject QB;
    public GameObject WR;
    public GameObject RB;
    public GameObject OLine;
    public GameObject firstDownMarker;
    public GameObject ScrimageLine;
    public GameObject endZone;

    //Public OffensePlaySelection Vars
    public float WR1_x; public float WR1_y; public float WR1_z; public float WR1_speed; public string WR1_route; public string WR1_dir;
    public float WR2_x; public float WR2_y; public float WR2_z; public float WR2_speed; public string WR2_route; public string WR2_dir;
    public float WR3_x; public float WR3_y; public float WR3_z; public float WR3_speed; public string WR3_route; public string WR3_dir;
    public float TE_x; public float TE_y; public float TE_z; public float TE_speed; public string TE_route; public string TE_dir;
    public float RB_x; public float RB_y; public float RB_z; public float RB_speed; public string RB_route; public string RB_dir;

    //LT
    //LG
    //C
    //RG
    //RT

    //Public DefensePlaySelection Vars
    public bool zone_play;
    public defenseplay defenseplay;
    public defenseplay defenseplay2;
    public defenseplay defenseplay3;
    public defenseplay defenseplay4;
    public defenseplay defenseplay5;

    public offensiveplay currentplay;
    public offensiveplay hbdive;
    public offensiveplay hbinsidetackle;
    public offensiveplay hbofftackle;
    public offensiveplay hbcounter;
    public offensiveplay hbstretch;
    public offensiveplay test;
    public offensiveplay slants;
    public offensiveplay fourverts;
    public offensiveplay curls;
    public offensiveplay hailMerry;
    public offensiveplay bench;
    public offensiveplay stick;

    //not done
    public offensiveplay flood;
    public offensiveplay texas;
    public offensiveplay wheel;
    public offensiveplay mesh;
    public offensiveplay shotseam;
    public offensiveplay teAttack;
    public offensiveplay screen;
    public offensiveplay bubblescreenleft;
    public offensiveplay bubblescreenright;



    public bool runplay;
    private bool passplay;

    // Start is called before the first frame update
    void Start()
    {
        TouchDown = false;
        playerScore = 0;
        enemyScore = 0;
        down = 1;
        yardsToGo = 10;
        yardLine = 25;
        //GameObject sL = GameObject.Find("ScrimageLine");
        //scrimageLinePos = sL.transform.position.x;
        nextplay();
        //Debug.Log("Game Start at " + yardLine + " yard line");
        //Debug.Log("It is " + down + " down and " + yardsToGo + " yards to go");

        Score.text = playerScore + " - " + enemyScore;
        DownYardStats.text = "Down: " + down + " | Yards: " + yardsToGo.ToString("F0") + " | Yard Line: " + yardLine.ToString("F0");
        announcerText.text = "The Game Begins at the " + yardLine.ToString("F0") + " yard line. It is " + down + " and " + yardsToGo.ToString("F0") + " and the player has the ball to start this game.";
    }
 
    // Update is called once per frame
    void Update() 
    {
        DownYardStats.text = "Down: " + down + " | Yards: " + yardsToGo.ToString("F0") + " | Yard Line: " + yardLine.ToString("F0");
        GameObject GM = GameObject.Find("GameManager");
        if (GM.GetComponent<GameClock>().endOfGame)
        {
            announcerText.text = "The Game is Over. Final Score is "+playerScore+"-"+enemyScore;
            destroyfield();
        }

        if (GM.GetComponent<GameClock>().halftime)
        {
            DefensePlays(85);
            down = 1;
            yardsToGo = 10;
            yardLine = 25;
            nextplay();
            announcerText.text = "Halftime";
            GameObject.Find("GameManager").GetComponent<GameClock>().halftime = false;
        }

        if (GM.GetComponent<GameClock>().snaptimeRemaining == 0 && !GM.GetComponent<gamestart>().start_game)
        {
            announcerText.text = "Delay of Game. Offense. 5 yard penalty. Still " + down + " down";
            yardsToGo = yardsToGo + 5;
            GM.GetComponent<GameClock>().snaptimeRemaining = 30;
            GM.GetComponent<GameClock>().quartertimerIsRunning = false;
            nextplay();
        }
        
        if (GameObject.Find("WR with Ball"))
        {
            if (GameObject.Find("EndZone").GetComponent<EndZone>().TouchDown == true)
            {
                TouchDown = true;
            }
            else if (GameObject.Find("WR with Ball").GetComponent<WRwithBallScript>().WRtackled == true)
            {
                WRtackled = true;
            }
        }

        if (TouchDown == true)
        {
            Destroy(GameObject.Find("LandedFootball"));
            //Debug.Log("TOUCH DOWN");
            playerScore = playerScore + 7;
            down = 1;
            yardsToGo = 10;
            yardLine = 25;
            Score.text = playerScore + " - " + enemyScore;
            DownYardStats.text = "Down: " + down + " | Yards: " + yardsToGo.ToString("F0") + " | Yard Line: " + yardLine.ToString("F0");
            nextplay();//ResetScene();
            opponentYardLine = 25;
            DefensePlays(opponentYardLine);
            GM.GetComponent<GameClock>().snaptimeRemaining = 30;
            announcerText.text = "Offensive is back on the field. Last time out they scored a Touch Down";
            TouchDown = false;
        }
        else if (GameObject.Find("DB with Ball"))
        {
            if (GameObject.Find("WR with Ball"))
            {
                Destroy(GameObject.Find("LandedFootball"));
                Debug.Log("WR caught and tackled");
                Destroy(GameObject.Find("DB with Ball"));
                Destroy(GameObject.Find("WR with Ball"));
                GameObject tpos = GameObject.Find("DB with Ball");
                GameObject tm = Instantiate(TackleMade) as GameObject;
                tm.transform.position = tpos.transform.position;
                tm.name = "WR tackled";
                announcer(tm, false, false);
                nextplay();//ResetScene();
            }
        }
        else if (GameObject.Find("landed ball"))
        {
            Debug.Log("Incomplete");

            // PASS to gameclock to pause quarter clock. 
            GM.GetComponent<GameClock>().quartertimerIsRunning = false;

            Destroy(GameObject.Find("landed ball"));
            Destroy(GameObject.Find("LandedFootball"));
            GameObject sL = GameObject.Find("ScrimageLine");
            announcer(sL, false, false);
            nextplay();//ResetScene();
        }
        else if (WRtackled == true)
        {
            GameObject wrBall = GameObject.Find("WR with Ball");
            GameObject tm = Instantiate(TackleMade) as GameObject;
            tm.transform.position = GameObject.Find("WR with Ball").transform.position;
            tm.name = "WR tackled";
            Destroy(wrBall);
            Destroy(GameObject.Find("LandedFootball"));

            announcer(tm, false, false);
            nextplay();//ResetScene();
            WRtackled = false;
        }
        else if (GameObject.Find("DB tackled"))
        {
            Destroy(GameObject.Find("LandedFootball"));
            GameObject i = GameObject.Find("DB tackled");
            opponentYardLine = 100 - yardLine;
            DefensePlays(opponentYardLine);
            announcer(i, true, false);
            nextplay();//ResetScene();
            
        }
        else if (GameObject.Find("sack"))
        {
            Debug.Log("SACK");
            GameObject s = GameObject.Find("sack");
            //when sacked, i can still throw ball. should not be the case.
            announcer(s, false, true);
            nextplay();//ResetScene();
            Destroy(GameObject.Find("LandedFootball"));
            Destroy(GameObject.Find("sack"));
        }
    }

    public void nextplay()
    {
        destroyfield();

        //select play
        GameObject qb = Instantiate(QB) as GameObject; qb.transform.position = new Vector3(-9f, 0f, 0f); qb.name = "QB";

        GameObject sL = Instantiate(ScrimageLine) as GameObject;
        sL.name = "ScrimageLine";
        scrimageLinePos = sL.transform.position.x;
        sL.transform.position = new Vector3(-8, 0, 0);

        GameObject downMarker = Instantiate(firstDownMarker) as GameObject;
        downMarker.name = "First Down Marker";
        downMarker.transform.position = new Vector3(sL.transform.position.x + yardsToGo/2, 0, 0);

        GameObject ez = Instantiate(endZone) as GameObject;
        ez.name = "EndZone";
        ez.transform.position = new Vector3((100f - yardLine)/2 + sL.transform.position.x + 2.5f, 0, 0);
        
        spawnOffense(currentplay);


        //early defense AI needs more plays and needs more strategy
        int RandomPlay = UnityEngine.Random.Range(1, 6);
        Debug.Log(RandomPlay);
        if(RandomPlay == 1)
        {
            defenseplay.SpawnDefense();
        } 
        else if(RandomPlay == 2) {
            defenseplay2.SpawnDefense();
        }
        else if (RandomPlay == 3)
        {
            defenseplay3.SpawnDefense();
        }
        else if (RandomPlay == 4)
        {
            defenseplay4.SpawnDefense();
        }
        else if (RandomPlay == 5)
        {
            defenseplay5.SpawnDefense();
        }
        //announcerText.text = "Pass Complete to " + "It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
    }

    void spawnWR(GameObject wr, string name, float x, float y, float z, float speed, string routeType, string routeDirection) //make code to streamline this
    {
        wr.transform.position = new Vector3(x, y, z);
        wr.name = name;
        wr.GetComponent<WRScript>().routeType = routeType;
        wr.GetComponent<WRScript>().routeDirection = routeDirection;
        wr.GetComponent<WRScript>().speed = speed;
    }
    void spawnOL(GameObject o, string name, float x, float y, float z, float speed) //make code to streamline this
    {
        o.transform.position = new Vector3(x, y, z);
        o.name = name;
        o.GetComponent<Oline>().speed = speed;
    }

    void announcer(GameObject tm, bool intercepted, bool sack)
    {
        GameObject GM = GameObject.Find("GameManager");
        GM.GetComponent<GameClock>().snaptimeRemaining = 30;


        GameObject firstDownMarker = GameObject.Find("First Down Marker");

        if (sack == true)
        {
            playYards = -5;
        }
        else {
            playYards = (tm.transform.position.x - scrimageLinePos) * 2;
        }
        yardLine = yardLine + playYards;
        if (sack == true)
        {
            down = down + 1;
            playYards = -5;
            yardsToGo = yardsToGo - playYards;
            //Debug.Log("Sack for: " + playYards + "yards");
            int TextIndex = UnityEngine.Random.Range(1, 4);
            if(TextIndex == 1) {announcerText.text = "SACK! QB got hit hard for " + playYards.ToString("F0") +" yards. It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
            } else if(TextIndex == 2) {announcerText.text = "Big sack on the QB. Offense moves back " + playYards.ToString("F0") +" yards. It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
            } else if(TextIndex == 3) {announcerText.text = "Defensive fronts makes a big play and gets a sack for " + playYards.ToString("F0") +" yards. It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
            } else if(TextIndex == 4) {announcerText.text = "QB wasn't quick enough and gets sacked for " + playYards.ToString("F0") +" yards. It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
            } 
        }
        else if (intercepted == true)
        {
            //Debug.Log("INTERCEPTED");
            down = 1;
            yardsToGo = 10;
            yardLine = 25;
            int TextIndex = UnityEngine.Random.Range(1, 4);
            if(TextIndex == 1) {announcerText.text = "Offense looks to shake off that last interception. It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
            } else if(TextIndex == 2) {announcerText.text = "Offense takes over on " + down + " and " + yardsToGo.ToString("F0") + ". Last time out the QB threw a poor pass and got intercepted. Let's see what happens this drive.";
            } else if(TextIndex == 3) {announcerText.text = "Offense comes back onto the field after a brutal interception last drive. Fresh drive with " + down + " and " + yardsToGo.ToString("F0") + " at the " + yardLine.ToString("F0") + " yard line." ;
            } else if(TextIndex == 4) {announcerText.text = "Defense made a massive stop last drive. Can they do it again? Offense starts at the " + yardLine.ToString("F0") + " yard line with " + down + " and " + yardsToGo.ToString("F0") + ".";
            } 
        }
        else if (firstDownMarker.transform.position.x < tm.transform.position.x)
        {
            //Debug.Log("First Down!");
            down = 1;
            yardsToGo = 10;
            //Debug.Log("Pass Completed for: " + playYards + "yards");
            if (yardLine > 90)
            {
                yardsToGo = 100 - yardLine;
            }
            int TextIndex = UnityEngine.Random.Range(1, 4);
            if(TextIndex == 1) {announcerText.text = "FIRST DOWN! Play complete for "+ playYards.ToString("F0") + ". It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
            } else if(TextIndex == 2) {announcerText.text = "Offense moves the chains and gets a first down! Play complete for "+ playYards.ToString("F0") + ". It is now " + down + " and " + yardsToGo.ToString("F0") + " to go.";
            } else if(TextIndex == 3) {announcerText.text = "Offense gets a new set of downs. Play complete for "+ playYards.ToString("F0") + ". It is now " + down + " and " + yardsToGo.ToString("F0") + " to go.";
            } else if(TextIndex == 4) {announcerText.text = "Offense keeps drive alive with another first down. It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
            }
        }
        else if (down < 4)
        {
            down = down + 1;
            yardsToGo = yardsToGo - playYards;
            //Debug.Log("Pass Completed for: " + playYards + "yards");
            announcerText.text = "Play complete for "+ playYards.ToString("F0") + " yards. It is now " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line.";
        }
        else
        {
            //Debug.Log("TURN OVER ON DOWNS");
            down = 1;
            yardsToGo = 10;
            opponentYardLine = 100 - yardLine;
            yardLine = 25;
            //idk if I want this here maybe I just want a variable called turnover to equal true and run this in the update section
            int TextIndex = UnityEngine.Random.Range(1, 4);
            if(TextIndex == 1) {announcerText.text = "Last drive the offense turned the ball over on downs. They try again with " + down + " and " + yardsToGo.ToString("F0") + " at the " + yardLine.ToString("F0") + " yard line.";
            } else if(TextIndex == 2) {announcerText.text = "Offensive couldn't keep the drive alive last time out. What can they do here with " + down + " and " + yardsToGo.ToString("F0") + " to go at the " + yardLine.ToString("F0") + " yard line?";
            }
            DefensePlays(opponentYardLine);
        }
        DownYardStats.text = "Down: " + down + " | Yards: " + yardsToGo.ToString("F0") + " | Yard Line: " + yardLine.ToString("F0");
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DefensePlays(float opponentYardLine)
    {
        GameObject GM = GameObject.Find("GameManager");
        GM.GetComponent<GameClock>().quartertimerIsRunning = false;
        GM.GetComponent<GameClock>().quartertimeRemaining = GM.GetComponent<GameClock>().quartertimeRemaining - UnityEngine.Random.Range(10, 30);
        opponentSuccessChance = (0.0046 * Math.Pow(opponentYardLine, 2)) + (0.2368 * (opponentYardLine)) + 25.823;
         
        if (opponentSuccessChance * 0.85 > UnityEngine.Random.Range(1, 101))
        {
            enemyScore = enemyScore + 7;
        }
        else if (opponentSuccessChance > UnityEngine.Random.Range(1, 101))
        {
            enemyScore = enemyScore + 3;
        }
        //update what yard line player start
        Score.text = playerScore + " - " + enemyScore;
    }

    public void HBdive()
    {
        currentplay = hbdive;
    }

    public void HBinsideTackle()
    {
        currentplay = hbinsidetackle;
    }

    public void HBoffTackle()
    {
        currentplay = hbofftackle;
    }

    public void HBcounter()
    {
        currentplay = hbcounter;
        //nextplay();
    }

    public void HBstretch()
    {
        currentplay = hbstretch;
    }

    public void Test()
    {
        currentplay = test;
    }

    public void Slants()
    {
        currentplay = slants;
    }

    public void FourVerts()
    {
        currentplay = fourverts;
    }
    
    public void Curls()
    {
        currentplay = curls;
    }

    public void TEattack()
    {
        currentplay = teAttack;
    }
    public void Bench()
    {
        currentplay = bench;
    }
    public void HailMerry()
    {
        currentplay = hailMerry;
    }
    public void Stick()
    {
        currentplay = stick;
    }

    public void Flood()
    {
        currentplay = flood;
    }
    public void Texas()
    {
        currentplay = texas;
    }
    public void Wheel()
    {
        currentplay = wheel;
    }
    public void Mesh()
    {
        currentplay = mesh;
    }
    public void Shotseam()
    {
        currentplay = shotseam;
    }
    public void Screen()
    {
        currentplay = screen;
    }
    public void BubbleBcreenLeft()
    {
        currentplay = bubblescreenleft;
    }
    public void BubbleBcreenRight()
    {
        currentplay = bubblescreenright;
    }

    public void FieldGoalAttempt()
    {
        
        FGsuccessChance = (-0.0308* Math.Pow(100 - yardLine,2)) - (0.1512 * (100 - yardLine)) + 98.276;
        //Add this to the chance text
        //FieldGoalChance.text = "Kick a Field Goal\nChance to score: " + FGsuccessChance.ToString("F2");
        if(FGsuccessChance > UnityEngine.Random.Range(1, 101))
        {
            playerScore = playerScore + 3;
            announcerText.text = "Offensive takes the field again after geting 3 points last drive.";
        } else {
            announcerText.text = "Offensive takes the field after missing a field goal last time out.";
        }
        opponentYardLine = 25;
        DefensePlays(opponentYardLine);
        down = 1;
        yardsToGo = 10;
        yardLine = 25;
        nextplay();
        DownYardStats.text = "Down: " + down + " | Yards: " + yardsToGo.ToString("F0") + " | Yard Line: " + yardLine.ToString("F0");
    }

    public void Punt()
    {
        
        if (yardLine < 60)
        {
            opponentYardLine = 100 - yardLine - UnityEngine.Random.Range(25, 45);
            if (opponentYardLine < 0)
            {
                opponentYardLine = 25;
            }
        }
        else
        {
            opponentYardLine = 100 - yardLine - UnityEngine.Random.Range(20, 35);

            if (opponentYardLine < 0)
            {
                opponentYardLine = 25;

            }
        }
        DefensePlays(opponentYardLine);
        down = 1;
        yardsToGo = 10;
        yardLine = 40;
        announcerText.text = "Offensive couldn't get it down last drive. Let's see if they have more luck this time out.";
        DownYardStats.text = "Down: " + down + " | Yards: " + yardsToGo.ToString("F0") + " | Yard Line: " + yardLine.ToString("F0");
        nextplay();
    }

    void spawnOffense(offensiveplay currentplay)
    {
        //Debug.Log("Current Play: " + currentplay);
        //Debug.Log(down);

        if (cancel4thdownPopUp == false)
        {
            if (down == 4)
            {
                announcerText.text = "It is forth down and the offense of short of the first down line. What play will they choose now?";
                //anouncer(tm,false,false);
                FourthDownCanvas.SetActive(true);
                fourthdownBool = true;
            }
        } else
        {
            cancel4thdownPopUp = false;
        }
        

        //if play1 use play1 scriptable object
        runplay = currentplay.runplay;
        //spawn stuff here;
        //GameObject wr = Instantiate(WR) as GameObject; spawnWR(wr, currentplay.WR1_name, WR1_x, WR1_y, WR1_z, WR1_speed, WR1_route, WR1_dir);
        WR1_x = currentplay.WR1_pos.x;
        WR1_y = currentplay.WR1_pos.y;
        WR1_z = currentplay.WR1_pos.z;
        WR1_speed = currentplay.WR1_speed;
        WR1_route = currentplay.WR1_route;
        WR1_dir = currentplay.WR1_dir;

        WR2_x = currentplay.WR2_pos.x;
        WR2_y = currentplay.WR2_pos.y;
        WR2_z = currentplay.WR2_pos.z;
        WR2_speed = currentplay.WR2_speed;
        WR2_route = currentplay.WR2_route;
        WR2_dir = currentplay.WR2_dir;

        WR3_x = currentplay.WR3_pos.x;
        WR3_y = currentplay.WR3_pos.y;
        WR3_z = currentplay.WR3_pos.z;
        WR3_speed = currentplay.WR3_speed;
        WR3_route = currentplay.WR3_route;
        WR3_dir = currentplay.WR3_dir;

        TE_x = currentplay.TE_pos.x;
        TE_y = currentplay.TE_pos.y;
        TE_z = currentplay.TE_pos.z;
        TE_speed = currentplay.TE_speed;
        TE_route = currentplay.TE_route;
        TE_dir = currentplay.TE_dir;

        RB_x = currentplay.RB_pos.x;
        RB_y = currentplay.RB_pos.y;
        RB_z = currentplay.RB_pos.z;
        RB_speed = currentplay.RB_speed;
        RB_route = currentplay.RB_route;
        RB_dir = currentplay.RB_dir;

        GameObject wr = Instantiate(WR) as GameObject; spawnWR(wr, currentplay.WR1_name, WR1_x, WR1_y, WR1_z, WR1_speed, WR1_route, WR1_dir);
        GameObject wr2 = Instantiate(WR) as GameObject; spawnWR(wr2, currentplay.WR2_name, WR2_x, WR2_y, WR2_z, WR2_speed, WR2_route, WR2_dir);
        GameObject wr3 = Instantiate(WR) as GameObject; spawnWR(wr3, currentplay.WR3_name, WR3_x, WR3_y, WR3_z, WR3_speed, WR3_route, WR3_dir);
        GameObject te = Instantiate(WR) as GameObject; spawnWR(te, currentplay.TE_name, TE_x, TE_y, TE_z, TE_speed, TE_route, TE_dir);
        GameObject rb = Instantiate(RB) as GameObject; spawnWR(rb, currentplay.RB_name, RB_x, RB_y, RB_z, RB_speed, RB_route, RB_dir);

        GameObject lt = Instantiate(OLine) as GameObject; spawnOL(lt, "LT", -8f, 1f, 0f, 0.5f);
        GameObject lg = Instantiate(OLine) as GameObject; spawnOL(lg, "LG", -8f, 0.48f, 0f, 0.5f);
        GameObject c = Instantiate(OLine) as GameObject; spawnOL(c, "C", -8f, 0f, 0f, 0.5f);
        GameObject rg = Instantiate(OLine) as GameObject; spawnOL(rg, "RG", -8f, -0.44f, 0f, 0.5f);
        GameObject rt = Instantiate(OLine) as GameObject; spawnOL(rt, "RT", -8f, -0.90f, 0f, 0.5f);

    }

    public void destroyfield()
    {
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

        Destroy(GameObject.Find("WR with Ball"));
        Destroy(GameObject.Find("DB with Ball"));

        defenseplay.DestroyDefense();
    }

    public void fourthdownUItoggle()
    {
        fourthdownBool = false;
    }

    public void cancelPopUp()
    {
        cancel4thdownPopUp = true;
    }

}