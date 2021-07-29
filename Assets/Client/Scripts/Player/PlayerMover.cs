using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerMoverInfo playerMoverInfo;

    private float speed;
    private float constAcceleration, acceleration;
    private Rigidbody _rb;

    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isFight = false;


    [SerializeField] private CheckForward checkForward;

    private AnimationState _animationState;
    private string lastDirection;

    private bool isDead = false;

    private void Awake()
    {
        speed = playerMoverInfo.speed * 1000;
        acceleration = playerMoverInfo.acceleration * 1000;
        constAcceleration = acceleration;
    }

    private void OnDisable()
    {
        isFight = false;
        isMoving = false;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animationState = gameObject.GetComponent<AnimationState>();

        constAcceleration = acceleration;
    }

    private void FixedUpdate()
    {
        if (isFight == false)
        {
            if (isMoving)
            {
                Runnig();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && isFight == false)
        {
            StopRun();
        }
    }

    public void StartRun(string direction)
    {
        switch (direction)
        {
            case "forward":
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case "-forward":
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case "right":
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case "-right":
                transform.localRotation = Quaternion.Euler(0, 270, 0);
                break;
        }

        lastDirection = direction;

        if(checkForward.IsForwardClearing())
        {
            if(_animationState == null)
            {
                _animationState = GetComponent<AnimationState>();
            }

            ////////////if (trail.gameObject.activeSelf == false)
            ////////////{
            ////////////    StartCoroutine(TimerTrail());
            ////////////}

            _animationState.OnRun();
            isFight = false;
            isMoving = true;
        }
    }

    private void Runnig()
    {
        _rb.AddForce(transform.forward * (speed + acceleration )* Time.fixedDeltaTime, ForceMode.Force);
        acceleration += constAcceleration / 2f;

        SetupRotation();
    }

    public void StopRun()
    {
        if (isDead == false)
        {
            _animationState.OnIdle();

            float newPos = Mathf.Abs(Mathf.Floor(transform.position.x) + 0.5f);

            if (transform.position.x < 0)
            {
                transform.position = new Vector3(-newPos, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
            }

            newPos = Mathf.Abs(Mathf.Floor(transform.position.z) + 0.5f);

            if (transform.position.z < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -newPos);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, newPos);
            }

            isMoving = false;

            acceleration = constAcceleration;
        }
    }

    public void Dead()
    {
        isDead = true;
        isMoving = false;
        isFight = false;
    }

    public void Fighting()
    {
        isFight = true;

        //StartCoroutine(TimerFighting());
    }

    public void EndFight()
    {
        isFight = false;
    }

    IEnumerator TimerFighting()
    {
        yield return new WaitForSeconds(1f);

        isFight = false;
    }

    public void ContinueRun()
    {
        if (checkForward.IsForwardClearing())
        {
            if (_animationState == null)
            {
                _animationState = GetComponent<AnimationState>();
            }

            _animationState.OnRun();

            isFight = false;
            isMoving = true;

            StartRun(lastDirection);
        }
    }

    public void SetupRotation()
    {
        switch (lastDirection)
        {
            case "forward":
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case "-forward":
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case "right":
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case "-right":
                transform.localRotation = Quaternion.Euler(0, 270, 0);
                break;
        }
    }
}
