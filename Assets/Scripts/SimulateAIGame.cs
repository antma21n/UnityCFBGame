using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateAIGame : MonoBehaviour
{

    public team team1;
    public team team2;

    public float homeModifer = 1.2F;
    public float varianceModifer1;// = ((UnityEngine.Random.Range(0, 100))/100) + 1;
    public float varianceModifer2;// = ((UnityEngine.Random.Range(0, 100)) / 100) + 1;
    public float team1score;
    public float team2score;

    // Start is called before the first frame update
    void Start()
    {
        varianceModifer1 = ((UnityEngine.Random.Range(0F, 100F)) / 100) + 1;
        varianceModifer2 = ((UnityEngine.Random.Range(0F, 100F)) / 100) + 1;
        //Debug.Log(team1.name);
        //Debug.Log(team2.name);
        team1score = (0.5F * team1.OffenseRating + 0.5F * team1.DefenseRating) * homeModifer * varianceModifer1;
        team2score = 0.5F * team2.OffenseRating + 0.5F * team2.DefenseRating * varianceModifer2;
        //Debug.Log(team1score);
        //Debug.Log(team2score);
        if (team1score > team2score)
        {
            Debug.Log(team1.name + " beats " + team2.name);
        }
        else
        {
            Debug.Log(team2.name + " beats " + team1.name);
        }
    }
}
