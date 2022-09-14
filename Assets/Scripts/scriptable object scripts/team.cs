using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "team", menuName = "team")]
public class team : ScriptableObject
{
    public new string name;
    public string primaryColor;
    public string secondaryColor;
    public int wins;
    public int loses;
    //public list schedule;
    //public list results;
    public int OffenseRating;
    public int DefenseRating;
    public int OffenseRanking;
    public int DefenseRanking;
    public int Rank;
}

