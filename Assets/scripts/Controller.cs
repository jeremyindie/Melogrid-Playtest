using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    protected LayerMask _boundary;


    [SerializeField]
    private float _movementSpeed;
    protected float _movementDistance;

    private float _lerpDistanceToNewPosition;
    private bool _isLerpingToNewPosition;

    protected bool _playerHasLost;
    protected bool _inUIScreen;
    protected bool _doesCharacterMoveWithPositiveY; 

    protected bool _canMove;
    protected bool _isActive;

    protected bool _verticalKeyDown;
    protected bool _horizontalKeyDown;


    protected Vector3 _originalPosition;
    protected Vector3 _newPosition;

    protected List<Directions> _moveList;

    protected Directions _upKeyDirection;
    protected Directions _leftKeyDirection;
    protected Directions _rightKeyDirection;

    protected SpriteRenderer _spriteRenderer;
    protected List<Directions> _melody;

    protected bool _narrativeElementReady;

    protected AudioSource _audio;

    //playing sounds
    [SerializeField]
    protected float _playbackSpeedModifier = 1.0f;
    protected bool _isPlayingMelodyNote;
    protected bool _isPlayingMelody;
    protected int _melodyNote;
    protected float _timeInMelodyNote;

    public enum Directions
    {
        UP,
        LEFT,
        RIGHT
    };

    protected void Start()
    {
        _moveList = new List<Directions>();
        _movementDistance = Grid.Instance.GetGridDimention();
        _inUIScreen = false;
        _playerHasLost = false;
        _horizontalKeyDown = false;
        _verticalKeyDown = false;
        _originalPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected void Update()
    {
        if (!_isActive) return;

        if (_inUIScreen)
        {
            if (Input.GetKeyDown("space"))
            {
                InteractWithUI();
            }
        } else if (_isPlayingMelody && !_isPlayingMelodyNote)
        {
            _isPlayingMelodyNote = true;
            StartCoroutine(PlayMelodyNote(_melodyNote));
        } 
        else if (_canMove)
        {

            Move();
        }
        else if (_isLerpingToNewPosition)
        {
            LerpToNewPosition();
        }
       
    }
    private void LerpToNewPosition()
    {

        if (_lerpDistanceToNewPosition > 1)
        {
            _isLerpingToNewPosition = false;
            _canMove = true;
            _originalPosition = _newPosition;
            OnMoveEnd(_newPosition - _originalPosition);

            return;
        }
        _lerpDistanceToNewPosition += _movementSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(_originalPosition, _newPosition, _lerpDistanceToNewPosition);
    }

    private void InteractWithUI()
    {
        ExitUIScreen();
        if (PlayerTurnManager.Instance.IsInNarrativeScreen())
        {
            OnNarrativeEnd();
        }
        if (_playerHasLost)
        {
            PlayerTurnManager.Instance.OnLossRestart();
        }
    }


    protected virtual void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _newPosition = Vector3.zero;

        if (horizontal < 0 && !_horizontalKeyDown)
        {
            MoveDiagonallyLeftUp();
            _horizontalKeyDown = true;
        }
        else if (horizontal > 0 && !_horizontalKeyDown)
        {
            MoveDiagonallyRightUp();
            _horizontalKeyDown = true;

        }
        else if (vertical > 0 && !_verticalKeyDown)
        {
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
    }

    protected virtual void MoveUp(bool flipTheMovement= false)
    {
        int directionModifier = (_doesCharacterMoveWithPositiveY ^ flipTheMovement) ? 1 : -1;

        _newPosition = _originalPosition + Vector3.up * _movementDistance * directionModifier;
        StartMove(Directions.UP);

    }


    protected virtual void MoveDiagonallyLeftUp(bool flipTheMovement = false)
    {
        int directionModifier = (_doesCharacterMoveWithPositiveY ^ flipTheMovement) ? 1 : -1;

        _newPosition = _originalPosition + Vector3.up * _movementDistance * directionModifier - Vector3.right * _movementDistance * directionModifier;
        StartMove(Directions.LEFT);
       
    }

    protected virtual void  MoveDiagonallyRightUp(bool flipTheMovement = false)
    {
        int directionModifier = (_doesCharacterMoveWithPositiveY ^ flipTheMovement) ? 1 : -1;

        _newPosition = _originalPosition + Vector3.up * _movementDistance * directionModifier + Vector3.right * _movementDistance * directionModifier;
        StartMove(Directions.RIGHT);

    }

    protected void StartMove(Directions direction)
    {
        if (IsMoveLegal())
        {
            _isLerpingToNewPosition = true;
            _lerpDistanceToNewPosition = 0.0f;
            _canMove = false;
            OnMoveStart(direction);
        }
        else
        {
            OnIllegalMove();
        }
    }
    public void EnterUIScreen()
    {
        _inUIScreen = true;
    }

    public void ExitUIScreen()
    {
        _inUIScreen = false;
        NarrativeManager.Instance.TurnOffNarrativeScreen();

    }
    public virtual void StartTurn()
    {

    }

    protected string DirectionToString(Directions direction)
    {
        string dir = "";
        switch (direction)
        {
            case Directions.UP:
                dir = "Up";
                break;
            case Directions.LEFT:
                dir = "Left";
                break;
            case Directions.RIGHT:
                dir = "Right";
                break;

        }
        return dir;
    }

    

    protected void RandomizeDirections()
    {
        Directions [] dirs = PlayerTurnManager.Instance.GetRandomizedDirections();
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

    protected virtual bool IsMoveLegal()
    {
        return true;
    }
    protected virtual void OnMoveStart(Directions direction)
    {
        _moveList.Add(direction);
        string clipName = AudioManager.Instance.GetClipNameFromDirection(direction);
        AudioManager.Instance.Play(clipName);

    }

    protected virtual void OnMoveEnd(Vector2 moveDelta)
    {

    }

    protected void OnIllegalMove()
    {

    }


    protected virtual void OnTurnEnd()
    {
        PlayerTurnManager.Instance.ChangeTurn();
    }

    public void SetPosition(Vector3 position)
    {
        _originalPosition = position;
        transform.position = position;
    }

    public List <Directions> GetMoveList()
    {
        return _moveList;

    }
    protected Directions GetAdjustedDirection(Directions direction)
    {
        switch (direction)
        {
            case Directions.UP:
                return _upKeyDirection;
            case Directions.LEFT:
                return _leftKeyDirection;
            case Directions.RIGHT:
                return _rightKeyDirection;

        }
        return _upKeyDirection;
    }


    protected void PlayMelody()
    {
        _isPlayingMelody = true;
        _canMove = false;
        _isPlayingMelodyNote = true;
        _melodyNote = 0;
        StartCoroutine(PlayMelodyNote(_melodyNote));

    }


    IEnumerator PlayMelodyNote(int indexOfDirection)
    {
        Directions direction = _melody[indexOfDirection];

        string clipName = AudioManager.Instance.GetClipNameFromDirection(direction);

        AudioManager.Instance.Play(clipName);

        yield return new WaitForSeconds(AudioManager.Instance.GetClipLength(clipName) * _playbackSpeedModifier);

        _isPlayingMelodyNote = false;

        if (_melodyNote + 1 == _melody.Count)
        {
            EndMelody();
        }
        _melodyNote++;

    }

    protected void CreateMelody(List<Directions> melody)
    {
        _melody = melody;
    }

    public void EnableSprite()
    {
        _spriteRenderer.enabled = true;
    }
    public void SetNarrativeElementReady(bool isReady)
    {
        _narrativeElementReady = isReady; 
    }

    protected virtual void EndMelody()
    {
        _isPlayingMelody = false;
        _canMove = true;

    }

    protected virtual void OnNarrativeEnd()
    {

    }


    protected IEnumerator FadeInAudio()
    {
        float timeInFade = 0.25f;
        _audio.UnPause();
        while (timeInFade > 0.0f)
        {

            timeInFade -= Time.deltaTime;
            _audio.volume = Mathf.Lerp(0.0f, 1.0f, ((0.25f - timeInFade) * 4.0f));
            yield return null;

        }

    }

    protected IEnumerator FadeOutAudio()
    {
        float timeOutFade = 0.25f;
        while (timeOutFade > 0.0f)
        {

            timeOutFade -= Time.deltaTime;
            _audio.volume = Mathf.Lerp(1.0f, 0.0f, ((0.25f - timeOutFade) * 4.0f));
            yield return null;

        }
        _audio.Pause();

    }


}
