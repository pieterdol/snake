using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Scenes")]
    public string MainMenuScene;
    public string DifficultySelectScene;
    public string HighscoresScene;
    public string GameScene;
    public string WinScene;
    public string LoseScene;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void LoadDifficultySelectScene()
    {
        SceneManager.LoadScene(DifficultySelectScene);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void LoadHighscoresScene()
    {
        SceneManager.LoadScene(HighscoresScene);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(WinScene);
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene(LoseScene);
    }
}
