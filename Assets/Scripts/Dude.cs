using UnityEngine;

public class Dude : MonoBehaviour
{

    //Weaponry
    public GameObject melon;
    public Vector2 groundDispenseVelocity;
    public Vector2 verticalDispenseVelocity;

    //References
    public Transform trnsGun;
    public Transform trnsGunTip;
    //public SpriteRenderer sprRndDude;
    //public SpriteRenderer sprRndGun;

    //Movement
    public float movementSpeed;
    private Vector2 inputVector;

    //Animation
    //private int curAnimIndex = 0;
    //private float animTimer = 0;
    //public int fps;
    //public Sprite[] spritesRunAnim;
    //public Sprite spriteIdle;

    private Vector2 mousePos;
    private Vector2 mouseWorldPos;
    private bool startThrow = false;

    //new
    Vector2 DragStartPos;
    Vector2 Vect;
    public Vector2 minPower;
    public Vector2 maxPower;
    public float power = 5f;
    
    Rigidbody2D rb;
    LineRenderer lr;
    public GameObject LandMarker;
    public Vector2 TempEndTrajectory;
    public Vector2 EndTrajectory;
    public Vector2 _velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Movement();
        //RotateGun();
        //Animate();
        //FlipSprites();
        if (GameObject.Find("GameManager").GetComponent<gamestart>().start_game == true)
        {
            Shoot();
        }
    }

    void Movement()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if (inputVector != Vector2.zero)
        {
            transform.position += (Vector3)inputVector.normalized * movementSpeed * Time.deltaTime;
        }
    }

    //void RotateGun()
    //{
    //    mousePos = Input.mousePosition;

    //    Vector3 objectPos = Camera.main.WorldToScreenPoint(trnsGun.position);
    //    mousePos.x = mousePos.x - objectPos.x;
    //    mousePos.y = mousePos.y - objectPos.y;

    //    float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
    //    trnsGun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    //    //trnsGun.rotation = transform.rotation;

    //    //i want arm object to be in the direction of vector of mousestartpos - mouseendpos
    //}

    void Shoot()
    {
        //if (GameObject.Find("GameManager").GetComponent<GameManager>().fourthdownBool == false)
        //{

            //if run play do literallly nothing
            if (!GameObject.FindWithTag("WRwithBall"))
            {
                if (!GameObject.Find("DB with Ball"))
                {
                    if (!GameObject.FindWithTag("ball"))
                    {

                        if (Input.GetMouseButtonDown(0))
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().runplay == false)
                            {
                                DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                startThrow = true;
                            }
                        }



                        if (startThrow == true)
                        {
                            if (Input.GetMouseButton(0))
                            {
                                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                //Vect = new Vector2(Mathf.Clamp(DragStartPos.x - DragEndPos.x, minPower.x, maxPower.x), Mathf.Clamp(DragStartPos.y - DragEndPos.y, minPower.y, maxPower.y));
                                Vect = new Vector2(DragStartPos.x - DragEndPos.x, DragStartPos.y - DragEndPos.y);
                                _velocity = Vect * power;

                                Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity);
                                lr.positionCount = trajectory.Length;
                                Vector3[] positions = new Vector3[trajectory.Length];
                                for (int i = 0; i < trajectory.Length; i++)
                                {
                                    positions[i] = trajectory[i];
                                }
                                lr.SetPositions(positions);
                                if (trajectory.Length > 1)
                                {
                                    TempEndTrajectory = positions[trajectory.Length - 1];
                                    //Debug.Log(TempEndTrajectory);
                                }
                            }

                            if (Input.GetMouseButtonUp(0))
                            {
                                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                                //Vect = new Vector2(Mathf.Abs(DragStartPos.x - DragEndPos.x), Mathf.Abs(DragStartPos.y - DragEndPos.y));
                                //Debug.Log((DragStartPos.x - DragEndPos.x, DragStartPos.y - DragEndPos.y));
                                //Debug.Log((Mathf.Clamp(DragStartPos.x - DragEndPos.x, minPower.x, maxPower.x), Mathf.Clamp(DragStartPos.y - DragEndPos.y, minPower.y, maxPower.y)));
                                Vect = new Vector2(DragStartPos.x - DragEndPos.x, DragStartPos.y - DragEndPos.y);
                                //Vect = new Vector2(Mathf.Clamp(DragStartPos.x - DragEndPos.x, minPower.x, maxPower.x), Mathf.Clamp(DragStartPos.y - DragEndPos.y, minPower.y, maxPower.y));
                                _velocity = Vect * power;
                                Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity);
                                if (trajectory.Length > 1)
                                {
                                    EndTrajectory = trajectory[trajectory.Length - 1];
                                    //Debug.Log(EndTrajectory);
                                }
                                lr.positionCount = 0; //kills trajectory line

                                GameObject insantiatedMelon = Instantiate(melon, trnsGunTip.position, Quaternion.identity);
                                //original
                                //insantiatedMelon.GetComponent<FakeHeightObject>().Initialize(trnsGun.right * Random.Range(groundDispenseVelocity.x, groundDispenseVelocity.y), Random.Range(verticalDispenseVelocity.x, verticalDispenseVelocity.y));
                                //new
                                //insantiatedMelon.GetComponent<FakeHeightObject>().Initialize(trnsGun.right * Random.Range(_velocity[0], _velocity[1]), Random.Range(verticalDispenseVelocity.x, verticalDispenseVelocity.y));
                                insantiatedMelon.GetComponent<FakeHeightObject>().Initialize(_velocity, _velocity.magnitude / 2);

                            }
                        }

                    }
                    else
                    {
                        startThrow = false;
                    }
                }
            }
        //}
    }

    //void Animate()
    //{
    //    if (inputVector != Vector2.zero)
    //    {

    //        if (animTimer > 1f / fps)
    //        {
    //            sprRndDude.sprite = spritesRunAnim[curAnimIndex];

    //            curAnimIndex++;

    //            if (curAnimIndex >= spritesRunAnim.Length)
    //                curAnimIndex = 0;

    //            animTimer = 0;
    //        }

    //        animTimer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        sprRndDude.sprite = spriteIdle;
    //        curAnimIndex = 0;
    //        animTimer = 0;
    //    }
    //}

    //void FlipSprites()
    //{

    //    mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    if (mouseWorldPos.x > transform.position.x)
    //    {
    //        sprRndDude.flipX = false;
    //        sprRndGun.flipY = false;
    //    }
    //    else
    //    {
    //        sprRndDude.flipX = true;
    //        sprRndGun.flipY = true;
    //    }
    //}

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity)
    {
        float verticalVelocity = _velocity.magnitude / 2;
        float totalTime = 2 * (verticalVelocity) / 10;
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        int steps = Mathf.RoundToInt(totalTime / timestep);
        
        Vector2 moveStep = _velocity * timestep;
        Vector2[] results = new Vector2[steps];

        for (int i = 0; i < steps; i++)
        {
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }
}
