using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int levelNumber = 1;
    public string sceneToLoad;

    private void Awake()
    {
       if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
       else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void NextLevel()
    {
        levelNumber++;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void RestartLevel()
    {
        levelNumber = 0;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void GameOver()
    {
        // Show UI for death
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("LevelNumber", levelNumber);
    }

    public void ResetLevelNumber()
    {
        levelNumber = 1;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }
}
