using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public ColourChanges MelogridColour;
    public ColourChanges StartColour;
    public ColourChanges StartTextColour;
    public ColourChanges ExitColour;
    public ColourChanges ExitTextColour;
    public ColourChanges StringsColour;
    public ColourChanges MenuDrawingColour;
    public ColourChanges Story01Colour;
    public ColourChanges Story02Colour;
    public ColourChanges Story03Colour;
    public ColourChanges Story04Colour;
    public ColourChanges SpaceTextColour;
    public ColourChanges BackgroundColour;

    public AudioSource HappyMenuMusic;
    public AudioSource SadMenuMusic;
    private float FadeSpeed = 0.5f;
    private bool Fade01;
    private bool Fade02;

    public Button StartGameButton;
    public Button ExitGameButton;

    private bool CanPressSpace;
    private bool StartButtonClicked;
    private int StoryPhase;
    private bool OnceOnly;

    public void OnStartButton()
    {
        //Disable Clicks
        StartGameButton.enabled = false;
        ExitGameButton.enabled = false;

        //Fade Out
        MelogridColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
        StartColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
        StartTextColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
        ExitColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
        ExitTextColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
        MenuDrawingColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;

        //Audio
        Fade01 = true;
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void Update()
    {
        //Audio Events
        if(Fade01 == true && Fade02 == false)
        {
            HappyMenuMusic.volume -= Time.deltaTime * FadeSpeed;
            SadMenuMusic.volume += Time.deltaTime * FadeSpeed;
        }
        else if (Fade02)
        {
            SadMenuMusic.volume -= Time.deltaTime * FadeSpeed;
        }

        //Visual Events
        if(StoryPhase == 0 && MenuDrawingColour.ColourLerp >= 1)
        {
            Story01Colour.ColourFadeType = ColourChanges.FadeType.FadeToColour;

            if(Story01Colour.ColourLerp >= 1)
            {
                SpaceTextColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Story01Colour.EndColour = new Color(0, 0, 0, 0);
                    Story01Colour.IsReset = false;
                    SpaceTextColour.EndColour = new Color(0, 0, 0, 0);
                    SpaceTextColour.IsReset = false;
                    StoryPhase = 1;
                }
            }
        }
        else if(StoryPhase == 1)
        {
            Story02Colour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
            BackgroundColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;

            if(OnceOnly == false && Story02Colour.ColourLerp >= 1)
            {
                SpaceTextColour.EndColour = new Color(50/255f, 80/255f, 80/255f, 1f);
                SpaceTextColour.IsReset = false;
                OnceOnly = true;
            }
            else if(Story02Colour.ColourLerp >= 1 && Input.GetKeyDown(KeyCode.Space))
            {
                Story02Colour.EndColour = new Color(0, 0, 0, 0);
                Story02Colour.IsReset = false;
                SpaceTextColour.EndColour = new Color(0, 0, 0, 0);
                SpaceTextColour.IsReset = false;
                OnceOnly = false;
                StoryPhase = 2;
            }
        }
        else if (StoryPhase == 2)
        {
            Story03Colour.ColourFadeType = ColourChanges.FadeType.FadeToColour;

            if (OnceOnly == false && Story03Colour.ColourLerp >= 1)
            {
                SpaceTextColour.EndColour = new Color(50 / 255f, 80 / 255f, 80 / 255f, 1f);
                SpaceTextColour.IsReset = false;
                OnceOnly = true;
            }
            else if (Story03Colour.ColourLerp >= 1 && Input.GetKeyDown(KeyCode.Space))
            {
                Story03Colour.EndColour = new Color(0, 0, 0, 0);
                Story03Colour.IsReset = false;
                SpaceTextColour.EndColour = new Color(0, 0, 0, 0);
                SpaceTextColour.IsReset = false;
                BackgroundColour.EndColour = new Color(1, 1, 1, 1);
                BackgroundColour.IsReset = false;
                OnceOnly = false;
                StoryPhase = 3;
            }
        }
        else if (StoryPhase == 3)
        {
            Story04Colour.ColourFadeType = ColourChanges.FadeType.FadeToColour;

            if (OnceOnly == false && Story04Colour.ColourLerp >= 1)
            {
                SpaceTextColour.EndColour = new Color(50 / 255f, 50 / 255f, 50 / 255f, 1f);
                SpaceTextColour.IsReset = false;
                OnceOnly = true;
            }
            else if (Story03Colour.ColourLerp >= 1 && Input.GetKeyDown(KeyCode.Space))
            {
                Fade02 = true;
                Story04Colour.EndColour = new Color(0, 0, 0, 0);
                Story04Colour.IsReset = false;
                SpaceTextColour.EndColour = new Color(0, 0, 0, 0);
                SpaceTextColour.IsReset = false;
                BackgroundColour.EndColour = new Color(0, 0, 0, 1);
                BackgroundColour.IsReset = false;
                StringsColour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
                StringsColour.EndColour = new Color(0, 0, 0, 0);
            }
            
            if(StringsColour.IsReset && StringsColour.ColourLerp >= 1)
            {
                SceneManager.LoadScene("prototype 2");
            }
        }
    }
}
