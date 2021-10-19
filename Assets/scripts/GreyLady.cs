using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyLady : Controller
{
    [SerializeField]
    private bool _playNotesFirst = true; 
    private enum AbsoluteDirections { LEFT, UP, RIGHT, NONE};
    private enum RelativeDirections { FORWARD, BACK };

    private AbsoluteDirections _currentAbsoluteDirection;
    private RelativeDirections _currentRelativeDirection;
    private List<Directions> _greyMelody;
    private bool _waitingForPlayerToPushGo; 

    private void Awake()
    {
        _doesCharacterMoveWithPositiveY = false;
    }

    private void Start()
    {
        base.Start();
        _greyMelody = new List<Directions>()
        {
            Directions.LEFT,
            Directions.UP,
            Directions.RIGHT,
        };
        _waitingForPlayerToPushGo = false;
    }

    private void Update()
    {
        if (_waitingForPlayerToPushGo)
        {
            if (Input.GetKeyDown("space"))
            {
                _waitingForPlayerToPushGo = false;
                UIManager.Instance.SetInputText("");
                PlayMelody(); //sets _canMove to false


            }
            return;
        }
        base.Update();
    }

    protected override void Move()
    {
        bool flipTheMovement = (_currentRelativeDirection != RelativeDirections.FORWARD);

        switch (_currentAbsoluteDirection)
        {
            case AbsoluteDirections.UP:
                if (_currentRelativeDirection == RelativeDirections.FORWARD)
                {
                    _currentRelativeDirection = RelativeDirections.BACK;
                } else
                {
                    _currentRelativeDirection = RelativeDirections.FORWARD;
                    _currentAbsoluteDirection = AbsoluteDirections.RIGHT;
                }
                MoveUp(flipTheMovement);
                break;

            case AbsoluteDirections.LEFT:
                if (_currentRelativeDirection == RelativeDirections.FORWARD)
                {
                    _currentRelativeDirection = RelativeDirections.BACK;
                }
                else
                {
                    _currentRelativeDirection = RelativeDirections.FORWARD;
                    _currentAbsoluteDirection = AbsoluteDirections.UP;
                }
                MoveDiagonallyLeftUp(flipTheMovement);
                break;

            case AbsoluteDirections.RIGHT:
                if (_currentRelativeDirection == RelativeDirections.FORWARD)
                {
                    _currentRelativeDirection = RelativeDirections.BACK;
                }
                else
                {
                    _currentRelativeDirection = RelativeDirections.FORWARD;
                    _currentAbsoluteDirection = AbsoluteDirections.NONE;
                }
                MoveDiagonallyRightUp(flipTheMovement);
                break;


        }
    }


    protected override void OnMoveStart(Directions direction)
    {
        //Debug.Log(DirectionToString(direction) + " == " + GetAdjustedDirection(direction));
        //UIManager.Instance.SetUIText(DirectionToString(direction) + " Sound");
        UIManager.Instance.SetUIText(DirectionToString(direction));
        if (!_playNotesFirst)
        {
            string clipName = AudioManager.Instance.GetClipNameFromDirection(direction);
            AudioManager.Instance.Play(clipName);
        }
    }
    protected override void OnMoveEnd(Vector2 moveDelta)
    {
        if (_currentAbsoluteDirection == AbsoluteDirections.NONE)
        {
            OnTurnEnd();
        }
    }

    public void RefreshTiles()
    {
        Grid.Instance.SetPositionsAtStartOfTurn(transform.position);
       // Grid.Instance.MoveTileArray(transform.position, new Vector3(0.0f, 0.0f));

    }

    public override void StartTurn()
    {
        RandomizeDirections();
        _currentAbsoluteDirection = AbsoluteDirections.LEFT;
        _currentRelativeDirection = RelativeDirections.FORWARD;
        _canMove = true;
        _waitingForPlayerToPushGo = true;
        UIManager.Instance.SetInputText("Press Space When Ready");

        if (_playNotesFirst)
        {

            List<Directions> adjustedGreyMelody = new List<Directions>();
            adjustedGreyMelody.Add(GetAdjustedDirection(_greyMelody[0]));
            adjustedGreyMelody.Add(GetAdjustedDirection(_greyMelody[1]));
            adjustedGreyMelody.Add(GetAdjustedDirection(_greyMelody[2]));
            CreateMelody(adjustedGreyMelody);

        } 
        _spriteRenderer.enabled = true;
    }

    protected override void OnTurnEnd()
    {
        _spriteRenderer.enabled = false;
        PlayerTurnManager.Instance.ChangeTurn();

    }
}
