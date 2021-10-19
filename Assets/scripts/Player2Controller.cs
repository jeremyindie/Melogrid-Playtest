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
    private Sprite _uiFail;
    [SerializeField]
    private bool _enableUICircles = false;
    [SerializeField]
    private GameObject _testVideo; 
    [SerializeField]
    private Bench _bench;

    private void Awake()
    {
        _isActive = false;
        _numberOfCorrectMovesNeeded = 0;
        _doesCharacterMoveWithPositiveY = false;
        _audio = GetComponent<AudioSource>();

    }

    public void Start()
    {
        
        Grid.Instance.CreateTileArray(transform.position);
        base.Start();
        _audio.Play();
        _audio.Pause();
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



    protected override void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _newPosition = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveDiagonallyLeftUp();

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveDiagonallyRightUp();

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
        _bench.transform.position = transform.position + transform.up * _movementDistance * (_numberOfCorrectMovesNeeded - _correctMoves) + new Vector3(0.25f, 0.5f) ;

        if (_correctMoves >= _numberOfCorrectMovesNeeded)
        {
            OnTurnEnd();
        }
        else if (_wrongMoves >= _numberOfWrongNotesAllowed)
        {
            OnPlayerLost();
        }
    }

    public void StartAudio()
    {
        StartCoroutine(FadeInAudio());

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
        _bench.transform.position = transform.position + transform.up * _movementDistance * (_numberOfCorrectMovesNeeded - _correctMoves) + new Vector3(0.25f, 0.5f);
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
        StartCoroutine(FadeOutAudio());
        //       PlayerTurnManager.Instance.ChangeTurn();
    }

   
    private void OnWrongMove()
    {
        _wrongMoves++;
        StartCoroutine(WrongNoteDisplay());
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

    public void Fall()
    {
        StartCoroutine(Falling());
    }
    IEnumerator Falling ()
    {
        Grid.Instance.DisableTiles();
        float timeFalling = 1.0f;
        float growthAmount = 4;

        while (timeFalling > 0.0f)
        {
            _testVideo.transform.localScale += Vector3.one * growthAmount * Time.deltaTime;
            timeFalling -= Time.deltaTime;
            //_testVideo.transform.Rotate(transform.forward, 360 * Time.deltaTime);
            yield return null;

        }
        timeFalling = 1.0f;

        while (timeFalling > 0.0f)
        {
            _testVideo.transform.localScale -= Vector3.one * growthAmount * Time.deltaTime;
            timeFalling -= Time.deltaTime;
            //_testVideo.transform.Rotate(transform.forward, 360 * Time.deltaTime);
            yield return null;

        }
        Grid.Instance.EnableTiles();
    }
    private void OnPlayerLost()
    {
        DisableUI();

        UIManager.Instance.EraseUIText();
        _playerHasLost = true;
        EnterUIScreen();
        PlayerTurnManager.Instance.ShowLossScreen();
        StartCoroutine(FadeOutAudio());

    }

    protected override void EndMelody()
    {
        base.EndMelody();
        UIManager.Instance.SetUIText("");// _numberOfCorrectMovesNeeded + " correct moves left");

        if (PlayerTurnManager.Instance.GetIsFirstTurn())
        {
            UIManager.Instance.SetInputText("Press Space to Listen to Melody Again");

        }
    }
    private void DisableUI()
    {
        int numberOfUINotes = Mathf.Min(_uiForCheckingTheMoves.Count, _numberOfCorrectMovesNeeded);
        for (int i = _uiForCheckingTheMoves.Count - numberOfUINotes; i < _uiForCheckingTheMoves.Count; i++)
        {
            _uiForCheckingTheMoves[i].enabled = false;
        }
        _bench.TurnOffSprites();
    }
    private void EnableUI()
    {
        int numberOfUINotes = Mathf.Min(_uiForCheckingTheMoves.Count, _numberOfCorrectMovesNeeded);

        for (int i = _uiForCheckingTheMoves.Count - numberOfUINotes; i < _uiForCheckingTheMoves.Count; i++)
        {
            _uiForCheckingTheMoves[i].enabled = true;
            _uiForCheckingTheMoves[i].sprite = _uiNotCompleted;
        }
        _bench.TurnOnSprites();

    }

    private void UpdateUI()
    {
        _uiForCheckingTheMoves[_uiForCheckingTheMoves.Count - 1 - _numberOfCorrectMovesNeeded + _correctMoves].sprite = _uiSuccess;
    }



    protected override void OnNarrativeEnd()
    {
        PlayerTurnManager.Instance.StartClockBackward();
    }


    IEnumerator WrongNoteDisplay()
    {
        _uiForCheckingTheMoves[_correctMoves].sprite = _uiFail;
        yield return new WaitForSeconds(.5f);
        _uiForCheckingTheMoves[_correctMoves].sprite = _uiNotCompleted;

    }



}


