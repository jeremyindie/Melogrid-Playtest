using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : Controller
{
   

    //private Directions _downKeyDirection;




    //Trackers for right and wrong moves
    [SerializeField]
    private int _numberOfWrongNotesAllowed = 4;
    private int _numberOfCorrectMovesNeeded;
    private int _correctMoves;
    private int _wrongMoves;

    //UI
    [SerializeField]
    private List<SpriteRenderer> _uiForCheckingTheMoves;
    [SerializeField]
    private Sprite _uiNotCompleted;
    [SerializeField]
    private Sprite _uiSuccess;
    [SerializeField]
    private bool _enableUICircles = false;

    [SerializeField]
    private SpriteRenderer _benchSpriteRenderer; 

    private void Awake()
    {
        _isActive = false;
        _numberOfCorrectMovesNeeded = 0;
        _doesCharacterMoveWithPositiveY = false;
        

    }

    public void Start()
    {
        Grid.Instance.CreateTileArray(transform.position);
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (!_isActive || _inUIScreen) return;

   
        if (_canMove)
        {
            if (Input.GetKeyDown("space"))
            {
                PlayMelody();
            }
        }

    }



    protected override void OnMoveStart(Directions direction)
    {
        Directions adjustedDirection = GetAdjustedDirection(direction);

        if (PlayerTurnManager.Instance.GetPathlist()[_correctMoves] == adjustedDirection) 
        {

            OnCorrectMove(adjustedDirection);
        }
        else
        {
            OnWrongMove();

        }
    }

    protected override void OnMoveEnd(Vector2 moveDelta)
    {
        Grid.Instance.MoveTileArray(transform.position, moveDelta);
        _benchSpriteRenderer.transform.position = transform.position + transform.up * _movementDistance *(_numberOfCorrectMovesNeeded - _correctMoves);

        if (_correctMoves >= _numberOfCorrectMovesNeeded)
        {
            OnTurnEnd();
        }
        else if (_wrongMoves >= _numberOfWrongNotesAllowed)
        {
            OnPlayerLost();
        }
    }
    public override void StartTurn()
    {
        _correctMoves = 0;
        _wrongMoves = 0;
        _numberOfCorrectMovesNeeded = PlayerTurnManager.Instance.GetPathlist().Count;
        RandomizeDirections();
        CreateMelody(PlayerTurnManager.Instance.GetPathlist());
        PlayMelody();


        if (_enableUICircles)
        {
            EnableUI();
        } else
        {
            DisableUI();
        }
        _benchSpriteRenderer.transform.position = transform.position + transform.up * _movementDistance * _numberOfCorrectMovesNeeded;
        UIManager.Instance.SetUIText("Listen to the Melody");
    }
    protected override void OnTurnEnd()
    {
        DisableUI();
        UIManager.Instance.EraseUIText();

        if(_narrativeElementReady)
        {
            PlayerTurnManager.Instance.StartNarrativeScreen();
            EnterUIScreen();
        }
        else
        {
            PlayerTurnManager.Instance.StartClockBackward();
        }
        //       PlayerTurnManager.Instance.ChangeTurn();
    }

   
    private void OnWrongMove()
    {
        _wrongMoves++;
        Grid.Instance.DarkenSprites(_wrongMoves);
        AudioManager.Instance.Play("WrongSound");
    }

    private void OnCorrectMove(Directions direction)
    {
        if (_enableUICircles)
        {
            UpdateUI();
        }
        _correctMoves++;

        string clipName = AudioManager.Instance.GetClipNameFromDirection(direction);
        AudioManager.Instance.Play(clipName);
    }
    private void OnPlayerLost()
    {
        DisableUI();

        UIManager.Instance.EraseUIText();
        _playerHasLost = true;
        EnterUIScreen();
        PlayerTurnManager.Instance.ShowLossScreen();
    }

    protected override void EndMelody()
    {
        base.EndMelody();
        UIManager.Instance.SetUIText(_numberOfCorrectMovesNeeded + " correct moves left");

        if (PlayerTurnManager.Instance.GetIsFirstTurn())
        {
            UIManager.Instance.SetInputText("Press Space to Listen to Melody Again");

        }
    }
    private void DisableUI()
    {
        for (int i = 0; i < _uiForCheckingTheMoves.Count; i++)
        {
            _uiForCheckingTheMoves[i].enabled = false;
        }
        _benchSpriteRenderer.enabled = false; 
    }
    private void EnableUI()
    {
        for (int i = 0; i < _uiForCheckingTheMoves.Count; i++)
        {
            _uiForCheckingTheMoves[i].enabled = true;
            _uiForCheckingTheMoves[i].sprite = _uiNotCompleted;
        }
        _benchSpriteRenderer.enabled = true;

    }

    private void UpdateUI()
    {
        _uiForCheckingTheMoves[_correctMoves].sprite = _uiSuccess;
    }



    protected override void OnNarrativeEnd()
    {
        PlayerTurnManager.Instance.StartClockBackward();
    }






}


