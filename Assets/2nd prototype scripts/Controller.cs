using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private LayerMask _boundary;

    protected bool _inUIScreen; 
    
    protected bool _canMove;
    protected bool _isActive; 
    private bool _isLerpingToNewPosition;

    protected float _movementDistance;
    [SerializeField]
    private float _movementSpeed;
    private float _lerpDistanceToNewPosition;

    protected Vector3 _originalPosition;
    protected Vector3 _newPosition;

    protected List<string> _moveList; 

    protected void Start()
    {
        _moveList = new List<string>();
        _movementDistance = Grid.Instance.GetGridDimention();
        _inUIScreen = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_inUIScreen)
        {
            if (Input.GetKeyDown("space"))
            {
                ExitUIScreen();
            }
        }
        else if (!_canMove && _isLerpingToNewPosition)
        {

            if (_lerpDistanceToNewPosition > 1)
            {
                _isLerpingToNewPosition = false;
                _canMove = true;
                OnSuccessfulMove(_newPosition - _originalPosition);
                _originalPosition = _newPosition;
                return;
            }
            _lerpDistanceToNewPosition += _movementSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(_originalPosition, _newPosition, _lerpDistanceToNewPosition);
        }


    }

    protected virtual bool MoveUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("U");
            return true;
        }
        return false;
    }


    protected virtual bool MoveDiagonallyLeftUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance - Vector3.right * _movementDistance;
        if (StartMove())
        {
            _moveList.Add("L");
            return true;
        }
        return false; 
    }

    protected virtual bool MoveDiagonallyRightUp()
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
        if (_newPosition != Vector3.zero && !Physics2D.OverlapCircle(_newPosition, _movementDistance / 2, _boundary))
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


    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }
}
