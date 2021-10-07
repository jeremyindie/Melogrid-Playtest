using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyLady : Controller
{

    private enum MovingDirections { LEFT, LEFT_RETURN, UP, UP_RETURN, RIGHT, RIGHT_RETURN};
    private MovingDirections _currentMovementDirection;
    private bool _isMoving;

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
                MoveUp(true);
                _currentMovementDirection = MovingDirections.UP_RETURN;
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
                MoveDiagonallyRightUp(false);
                _currentMovementDirection = MovingDirections.UP;
                PlayerTurnManager.Instance.ChangeTurn();
                break;

        }
    }

    protected override void OnSuccessfulMove(Vector2 moveDelta)
    {
        
    }

    protected override bool MoveUp(bool isMovingForward = true)
    {
        int direction = -1;
        if (isMovingForward) direction = 1;

        _newPosition = _originalPosition + direction * Vector3.up * _movementDistance;
        if (StartMove())
        {
            AudioManager.Instance.Play("UpSound");
            return true;
        }
        return false;

    }

    protected override bool MoveDiagonallyLeftUp(bool isMovingForward = true)
    {
        int direction = -1;
        if (isMovingForward) direction = 1;
        _newPosition = _originalPosition + direction * Vector3.up * _movementDistance - direction * Vector3.right * _movementDistance;
        if (StartMove())
        {
            AudioManager.Instance.Play("LeftSound");
            return true;
        }
        return false;
    }

    protected override bool MoveDiagonallyRightUp(bool isMovingForward = true)
    {
        int direction = -1;
        if (isMovingForward) direction = 1;
        _newPosition = _originalPosition + direction * Vector3.up * _movementDistance + direction * Vector3.right * _movementDistance;
        if (StartMove())
        {
            AudioManager.Instance.Play("RightSound");
            return true;

        }
        return false;
    }

    public override void StartTurn()
    {
        RandomizeDirections();
        Grid.Instance.MoveTileArray(transform.position, new Vector3(0.0f, 0.0f));
        _currentMovementDirection = MovingDirections.UP;
        _canMove = true;
    }
}
