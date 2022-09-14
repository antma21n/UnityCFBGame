using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "defenseplay", menuName = "defenseplay")]
public class defenseplay : ScriptableObject
{
    //DE1
    public string DE1_name = "DE1";
    public Vector3 DE1_pos;
    public float DE1_speed;
    public bool DE1_unblockable;
    //DE2
    public string DE2_name = "DE2";
    public Vector3 DE2_pos;
    public float DE2_speed;
    public bool DE2_unblockable;
    //DT1
    public string DT1_name = "DT1";
    public Vector3 DT1_pos;
    public float DT1_speed;
    public bool DT1_unblockable;
    //DT2
    public string DT2_name = "DT2";
    public Vector3 DT2_pos;
    public float DT2_speed;
    public bool DT2_unblockable;
    //LB1
    public string LB1_name = "LB1";
    public string LB1_zoneName = "LB1 Zone";
    public Vector3 LB1_pos;
    public float LB1_speed;
    public float LB1_delay;
    public int LB1_catchChance;
    public bool LB1_zone_play;
    //LB2
    public string LB2_name = "LB2";
    public string LB2_zoneName = "LB2 Zone";
    public Vector3 LB2_pos;
    public float LB2_speed;
    public float LB2_delay;
    public int LB2_catchChance;
    public bool LB2_zone_play;
    //LB3
    public string LB3_name = "LB3";
    public string LB3_zoneName = "LB3 Zone";
    public Vector3 LB3_pos;
    public float LB3_speed;
    public float LB3_delay;
    public int LB3_catchChance;
    public bool LB3_zone_play;
    //LBSpy
    //CB1
    public string CB1_name = "CB1";
    public string CB1_zoneName = "CB1 Zone";
    public Vector3 CB1_pos;
    public float CB1_speed;
    public float CB1_delay;
    public int CB1_catchChance;
    public bool CB1_zone_play;
    //CB2
    public string CB2_name = "CB2";
    public string CB2_zoneName = "CB2 Zone";
    public Vector3 CB2_pos;
    public float CB2_speed;
    public float CB2_delay;
    public int CB2_catchChance;
    public bool CB2_zone_play;
    //FS
    public string FS_name = "FS";
    public string FS_zoneName = "FS Zone";
    public Vector3 FS_pos;
    public float FS_speed;
    public float FS_delay;
    public int FS_catchChance;
    public bool FS_zone_play;
    //SS
    public string SS_name = "SS";
    public string SS_zoneName = "SS Zone";
    public Vector3 SS_pos;
    public float SS_speed;
    public float SS_delay;
    public int SS_catchChance;
    public bool SS_zone_play;

    //GameObjects
    public GameObject DLine;
    public GameObject DB;
    public GameObject DBzone;
    public GameObject LBZoneBubble;
    public GameObject CBZoneBubble;
    public GameObject SSZoneBubble;
    public GameObject FSZoneBubble;

    public void DestroyDefense()
    {
        Destroy(GameObject.Find(DE1_name));
        Destroy(GameObject.Find(DE2_name));
        Destroy(GameObject.Find(DT1_name));
        Destroy(GameObject.Find(DT2_name));

        Destroy(GameObject.Find(LB1_name));
        Destroy(GameObject.Find(LB2_name));
        Destroy(GameObject.Find(LB3_name));
        Destroy(GameObject.Find("LB Spy"));

        Destroy(GameObject.Find(CB1_name));
        Destroy(GameObject.Find(CB2_name));

        Destroy(GameObject.Find(SS_name));
        Destroy(GameObject.Find(FS_name));

        Destroy(GameObject.Find(LB1_zoneName));
        Destroy(GameObject.Find(LB2_zoneName));
        Destroy(GameObject.Find(LB3_zoneName));
        Destroy(GameObject.Find(CB1_zoneName));
        Destroy(GameObject.Find(CB2_zoneName));
        Destroy(GameObject.Find(SS_zoneName));
        Destroy(GameObject.Find(FS_zoneName));
    }

