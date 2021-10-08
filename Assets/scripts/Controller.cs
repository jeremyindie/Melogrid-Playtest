using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private LayerMask _boundary;


    [SerializeField]
    private float _movementSpeed;
    protected float _movementDistance;

    private float _lerpDistanceToNewPosition;
    private bool _isLerpingToNewPosition;

    protected bool _playerHasLost;
    protected bool _inUIScreen;

    protected bool _canMove;
    protected bool _isActive;

    protected bool _verticalKeyDown;
    protected bool _horizontalKeyDown;


    protected Vector3 _originalPosition;
    protected Vector3 _newPosition;

    protected List<string> _moveList;

    protected string _upKeyDirection;
    protected string _leftKeyDirection;
    protected string _rightKeyDirection;

    protected void Start()
    {
        _moveList = new List<string>();
        _movementDistance = Grid.Instance.GetGridDimention();
        _inUIScreen = false;
        _playerHasLost = false;
        _horizontalKeyDown = false;
        _verticalKeyDown = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_inUIScreen)
        {
            if (Input.GetKeyDown("space"))
            {
                ExitUIScreen();
                if (_playerHasLost)
                {
                    PlayerTurnManager.Instance.OnLossRestart();
                }
            }
        }
        else if (!_canMove && _isLerpingToNewPosition)
        {

            if (_lerpDistanceToNewPosition > 1)
            {
                _isLerpingToNewPosition = false;
                _canMove = true;
                _originalPosition = _newPosition;
                OnSuccessfulMove(_newPosition - _originalPosition);

                return;
            }
            _lerpDistanceToNewPosition += _movementSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(_originalPosition, _newPosition, _lerpDistanceToNewPosition);
        }


    }

    protected virtual bool MoveUp(bool isMovingForward = true)
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("U");
            return true;
        }
        return false;
    }


    protected virtual bool MoveDiagonallyLeftUp(bool isMovingForward = true)
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance - Vector3.right * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("L");
            return true;
        }
        return false; 
    }

    protected virtual bool MoveDiagonallyRightUp(bool isMovingForward = true)
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance + Vector3.right * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("R");
            return true;
        }
        return false;
    }

    public void EnterUIScreen()
    {
        _inUIScreen = true; 
    }

    protected void ExitUIScreen()
    {
        _inUIScreen = false;
        Debug.Log("ERE");
        NarrativeManager.Instance.TurnOffNarrativeScreen();

    }
    public virtual void StartTurn()
    {

    }
    protected bool StartMove()
    {
        if (!Physics2D.OverlapCircle(_newPosition, _movementDistance / 3, _boundary))
        {
            _isLerpingToNewPosition = true;
            _lerpDistanceToNewPosition = 0.0f;
            _canMove = false;

            return true;
        }
        return false;
    }

    protected virtual void OnSuccessfulMove(Vector2 moveDelta)
    {

    }

    protected void RandomizeDirections()
    {
        string[] dirs = PlayerTurnManager.Instance.GetRandomizedDirections();
        _upKeyDirection = dirs[0];
        _leftKeyDirection = dirs[1];
        _rightKeyDirection = dirs[2];
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    public float GetMovementSpeed()
    {
        return 1 / _movementSpeed;
    }
}
