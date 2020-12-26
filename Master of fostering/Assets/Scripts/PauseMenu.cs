using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Resume()
    {
        GameManagement.instance.Resume();
    }

    public void Exit()
    {
        GameManagement.instance.QuitGame();
    }

    public void Menu()
    {
        GameManagement.instance.LoadMenu();
    }
}
