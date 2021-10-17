using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    [SerializeField]
    private Transform _hourHand;
    [SerializeField]
    private SpriteRenderer _hourHandSprite;
    [SerializeField]
    private Transform _minuteHand;
    [SerializeField]
    private SpriteRenderer _minuteHandSprite;
    private SpriteRenderer _clockFaceSprite; 
    
    
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _numberOfSeconds; 

    private float _angle;
    private float _speed;
    private bool _speedIncreasing;
    private float _lerpAmount; 
    private float _lerpValueA;
    private float _lerpValueB;
    private bool _isAnimated;
    private bool _isForwardInTime;
    private AudioSource _audio;

    void Start()
    {
        _clockFaceSprite = GetComponent<SpriteRenderer>();
        _isAnimated = false;
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAnimated) AnimateTime();
        
    }

    public void AnimateTime()
    {
        if (_lerpAmount >= 1)
        {
            if (_speedIncreasing)
            {
                HalfwayPointOfClock();
            } else
            {
                OnFinish();
            }
        }
        _lerpAmount += Time.deltaTime / _numberOfSeconds;
        _speed = Mathf.Lerp(_lerpValueA, _lerpValueB, _lerpAmount);
        _angle = _speed * Time.deltaTime;
        _hourHand.Rotate(-transform.forward, _angle);
        _minuteHand.Rotate(-transform.forward, _angle * 10);
        float scale = 1.0f;
        if (_speedIncreasing)
        {
            scale = Mathf.Lerp(0.0f, 4.0f, _lerpAmount);

        }
        else
        {
            scale = Mathf.Lerp(0.0f, 4.0f,1.0f - _lerpAmount);

        }
        transform.localScale = Vector3.one * scale;

    }
    public void StartTimeForward()
    {
        _isForwardInTime = true;
        _lerpValueB = _maxSpeed;
        transform.position = PlayerTurnManager.Instance.GetPlayerOneTransform().position;
        InitializeClock();
        _audio.Play();
    }
    public void StartTimeRewind()
    {
        _isForwardInTime = false;
        _lerpValueB = -_maxSpeed;
        transform.position = PlayerTurnManager.Instance.GetPlayerTwoTransform().position;
        InitializeClock();
        _audio.Play();
    }


    private void HalfwayPointOfClock()
    {
        PlayerTurnManager.Instance.PrepareNextCharacter();

        _speedIncreasing = false;
        _lerpValueA = _lerpValueB;
        _lerpValueB = 0.0f;
        _lerpAmount = 0.0f;
        if (_isForwardInTime)
        {
            transform.position = PlayerTurnManager.Instance.GetPlayerGreyTransform().position;
        }
        else
        {
            transform.position = PlayerTurnManager.Instance.GetPlayerOneTransform().position;
        }


    }
    private void InitializeClock()
    {

        _lerpAmount = 0.0f;
        _lerpValueA = 0.0f;
        _isAnimated = true;
        _speedIncreasing = true;
        transform.localScale = Vector3.zero;
        _clockFaceSprite.enabled = true;
        _minuteHandSprite.enabled = true;
        _hourHandSprite.enabled = true;
    }

    private void OnFinish()
    {
        _clockFaceSprite.enabled = false;
        _minuteHandSprite.enabled = false;
        _hourHandSprite.enabled = false;
        _isAnimated = false;
        _audio.Stop();
        PlayerTurnManager.Instance.ChangeTurn(true, 0.0f);

    }
}
