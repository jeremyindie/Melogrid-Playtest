using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private Instrument _startingInstrument;

    private Instrument _currentInstrument; 

    private Sounder[] sounds;



    /// <summary>
    /// /////////////////////////////////////////////////////
    /// Copy paste all of this
    /// Change "SingletonPattern everywhere for the name of your class.
    /// YOU'RE DONE. You can now use SingletonPattern.Instance.WhateverYouLike(); from ANYWHERE
    /// </summary>
    private static AudioManager s_instance;

    public static AudioManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                AudioManager singleton = GameObject.FindObjectOfType<AudioManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    s_instance = go.AddComponent<AudioManager>();
                }
            }
            return s_instance;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        s_instance = this;
        DontDestroyOnLoad(gameObject);
        sounds = new Sounder[4];
        _currentInstrument = _startingInstrument;
        InitializeCurrentInstrument();


    }


    private void InitializeCurrentInstrument()
    {

        sounds[0] = _currentInstrument.GetUpSound();
        sounds[1] = _currentInstrument.GetLeftSound();
        sounds[2] = _currentInstrument.GetRightSound();
        sounds[3] = _currentInstrument.GetWrongSound();
    }

    public void SetNewInstrument(Instrument newInstrument)
    {
        _currentInstrument = newInstrument;
        InitializeCurrentInstrument();
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sounder s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    public float GetClipLength(string name)
    {
        Sounder s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return 0.0f;
        }
        return s.source.clip.length;

    }
    public string GetClipNameFromDirection(Controller.Directions direction)
    {
        string clipName = "";
        switch (direction)
        {
            case Controller.Directions.UP:
                clipName = "UpSound";
                break;
            case Controller.Directions.LEFT:
                clipName = "LeftSound";
                break;
            case Controller.Directions.RIGHT:
                clipName = "RightSound";
                break;
        }
        return clipName;
    }
    
}
