using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void OnStartButton()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("prototype 2");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void Update()
    {

    }

}