    public void SpawnDefense() 
    {
         
        spawnDL(DE1_name, DE1_pos.x, DE1_pos.y, DE1_pos.z, DE1_speed, DE1_unblockable);
        spawnDL(DE2_name, DE2_pos.x, DE2_pos.y, DE2_pos.z, DE2_speed, DE2_unblockable);
        spawnDL(DT1_name, DT1_pos.x, DT1_pos.y, DT1_pos.z, DT1_speed, DT1_unblockable);
        spawnDL(DT2_name, DT2_pos.x, DT2_pos.y, DT2_pos.z, DT2_speed, DT2_unblockable);

        
        spawnDB(LB1_name, LB1_pos.x, LB1_pos.y, LB1_pos.z, LB1_delay, LB1_speed, LB1_catchChance, LB1_zoneName, LB1_zone_play);
        spawnDB(LB2_name, LB2_pos.x, LB2_pos.y, LB2_pos.z, LB2_delay, LB2_speed, LB2_catchChance, LB2_zoneName, LB2_zone_play);
        spawnDB(LB3_name, LB3_pos.x, LB3_pos.y, LB3_pos.z, LB3_delay, LB3_speed, LB3_catchChance, LB3_zoneName, LB3_zone_play);
        //if spy dont spawn lb 3, spawn lb spy
        spawnDB(CB1_name, CB1_pos.x, CB1_pos.y, CB1_pos.z, CB1_delay, CB1_speed, CB1_catchChance, CB1_zoneName, CB1_zone_play);
        spawnDB(CB2_name, CB2_pos.x, CB2_pos.y, CB2_pos.z, CB2_delay, CB2_speed, CB2_catchChance, CB2_zoneName, CB2_zone_play);
        spawnDB(SS_name, SS_pos.x, SS_pos.y, SS_pos.z, SS_delay, SS_speed, SS_catchChance, SS_zoneName, SS_zone_play);
        spawnDB(FS_name, FS_pos.x, FS_pos.y, FS_pos.z, FS_delay, FS_speed, FS_catchChance, FS_zoneName, FS_zone_play);


    }
    void spawnDL(string name, float x, float y, float z, float speed, bool unblockable) //make code to streamline this
    {
        GameObject d = Instantiate(DLine) as GameObject;
        d.transform.position = new Vector3(x, y, z);
        d.name = name;
        d.GetComponent<Dline>().speed = speed;
        //d.GetComponent<DLine>().unblockable = unblockable;
    }
    void spawnDB(string name, float x, float y, float z, float delay, float speed, int catchChance, string zoneName, bool zone_play)
    {
        if (zone_play == true)
        {
            GameObject lb = Instantiate(DBzone) as GameObject;
            catchChance = 100;
            lb.transform.position = new Vector3(x, y, z);
            lb.name = name;
            lb.GetComponent<DBZoneScript>().speed = speed;
            lb.GetComponent<DBZoneScript>().delay = delay;
            lb.GetComponent<DBZoneScript>().catchChance = catchChance;
            lb.GetComponent<DBZoneScript>().zoneName = zoneName;
            if (zoneName == LB1_zoneName || zoneName == LB2_zoneName || zoneName == LB3_zoneName)
            {
                GameObject zone = Instantiate(LBZoneBubble) as GameObject;
                zone.transform.position = new Vector3(x, y, z);
                zone.name = zoneName;
            }
            if (zoneName == CB1_zoneName || zoneName == CB2_zoneName)
            {
                GameObject zone = Instantiate(CBZoneBubble) as GameObject;
                zone.transform.position = new Vector3(x, y, z); //SOMETIMES ZONE MAYBE SOMEWHERE ELSE NEED TO FIX
                zone.name = zoneName;
            }
            if (zoneName == SS_zoneName)
            {
                GameObject zone = Instantiate(SSZoneBubble) as GameObject;
                zone.transform.position = new Vector3(x, y, z);
                zone.name = zoneName;
            }
            if (zoneName == FS_zoneName)
            {
                GameObject zone = Instantiate(FSZoneBubble) as GameObject;
                zone.transform.position = new Vector3(x, y, z);
                zone.name = zoneName;
            }
        }
        else //if blitz_play == true spawn a fast dlinemen to charge qb
        {
            GameObject lb = Instantiate(DB) as GameObject;
            catchChance = 100;
            lb.transform.position = new Vector3(x, y, z);
            lb.name = name;
            lb.GetComponent<DBScript>().speed = speed;
            lb.GetComponent<DBScript>().delay = delay;
            lb.GetComponent<DBScript>().catchChance = catchChance;
        }
    }
}