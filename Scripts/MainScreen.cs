using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public string levelSelect = "LevelSelect";
    public GameObject helpUI;
    public SceneFader sceneFader;

    public void Play()
    {
        //SceneManager.LoadScene(levelToLoad);
        sceneFader.FateTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Help()
    {

        helpUI.SetActive(!helpUI.activeSelf);
        if (helpUI.activeSelf)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }

    }

    public void LevelSelect()
    {
        sceneFader.FateTo(levelSelect);
    }
}
