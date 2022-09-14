using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Change the name of this script. 

public class BallControl2 : MonoBehaviour
{
    public float power = 5f;
    Rigidbody2D rb;
    LineRenderer lr;
    Vector2 DragStartPos;
    public GameObject Arrow;
    public GameObject LandMarker;
    public Vector2 EndTrajectory;

    public Vector2 minPower;
    public Vector2 maxPower;
    Vector2 Vect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameObject.FindWithTag("ball"))
        {

            if (Input.GetMouseButtonDown(0))
            {
                DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vect = new Vector2(Mathf.Clamp(DragStartPos.x - DragEndPos.x, minPower.x, maxPower.x), Mathf.Clamp(DragStartPos.y - DragEndPos.y, minPower.y, maxPower.y));
                Vector2 _velocity = Vect * power;

                Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);
                lr.positionCount = trajectory.Length;
                Vector3[] positions = new Vector3[trajectory.Length];
                for (int i = 0; i < trajectory.Length; i++)
                {
                    positions[i] = trajectory[i];
                }
                lr.SetPositions(positions);
                EndTrajectory = positions[trajectory.Length - 1];

            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vect = new Vector2(Mathf.Clamp(DragStartPos.x - DragEndPos.x, minPower.x, maxPower.x), Mathf.Clamp(DragStartPos.y - DragEndPos.y, minPower.y, maxPower.y));
                Vector2 _velocity = Vect * power;

                rb.velocity = _velocity;
                //when we hit space key we want to launch a football
                GameObject ArrowIns = Instantiate(Arrow, transform.position, transform.rotation);
                //Apply force to the arrow we just created
                ArrowIns.name = "Football";
                ArrowIns.GetComponent<Rigidbody2D>().velocity = _velocity;
                lr.positionCount = 0; //kills trajectory line
                GameObject m = Instantiate(LandMarker) as GameObject;
                m.transform.position = new Vector3(EndTrajectory[0], EndTrajectory[1], 0);
            }
        }
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

}
