using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject pauseUI;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    public GameObject normal;
    public GameObject fast;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) /*|| Input.GetKeyDown(KeyCode.P)*/)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        
        if (ui.activeSelf)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Toggle1()
    {
       
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Fast()
    {

        
        Time.timeScale = 2f;
        normal.SetActive(true);
        fast.SetActive(false);
        
    }

    public void Normal()
    {

        Time.timeScale = 1f;
        normal.SetActive(false);
        fast.SetActive(true);
    }

    public void Retry()
    {
        Toggle();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        sceneFader.FateTo(SceneManager.GetActiveScene().name);
        
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FateTo(menuSceneName);
        
    }


}
