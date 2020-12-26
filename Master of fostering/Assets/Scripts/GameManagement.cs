using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    public static bool isGamePaused = false;
    public GetBlurTexture blurTexture;
    public GameObject pauseMenu;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume(bool triggerBySkill = false)
    {
        if(!triggerBySkill) pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Pause(bool resumeBySkill = false)
    {
        if(!resumeBySkill) pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        blurTexture.GetBlurMaterial();
        isGamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Load Menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }
}
