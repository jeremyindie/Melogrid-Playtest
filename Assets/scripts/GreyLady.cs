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

    private void Awake()
    {
        _doesCharacterMoveWithPositiveY = false;
    }

    public void Start()
    {
        base.Start();
        _greyMelody = new List<Directions>()
        {
            Directions.LEFT,
            Directions.UP,
            Directions.RIGHT,
        };
    }

    private void Update()
    {
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
        Debug.Log(DirectionToString(direction) + " == " + _rightKeyDirection);
        UIManager.Instance.SetUIText(DirectionToString(direction) + " Sound");
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

    public override void StartTurn()
    {
        RandomizeDirections();
        Grid.Instance.MoveTileArray(transform.position, new Vector3(0.0f, 0.0f));
        _currentAbsoluteDirection = AbsoluteDirections.LEFT;
        _currentRelativeDirection = RelativeDirections.FORWARD;
        _canMove = true;

        if (_playNotesFirst)
        {
            CreateMelody(_greyMelody);
            PlayMelody(); //sets _canMove to false

        } 
        _spriteRenderer.enabled = true;
    }

    protected override void OnTurnEnd()
    {
        _spriteRenderer.enabled = false;
        PlayerTurnManager.Instance.ChangeTurn();

    }
}
