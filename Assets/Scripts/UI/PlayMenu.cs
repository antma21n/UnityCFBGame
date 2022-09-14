using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //GameObject myCharactersScript = 
        //GameObject.Find("GameManager").GetComponent<GameManager>().nextplay();
        //myCharactersScript.NextPlay();
        SceneManager.LoadScene("Field");
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
