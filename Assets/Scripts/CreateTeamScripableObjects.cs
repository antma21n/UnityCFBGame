using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class CreateTeamScripableObjects : MonoBehaviour
{
    //public team team;
    public string allText;
    public string path;

    // Start is called before the first frame update
    void Start()
    {
        List<String> allTeams = new List<String>();
        StreamReader MyReader = new StreamReader(Application.dataPath + "/Scriptable Objects/Teams/txts/teamlist.txt");
        while ((allText = MyReader.ReadLine()) != null)
        {
            //Debug.Log(allText);
            allTeams.Add(allText);
        }
        MyReader.Close();
        //Debug.Log(allTeams);

        for (int i = 0; i < 129; i++)
        {
            createScripable(allTeams[i]);
        }
    }
    
    //function to create scriptable object for each time
    void createScripable(string SchoolName)
    {
#if UNITY_EDITOR
        var obj = ScriptableObject.CreateInstance<team>();
        path = "Assets/Scriptable Objects/Teams/" + SchoolName + ".asset";

        List<String> data = new List<String>();
        StreamReader MyReader = new StreamReader(Application.dataPath + "/Scriptable Objects/Teams/txts/"+SchoolName+".txt");
        while ((allText = MyReader.ReadLine()) != null)
        {
            //Debug.Log(allText);
            data.Add(allText);
        }
        MyReader.Close();
        //Debug.Log(allTeams);

        obj.name = data[0];
        obj.primaryColor = "";
        obj.secondaryColor = "";
        obj.wins = int.Parse(data[1]);
        obj.loses = int.Parse(data[2]);
        obj.OffenseRating = int.Parse(data[3]);
        obj.DefenseRating = int.Parse(data[4]);
        obj.OffenseRanking = int.Parse(data[5]);
        obj.DefenseRanking = int.Parse(data[6]);
        obj.Rank = int.Parse(data[7]);

        UnityEditor.AssetDatabase.CreateAsset(obj, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

}
