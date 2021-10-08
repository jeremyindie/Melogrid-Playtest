using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// /////////////////////////////////////////////////////
    /// Copy paste all of this
    /// Change "SingletonPattern everywhere for the name of your class.
    /// YOU'RE DONE. You can now use SingletonPattern.Instance.WhateverYouLike(); from ANYWHERE
    /// </summary>
    [SerializeField]
    private Text _uiHelperText;
    [SerializeField]
    private Text _inputText;
    private static UIManager s_instance;

    public static UIManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                UIManager singleton = GameObject.FindObjectOfType<UIManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    s_instance = go.AddComponent<UIManager>();
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

    }
    void Start()
    {
        
    }
    public void EraseInputText()
    {
        _inputText.text = "";
    }

    public void SetInputText(string text)
    {
        _inputText.text = text;
    }
    public void EraseUIText()
    {
        _uiHelperText.text = "";
    }

    public void SetUIText(string text)
    {
        _uiHelperText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
