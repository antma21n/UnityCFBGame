using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "offensiveplay", menuName = "offensiveplay")]
public class offensiveplay : ScriptableObject
{
    public bool runplay;
    public bool passplay;

    //QB
    public string QB_name = "QB";
    public Vector3 QB_pos;
    //RB
    public string RB_name = "RB";
    public Vector3 RB_pos;
    public float RB_speed;
    public string RB_route;
    public string RB_dir;
    //WR1
    public string WR1_name = "WR1";
    public Vector3 WR1_pos;
    public float WR1_speed;
    public string WR1_route;
    public string WR1_dir;
    //WR2
    public string WR2_name = "WR2";
    public Vector3 WR2_pos;
    public float WR2_speed;
    public string WR2_route;
    public string WR2_dir;
    //WR3
    public string WR3_name = "WR3";
    public Vector3 WR3_pos;
    public float WR3_speed;
    public string WR3_route;
    public string WR3_dir;
    //TE
    public string TE_name = "TE";
    public Vector3 TE_pos;
    public float TE_speed;
    public string TE_route;
    public string TE_dir;
    //LT
    //LG
    //C
    //RG
    //RT

    public void destroyOffense()
    {

    }

    public void spawnOffense()
    {

    }
}

