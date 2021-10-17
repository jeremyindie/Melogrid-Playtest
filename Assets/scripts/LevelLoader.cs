using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
