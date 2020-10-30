using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    private void Awake()
    {
        CheckIfDuplicated();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ReturnToMenu();   
    }
    void CheckIfDuplicated()
    {
        LevelsManager[] levels = FindObjectsOfType<LevelsManager>();
        if (levels.Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
    void LoadNight(int night)
    {
        SceneManager.LoadScene(night);
    }
    public void Lose()
    {
        LoadNight(7);
    }
    public void ReturnToMenu()
    {
        LoadNight(0);
    }
    public void LoadNextNight()
    {
        LoadNight(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Reload()
    {
        LoadNight(SceneManager.GetActiveScene().buildIndex);
    }
    public Scene GetNight()
    {
        return SceneManager.GetActiveScene();
    }
}
