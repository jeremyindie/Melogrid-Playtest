using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{

    private  Image _image;
    [SerializeField]
    private Text _text; 
    [SerializeField]
    private List <string> _forTesting;
    private int _narrativePoint; 


    /// <summary>
    /// /////////////////////////////////////////////////////
    /// Copy paste all of this
    /// Change "SingletonPattern everywhere for the name of your class.
    /// YOU'RE DONE. You can now use SingletonPattern.Instance.WhateverYouLike(); from ANYWHERE
    /// </summary>
    private static NarrativeManager s_instance;

    public static NarrativeManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                NarrativeManager singleton = GameObject.FindObjectOfType<NarrativeManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    s_instance = go.AddComponent<NarrativeManager>();
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
        _image = GetComponent<Image>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNarrativePoint(int narrativePoint)
    {
        _narrativePoint = narrativePoint;
    }
    public void TurnOffNarrativeScreen()
    {
        _image.enabled = false;
        _text.enabled = false; 
    }

    public void DisplayCustomScreen(string text)
    {
        _text.enabled = true;
        _image.enabled = true;
        _text.text = text; 
    }
    public void DisplayNarrativeElement()
    {
        _text.enabled = true; 
        _image.enabled = true;
        _text.text = _forTesting[_narrativePoint];
    }
}
