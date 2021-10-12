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


    void Start()
    {
        _clockFaceSprite = GetComponent<SpriteRenderer>();
        _isAnimated = false;
        StartTimeForward();
        
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
                _speedIncreasing = false;
                _lerpValueA = _lerpValueB;
                _lerpValueB = 0.0f;
                _lerpAmount = 0.0f;
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
    }
    public void StartTimeForward()
    {
        _isAnimated = true;
        _speedIncreasing = true;
        _lerpValueA = 0.0f;
        _lerpValueB = _maxSpeed;
        _lerpAmount = 0.0f;
    }
    public void StartTimeRewind()
    {
        _isAnimated = false;
        _speedIncreasing = true;
        _lerpValueA = 0.0f;
        _lerpValueB = -_maxSpeed;
        _lerpAmount = 0.0f;
    }

    private void OnFinish()
    {
        _clockFaceSprite.enabled = false;
        _minuteHandSprite.enabled = false;
        _hourHandSprite.enabled = false;
        _isAnimated = false;

    }
}
