using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public GameObject WRwithBall;
    public bool TouchDown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WRwithBall"))
        {
            TouchDown = true;
            Debug.Log(TouchDown);
        }
    }
}
