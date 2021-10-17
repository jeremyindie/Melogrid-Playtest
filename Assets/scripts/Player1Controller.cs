using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : Controller
{
    private int _moveNumber;
    private int _currentTurnLength;

    private void Awake()
    {
        _canMove = true;
        _isActive = true;
        _moveNumber = 0;
        _originalPosition = transform.position;
        _doesCharacterMoveWithPositiveY = true;
        _audio = GetComponent<AudioSource>();
        //potential for checkpoints to set the current turn length
        _currentTurnLength = 4; 

}

    

    private void Start()
    {
        base.Start();
        _audio.Pause();
        StartAudio();

    }
    private void Update()
    {


        base.Update();
        if (_isActive && _canMove && !_inUIScreen)
        {
            Move();
        }
    }
    public override void StartTurn()
    {
        _moveNumber = 0;
        _canMove = true;
    }

    protected override void OnMoveEnd(Vector2 moveDelta)
    {
        IncrementMove();
    }


    protected override bool IsMoveLegal()
    {
        if (!Physics2D.OverlapCircle(_newPosition, _movementDistance / 3, _boundary))
        {
            return true;
        }
        return false;

    } 
    
    public void IncrementMove()
    {
        _moveNumber++;
        if (_moveNumber >= _currentTurnLength)
        {
            OnTurnEnd();
        }

    }

    protected override void OnTurnEnd()
    {
        if (_narrativeElementReady)
        {
            PlayerTurnManager.Instance.StartNarrativeScreen();
        } else
        {
            PlayerTurnManager.Instance.StartClockForward();
        }
        StartCoroutine(FadeOutAudio());

    }

    protected override void OnNarrativeEnd()
    {
        PlayerTurnManager.Instance.StartClockForward();

    }

    public void StartAudio()
    {
        StartCoroutine(FadeInAudio());

    }



}
