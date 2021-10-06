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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _newPosition = Vector3.zero;
        //bool moveWasSuccessful = false; 

        if (horizontal < 0)
        {
            // moveWasSuccessful = MoveDiagonallyLeftUp();
            MoveDiagonallyLeftUp();
        }
        else if (horizontal > 0)
        {
            //moveWasSuccessful = MoveDiagonallyRightUp();
            MoveDiagonallyRightUp();
        }
        else if (vertical > 0)
        {
            //moveWasSuccessful = MoveUp();
            MoveUp();
        }
        //if (moveWasSuccessful) IncrementMove();
    }

    protected override void OnSuccessfulMove(Vector2 moveDelta)
    {
        IncrementMove();
    }

    protected override bool MoveUp()
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

    protected override bool MoveDiagonallyLeftUp()
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

    protected override bool  MoveDiagonallyRightUp()
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
