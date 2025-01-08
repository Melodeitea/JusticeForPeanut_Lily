using UnityEngine;

public class LanceBallon : MonoBehaviour
{
    private GameObject Ball;

    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos, endPos;


    public float MinSwipDist = 0;
    private float BallVelocity = 0;
    public float BallSpeed = 0;
    public float MaxBallSpeed = 40;
    private Vector3 angle;

    private bool thrown, holding;
    private Vector3 newPosition;


    public float smooth = 0.7f;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        setupBall();
    }

    void setupBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        Ball = ball;
        rb = Ball.GetComponent<Rigidbody>();
        ResetBall();
    }

    //Remise � 0 des variables
    void ResetBall() 
    {
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        BallSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0; 
        thrown = holding = false; 

        //Reinitialise le rigidbody
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        Ball.transform.position = transform.position;
    }

    void PickupBall()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane * 5f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        newPosition.z += 5;

        //Bouge le ballon
        //Ball.transform.position = Vector3.Lerp(Ball.transform.position, newPosition, 80f * Time.deltaTime);
        Ball.transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //V�rifie les �tats si le joueur a le ballon ou pas
        if(holding)
        {
            PickupBall();
        }
        if(thrown)
        {
            return;
        }

        //Quand le joueur clique sur le ballon
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if(hit.transform == Ball.transform)  //Si le raycast touche le ballon, le joueur tient le ballon
                {
                    startTime = Time.time;
                    startPos = Input.mousePosition;
                    holding = true;
                }
            }
        }//Quand le joueur relache le clique
        else if(Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            endPos = Input.mousePosition;
            swipeDistance = (endPos - startPos).magnitude;
            swipeTime = endTime - startTime;

            if (swipeDistance < 0.5f && swipeDistance > 30f)
            {
                //Lancer la balle
                CalculateSpeed();
                CalculateAngle();
                rb.AddForce(new Vector3((angle.x * BallSpeed), (angle.y * BallSpeed), (angle.z * BallSpeed)));
                rb.useGravity = true;
                holding = false;
                thrown = true;
                Invoke("ResetBall", 4f); //Pas le temps de faire une coroutine
            }
            else
            {
                ResetBall();
            }
        }

    }

    void CalculateSpeed ()
    {
        if (swipeTime > 0)
        {
            BallVelocity = swipeDistance / (swipeDistance - swipeTime);
        }

        BallSpeed = BallVelocity * 40;

        if (BallSpeed >= MaxBallSpeed)
        {
            BallSpeed = MaxBallSpeed;  
        }

        if (BallSpeed <= MaxBallSpeed)
        {
            BallSpeed = BallSpeed += 40;
        }
        swipeTime = 0;

    }

    void CalculateAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, (Camera.main.nearClipPlane + 5)));
    }

}


