using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 speedVector = Vector3.forward;
    [SerializeField] private Vector3 targetPosition;

    public float carSpeed = 10; 
    public float turnLaneSpeed;
    public float laneDistance;
    private float speedModifire;
    private int angleModifire;

    public delegate void Triggered();
    public event Triggered TriggerEnter;

    public Lane currentLane = Lane.Right;
    public Lane turn = Lane.NotActions;

    private Vector3 vectorRight = Vector3.right;
    private Vector3 vectorLeft = Vector3.left;

    public Animator animator;
    public Animation carTurnings;
    public AudioSource turnAudio;


    private void Start()
    {
        animator = GetComponent<Animator>();
        carTurnings = GetComponent<Animation>();
        turnAudio = GetComponent<AudioSource>();
        angleModifire = (int)transform.eulerAngles.y;
    }

    void Update()
    {
        transform.localPosition += speedVector * (carSpeed + speedModifire) * Time.deltaTime;
        carSpeed += 0.3f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
       {

            if (Input.GetKeyDown(KeyCode.D) && currentLane != Lane.Right)
                MoveRight();

            if (Input.GetKeyDown(KeyCode.A) && currentLane != Lane.Left)
                MoveLeft();
       }

       if (Input.GetKeyDown(KeyCode.S))
        speedModifire = -0.3f * carSpeed;

       if (Input.GetKeyUp(KeyCode.S))
        speedModifire = 0;

        if (Input.GetKeyDown(KeyCode.W))
            speedModifire = 0.4f * carSpeed;

        if (Input.GetKeyUp(KeyCode.W))
            speedModifire = 0;

        if (turn == Lane.TurnLaneRight || turn == Lane.TurnLaneLeft)
        {
            if (turn == Lane.TurnLaneRight)
            {
                if (speedVector == Vector3.forward || speedVector == Vector3.back)
                    targetPosition = new Vector3(targetPosition.x, transform.localPosition.y, transform.localPosition.z);
                if (speedVector == Vector3.right || speedVector == Vector3.left)
                    targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, targetPosition.z);

                transform.position = Vector3.MoveTowards(transform.localPosition, targetPosition, turnLaneSpeed * Time.deltaTime);
            }


            else if (turn == Lane.TurnLaneLeft)
            {
                if (speedVector == Vector3.forward || speedVector == Vector3.back)
                    targetPosition = new Vector3(targetPosition.x, transform.localPosition.y, transform.localPosition.z);
                if (speedVector == Vector3.right || speedVector == Vector3.left)
                    targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, targetPosition.z);

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, turnLaneSpeed * Time.deltaTime);
            }

            if (speedVector == Vector3.forward || speedVector == Vector3.back)
                if (transform.position.x == targetPosition.x)
                    turn = Lane.NotActions;

            if (speedVector == Vector3.right || speedVector == Vector3.left)
                if (transform.position.z == targetPosition.z)
                    turn = Lane.NotActions;
        }

        if (turn == Lane.TurnRight || turn == Lane.TurningRight)
        {
            if (turn == Lane.TurningRight)
                if (!carTurnings.IsPlaying($"TurnRight{angleModifire}"))
                {
                    angleModifire += 90;

                    if (angleModifire == 360)
                        angleModifire = 0;

                    turn = Lane.NotActions;
                }

            if (turn == Lane.TurnRight)
            {
                TurnRight();
                turn = Lane.TurningRight;
            }
        }

        if (turn == Lane.TurnLeft || turn == Lane.TurningLeft)
        {
            if (turn == Lane.TurningLeft)
                if (!carTurnings.IsPlaying($"TurnLeft{angleModifire}"))
                {
                    angleModifire -= 90;

                    if (angleModifire == -90)
                        angleModifire = 270;

                    turn = Lane.NotActions;
                }

            if (turn == Lane.TurnLeft)
            {
                TurnLeft();
                turn = Lane.TurningLeft;
            }
        }
    }

    void MoveRight()
    {
        turnAudio.Play();
        targetPosition = transform.position + vectorRight * laneDistance;
        currentLane = Lane.Right;
        turn = Lane.TurnLaneRight;
    }

    void MoveLeft()
    {
        turnAudio.Play();
        targetPosition = transform.position + vectorLeft * laneDistance;
        currentLane = Lane.Left;
        turn = Lane.TurnLaneLeft;
    }

    void TurnRight()
    {
        turnAudio.Play();
        carTurnings.PlayQueued($"TurnRight{angleModifire}");
        SpeedVectorController();
    }

    void TurnLeft()
    {
        turnAudio.Play();
        carTurnings.PlayQueued($"TurnLeft{angleModifire}");
        SpeedVectorController();
    }

    void SpeedVectorController()
    {
        if (turn == Lane.TurnRight)
        {
            if (speedVector == Vector3.forward)
            {
                speedVector = Vector3.right;
                vectorRight = Vector3.back;
                vectorLeft = Vector3.forward;
                return;
            }
            if (speedVector == Vector3.right)
            {
                speedVector = Vector3.back;
                vectorRight = Vector3.left;
                vectorLeft = Vector3.right;
                return;
            }
            if (speedVector == Vector3.back)
            {
                speedVector = Vector3.left;
                vectorRight = Vector3.forward;
                vectorLeft = Vector3.back;
                return;
            }
            if (speedVector == Vector3.left)
            {
                speedVector = Vector3.forward;
                vectorRight = Vector3.right;
                vectorLeft = Vector3.left;
                return;
            }
        }
        if (turn == Lane.TurnLeft)
        {
            if (speedVector == Vector3.forward)
            {
                speedVector = Vector3.left;
                vectorRight = Vector3.forward;
                vectorLeft = Vector3.back;
                return;
            }
            if (speedVector == Vector3.left)
            {
                speedVector = Vector3.back;
                vectorRight = Vector3.left;
                vectorLeft = Vector3.right;
                return;
            }
            if (speedVector == Vector3.back)
            {
                speedVector = Vector3.right;
                vectorRight = Vector3.back;
                vectorLeft = Vector3.forward;
                return;
            }
            if (speedVector == Vector3.right)
            {
                speedVector = Vector3.forward;
                vectorRight = Vector3.right;
                vectorLeft = Vector3.left;
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NewTile")
            TriggerEnter();

        if (other.tag == "TurnRight" && turn == Lane.NotActions)
            turn = Lane.TurnRight;

        if (other.tag == "TurnLeft" && turn == Lane.NotActions)
            turn = Lane.TurnLeft;
    }
}
