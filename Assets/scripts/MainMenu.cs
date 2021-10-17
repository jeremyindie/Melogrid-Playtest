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
    public ColourChanges SpaceTextColour;
    public ColourChanges BackgroundColour;

    public Button StartGameButton;
    public Button ExitGameButton;

    private bool CanPressSpace;
    private bool StartButtonClicked;

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

        StartButtonClicked = true;
        //SceneManager.LoadScene("prototype 2");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(StartButtonClicked)
        {
            if (MenuDrawingColour.ColourLerp >= 1)
            {
                Story01Colour.ColourFadeType = ColourChanges.FadeType.FadeToColour;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
    }
}
