using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyLady : Controller
{

    private enum MovingDirections { LEFT, LEFT_RETURN, UP, UP_RETURN, RIGHT, RIGHT_RETURN};
    private MovingDirections _currentMovementDirection;
    private bool _isMoving;
    private SpriteRenderer _spriteRenderer; 
    private void Awake()
    {
        _originalPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
        if (_isActive && _canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        switch (_currentMovementDirection)
        {
            case MovingDirections.UP:
                _currentMovementDirection = MovingDirections.UP_RETURN;
                MoveUp(true);
                break;
            case MovingDirections.UP_RETURN:
                MoveUp(false);
                _currentMovementDirection = MovingDirections.LEFT;
                break;
            case MovingDirections.LEFT:
                MoveDiagonallyLeftUp(true);
                _currentMovementDirection = MovingDirections.LEFT_RETURN;
                break;
            case MovingDirections.LEFT_RETURN:
                MoveDiagonallyLeftUp(false);
                _currentMovementDirection = MovingDirections.RIGHT;
                break;
            case MovingDirections.RIGHT:
                MoveDiagonallyRightUp(true);
                _currentMovementDirection = MovingDirections.RIGHT_RETURN;

                break;
            case MovingDirections.RIGHT_RETURN:
                _currentMovementDirection = MovingDirections.UP;
                MoveDiagonallyRightUp(false);
                break;

        }
    }

    protected override void OnSuccessfulMove(Vector2 moveDelta)
    {
        if (_currentMovementDirection == MovingDirections.UP)
        {
            _spriteRenderer.enabled = false;
            PlayerTurnManager.Instance.ChangeTurn();
        }


    }
    private string GetSoundName(string direction)
    {
        switch (direction) {

            case "U": return "UpSound";
            case "L": return "LeftSound";
            case "R": return "RightSound";
        }
        return "";
    }

    protected override bool MoveUp(bool isMovingForward = true)
    {
        int direction = 1;
        if (isMovingForward) direction = -1;

        _newPosition = _originalPosition + direction * Vector3.up * _movementDistance;
        if (StartMove())
        {
            if (isMovingForward)
            {
                AudioManager.Instance.Play(GetSoundName(_upKeyDirection));
                Debug.Log("Up == " + _upKeyDirection);
                UIManager.Instance.SetUIText("Up Sound");
            }
            return true;
        }
        return false;

    }

    protected override bool MoveDiagonallyLeftUp(bool isMovingForward = true)
    {
        int direction = 1;
        if (isMovingForward) direction = -1;
        _newPosition = _originalPosition + direction * Vector3.up * _movementDistance - direction * Vector3.right * _movementDistance;
        if (StartMove())
        {
            if (isMovingForward)
            {
                AudioManager.Instance.Play(GetSoundName(_leftKeyDirection));
                Debug.Log("Left == " + _leftKeyDirection);
                UIManager.Instance.SetUIText("Left Sound");
            }
            return true;
        }
        return false;
    }

    protected override bool MoveDiagonallyRightUp(bool isMovingForward = true)
    {
        int direction = 1;
        if (isMovingForward) direction = -1;
        _newPosition = _originalPosition + direction * Vector3.up * _movementDistance + direction * Vector3.right * _movementDistance;
        if (StartMove())
        {
            if (isMovingForward)
            {
                AudioManager.Instance.Play(GetSoundName(_rightKeyDirection));
                Debug.Log("Right == " + _rightKeyDirection);
                UIManager.Instance.SetUIText("Right Sound");
            }
            return true;

        }
        return false;
    }

    public void SetPosition(Vector3 position)
    {
        _originalPosition = position;
        transform.position = position;
    }
    public override void StartTurn()
    {
        RandomizeDirections();
        Grid.Instance.MoveTileArray(transform.position, new Vector3(0.0f, 0.0f));
        _currentMovementDirection = MovingDirections.UP;
        _canMove = true;
        _spriteRenderer.enabled = true;
    }
}
