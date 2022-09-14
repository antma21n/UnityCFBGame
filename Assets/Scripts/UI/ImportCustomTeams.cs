using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
 

public class ImportCustomTeams : MonoBehaviour
{
    private string txtFileType;

    public string allText;
    public string filepath;
    public Text outputText;

    // Start is called before the first frame update
    void Start()
    {
        txtFileType = NativeFilePicker.ConvertExtensionToFileType("txt"); // Returns "application/pdf" on Android and "com.adobe.pdf" on iOS
        Debug.Log("txt's MIME/UTI is: " + txtFileType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImportTeam()
    {
        if (NativeFilePicker.IsFilePickerBusy())
            return;
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation cancelled");
            }
            else
            {
                Debug.Log("Picked file: " + path);
                filepath = path;
            }
                
                
        }, new string[] { txtFileType });
         
        Debug.Log("Permission result: " + permission);

        Debug.Log(filepath);

        LoadData(filepath);
    }

    public void LoadData(string filepath)
    {
        List<String> customData = new List<String>();
        StreamReader MyReader = new StreamReader(filepath);
        //StreamReader MyReader = new StreamReader(Application.dataPath + "/Scriptable Objects/Teams/txts/teamlist.txt");
        while ((allText = MyReader.ReadLine()) != null)
        {
            //Debug.Log(allText);
            customData.Add(allText);
            //outputText.text = allText;
        }
        MyReader.Close();
        //Debug.Log(customData);
        //Debug.Log(string.Join("\n", customData));
        outputText.text = string.Join("\n", customData);


    }
}
