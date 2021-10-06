using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : Controller
{
    private enum Directions
    {
        UP,
        //DOWN,
        LEFT,
        RIGHT
    };

    private string _upKeyDirection;
    private string _leftKeyDirection;
    private string _rightKeyDirection;
    //private Directions _downKeyDirection;

    private int _correctMoves;
    private int _wrongMoves;

    private bool _isPlayingMelodyNote;
    private bool _isPlayingMelody;
    private int _melodyNote;
    private float _timeInMelodyNote;

    private void Awake()
    {
        _correctMoves = 0;
        _wrongMoves = 0;
        _isPlayingMelodyNote = false;
        _isPlayingMelody = false;
        _canMove = false;
        _isActive = false;
        _originalPosition = transform.position;

    }

    private void Start()
    {
        Grid.Instance.CreateTileArray(transform.position);
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (_isActive && !_inUIScreen)
        {
            if (_isPlayingMelody && !_isPlayingMelodyNote)
            {
                _isPlayingMelodyNote = true;
                StartCoroutine(PlayMelodyNote(_melodyNote));
            } else if (_canMove)
            {
                Move();
            }
            if (_correctMoves >= 4)
            {
                PlayerTurnManager.Instance.ChangeTurn();
            }
            else if (_wrongMoves >= 4)
            {
                EnterUIScreen();
                //temporary 
                _wrongMoves = 0; 
                NarrativeManager.Instance.DisplayCustomScreen("You Done Lost Your Good Thing Now - BB King");
             //gingerbread House!
            }
        }
       




    }
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _newPosition = Vector3.zero;
        
        if (horizontal < 0)
        {
            MoveInDirection(Directions.LEFT);
        }
        else if (horizontal > 0)
        {
            MoveInDirection(Directions.RIGHT);
        }
        else if (vertical > 0)
        {
            MoveInDirection(Directions.UP);
        }
    }

    protected override void OnSuccessfulMove(Vector2 moveDelta)
    {
        Grid.Instance.MoveTileArray(transform.position, moveDelta);
    }
    private void MoveInDirection(Directions direction)
    {
        switch (direction)
        {
            case Directions.UP:
                MoveUp();
                break;
            case Directions.LEFT:
                MoveDiagonallyLeftUp();
                break;
            case Directions.RIGHT:
                MoveDiagonallyRightUp();
                break;

        }
    }

    private string GetAdjustedDirection(string direction)
    {
        switch (direction)
        {
            case "U":
                return _upKeyDirection;
            case "L":
                return _leftKeyDirection;
            case "R":
                return _rightKeyDirection;
 
        }
        return _upKeyDirection;
    }
    protected override bool MoveUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance;
        if (StartMove())
        {
            checkMove("U");
            return true;
        }
        return false;

    }

    protected override bool MoveDiagonallyLeftUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance - Vector3.right * _movementDistance;
        if (StartMove())
        {
            checkMove("L");
            return true;
        }
        return false;
    }

    protected override bool MoveDiagonallyRightUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance + Vector3.right * _movementDistance;
        if (StartMove())
        {
            checkMove("R");
            return true;
        }
        return false;
    }



    private void checkMove(string move)
    {
        if (GetAdjustedDirection(PlayerTurnManager.Instance.pathList[_correctMoves]) == move)
        {
            _correctMoves++;
            _moveList.Add(move);

        } else
        {
            //fade 
            Grid.Instance.DarkenSprites(_wrongMoves);
            _wrongMoves++;
            AudioManager.Instance.Play("WrongSound");
        }
    }

    IEnumerator PlayMelodyNote(int indexOfDirection)
    {
        string direction = GetAdjustedDirection(PlayerTurnManager.Instance.pathList[indexOfDirection]);
        string clipName = "";

        switch (direction)
        {
            case "U":
                clipName = "UpSound";
                break;
            case "L":
                clipName = "LeftSound";
                break;
            case "R":
                clipName = "RightSound";
                break;
        }
        AudioManager.Instance.Play(clipName);

        yield return new WaitForSeconds(AudioManager.Instance.GetClipLength(clipName));
        _isPlayingMelodyNote = false;
        if (_melodyNote + 1 == PlayerTurnManager.Instance.pathList.Count)
        {
            _isPlayingMelody = false;
            _canMove = true;
        }
        _melodyNote++;

    }


    private void RandomizeDirections()
    {
        string [] dirs = { "U", "L", "R" };
        for (int i = 0; i < 3; i++)
        {
            string temp = dirs[i];
            int randomLoc = Mathf.FloorToInt(Random.Range(0, 3));
            dirs[i] = dirs[randomLoc];
            dirs[randomLoc] = temp;
        }
        _upKeyDirection = dirs[0];
        _leftKeyDirection = dirs[1];
        _rightKeyDirection = dirs[2];

        if (_upKeyDirection == "U" && _leftKeyDirection == "L" && _rightKeyDirection == "R") RandomizeDirections();

    }
    public override void StartTurn()
    {
        _correctMoves = 0;
        _wrongMoves = 0;
        _isPlayingMelody = true;
        _canMove = false;
        _isPlayingMelodyNote = false;
        //always assuming swaping after 4 moves for prototype
        _melodyNote = 0;
        RandomizeDirections();
        Grid.Instance.MoveTileArray(transform.position, new Vector3(0.0f, 0.0f));
    }


}


