//
// CURRENTLY NOT USED BUT WILL BE USED FOR QB PHYSICS and MOVEMENT. May integrate ball control into this script as well. 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBScript : MonoBehaviour
{
    public Vector2 direction;
    public float LaunchForce;

    public GameObject Arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowPos = transform.position;
        direction = MousePos - bowPos;
        FaceMouse();


        if (Input.GetMouseButtonUp(0))
        {
            //when we hit space key we want to launch a football
            Shoot();
        }
    }

    void FaceMouse()
    {
        transform.right = direction;
    }

    void Shoot()
    {
        GameObject ArrowIns = Instantiate(Arrow, transform.position, transform.rotation);

        //Apply force to the arrow we just created
        ArrowIns.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce);
    }

}
