using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourChanges : MonoBehaviour
{
    //public Image gameObjectImage;
    //public Text gameObjectText;
    public Graphic UIObject;
    public Color StartColour = Color.white;
    public Color EndColour;
    public float ColourLerp;
    public float ColourChangeSpeed = 0.2f;
    public enum FadeType
    {
        NoChange,
        PingPong,
        FadeToColour,
    }
    public FadeType ColourFadeType;
    private bool GoingUp;
    public bool IsReset;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Image>())
        {
            UIObject = gameObject.GetComponent<Image>();
        }
        else if (gameObject.GetComponent<Text>())
        {
            UIObject = gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        colourManager();
    }

    private void colourManager()
    {
        
        if(ColourFadeType == FadeType.NoChange)//If no colour change yet.
        {
            StartColour = UIObject.color;
            ColourLerp = 0;
        }        
        else if(ColourFadeType == FadeType.PingPong)//Lerp between two colours back and forth.
        {
            if (ColourLerp >= 1)
            {
                GoingUp = false;
            }
            else if (ColourLerp <= 0)
            {
                GoingUp = true;
            }

            if (GoingUp)
            {
                ColourLerp += Time.deltaTime * ColourChangeSpeed;
            }
            else if (!GoingUp)
            {
                ColourLerp -= Time.deltaTime * ColourChangeSpeed;
            }
        }
        else if(ColourFadeType == FadeType.FadeToColour)//Lerp towards a target colour from current colour.
        {
            if(!IsReset)
            {
                ColourLerp = 0;
                StartColour = UIObject.color;
                IsReset = true;
            }

            if(ColourLerp <= 1)
            {
                ColourLerp += Time.deltaTime * ColourChangeSpeed;
            }
        }

        UIObject.color = Color.Lerp(StartColour, EndColour, ColourLerp);
    }
}
