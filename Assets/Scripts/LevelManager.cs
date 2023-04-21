using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy[] enemys;

    private float delay = 1.5f;
    private int currentSceneIndex = 0;

    private void Awake()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Time.timeScale = 0;
    }
    // Dừng game
    public void PauseGame()
    {
        player.IsPause = true;
        foreach (Enemy enemy in enemys)
        {
            enemy.IsPause = true;
        }
    }
    // Tiếp tục game
    public void ContinueGame()
    {
        player.IsPause = false;
        foreach (Enemy enemy in enemys)
        {
            enemy.IsPause = false;
        }
    }
    // Bắt đầu lại game
    public void ResetGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        
    }
    // Chuyển scene tiếp theo
    public void LoadNextScene()
    {
        currentSceneIndex++;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
    // Chuyển scene sau 1 thời gian
    public void NextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Invoke("LoadNextScene", delay);
    }
}
