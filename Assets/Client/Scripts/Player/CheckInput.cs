using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckInput : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _direction;
    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.UpArrow))
        {
            DevMover("forward");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            DevMover("-forward");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            DevMover("right");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            DevMover("-right");
        }
#endif

        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPos = touch.position;
                    break;

                case TouchPhase.Stationary:
                    _startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    _direction = touch.position - _startPos;
                    Mover(_direction.x, _direction.y);
                    break;
            }
        }
    }

    private void Mover(float x, float y)
    {
        if (Mathf.Abs(y) + Mathf.Abs(x) <= 10f)
        {
            return;
        }

        string direct = "";

        if ((Mathf.Abs(y) > Mathf.Abs(x) && y > 0 && x > 0)
            || (Mathf.Abs(y) > Mathf.Abs(x) && y > 0 && x < 0))
        {
            // up
            direct = "forward";
        }
        else if ((Mathf.Abs(y) > Mathf.Abs(x) && y < 0 && x > 0)
            || (Mathf.Abs(y) > Mathf.Abs(x) && y < 0 && x < 0))
        {
            //down
            direct = "-forward";
        }

        else if ((Mathf.Abs(x) > Mathf.Abs(y) && y > 0 && x > 0)
            || (Mathf.Abs(x) > Mathf.Abs(y) && y < 0 && x > 0))
        {
            //right
            direct = "right";
        }
        else if ((Mathf.Abs(x) > Mathf.Abs(y) && y > 0 && x < 0)
            || (Mathf.Abs(x) > Mathf.Abs(y) && y < 0 && x < 0))
        {
            //left
            direct = "-right";
        }

        if (direct != "" && _playerMover.isMoving == false)
        {
            _playerMover.StartRun(direct);
        }
    }

    private void DevMover(string direct)
    {
        if (_playerMover.isMoving == false)
        {
            _playerMover.StartRun(direct);
        }
    }

    public void EndGame()
    {
        _playerMover.StopRun();

        //AnimationState animationState = gameObject.GetComponent<AnimationState>();
        //animationState.OnDance();

        GetComponent<CheckInput>().enabled = false;
    }
}
