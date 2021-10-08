using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : Controller
{
    private int _moveNumber; 
    private void Awake()
    {
        _canMove = true;
        _isActive = true;
        _moveNumber = 0;
        _originalPosition = transform.position;
    }
    private void Update()
    {
        base.Update();
        if (_isActive && _canMove && !_inUIScreen)
        {
            Move();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _newPosition = Vector3.zero;
        //bool moveWasSuccessful = false; 

        if (horizontal < 0 && !_horizontalKeyDown)
        {
            // moveWasSuccessful = MoveDiagonallyLeftUp();
            MoveDiagonallyLeftUp();
            _horizontalKeyDown = true;
        }
        else if (horizontal > 0 && !_horizontalKeyDown)
        {
            //moveWasSuccessful = MoveDiagonallyRightUp();
            MoveDiagonallyRightUp();
            _horizontalKeyDown = true;

        }
        else if (vertical > 0 && !_verticalKeyDown)
        {
            //moveWasSuccessful = MoveUp();
            MoveUp();
            _verticalKeyDown = true;

        }
       
        if (horizontal == 0)
        {
            _horizontalKeyDown = false;
        }
        if (vertical == 0)
        {
            _verticalKeyDown = false;
        }
        //if (moveWasSuccessful) IncrementMove();
    }

    protected override void OnSuccessfulMove(Vector2 moveDelta)
    {
        IncrementMove();
    }

    protected override bool MoveUp(bool isMovingForward = true)
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("U");
            AudioManager.Instance.Play("UpSound");
            return true; 
        }
        return false;

    }

    protected override bool MoveDiagonallyLeftUp(bool isMovingForward = true)
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance - Vector3.right * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("L");
            AudioManager.Instance.Play("LeftSound");
            return true;
        }
        return false; 
    }

    protected override bool  MoveDiagonallyRightUp(bool isMovingForward = true)
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance + Vector3.right * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("R");
            AudioManager.Instance.Play("RightSound");
            return true; 

        }
        return false; 
    }

    public List<string> GetMoveList()
    {
        return _moveList;
    }

    public void IncrementMove()
    {
        _moveNumber++;
        if(_moveNumber >= 4)
        {
            PlayerTurnManager.Instance.ChangeTurn();
        }
    }
    public override void StartTurn()
    {
        _moveNumber = 0;
        _canMove = true;
    }


}
