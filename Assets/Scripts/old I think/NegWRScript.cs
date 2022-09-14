using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegWRScript : MonoBehaviour
{
    public GameObject Square;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Catch Made");
        if (other.gameObject.CompareTag("ball"))
        {
            GameObject e = Instantiate(Square) as GameObject;
            e.transform.position = transform.position;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
