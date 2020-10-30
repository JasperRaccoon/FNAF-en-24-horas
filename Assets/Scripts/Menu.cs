using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    LevelsManager manager;

    private void Start()
    {
        manager = FindObjectOfType<LevelsManager>();
    }
    public void Play()
    {
        manager.LoadNextNight();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void MenuButton()
    {
        manager.ReturnToMenu();
    }
}
