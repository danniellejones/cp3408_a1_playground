using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    // World Values to track - public accessible methods below
    private int levelNumber = 1;
    private int numberOfKeys = 0;

    // UI Menus
    private GameObject startMenuUI;
    private GameObject endLevelUI;
    private GameObject pauseMenuUI;
    private GameObject deathMenuUI;
    private GameObject gamePlayUI;

    public string sceneToLoad;

    // Set up audio for UI Menus
    [Header("Audio")]
    private WorldMusicPlayer worldMusicPlayer;
    private AudioSource gameManagerAudioSource;
    public AudioClip[] startMenuAudioClips;
    public AudioClip selectButtonAudioClip;
    public AudioClip onDeathAudioClip;

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
        gameManagerAudioSource = gamePlayUI.GetComponent<AudioSource>();

        //startMenuUI = startMenuUI.GetComponent<GameObject>();
        //startMenuUI.SetActive(true);
        //Scene currentScene = SceneManager.GetActiveScene();
        //string sceneName = currentScene.name;
        //if (currentScene.name == "startMenu")
        //{
        //    PlayAudioClip(GetRandomAudioClip(startMenuAudioClips), true);
        //}
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);

        // Assign UI instances to Game Manager
        //startMenuUI = startMenuUI.GetComponent<GameObject>();
        gamePlayUI = gamePlayUI.GetComponent<GameObject>();
        endLevelUI = endLevelUI.GetComponent<GameObject>();
        pauseMenuUI = pauseMenuUI.GetComponent<GameObject>();
        deathMenuUI = deathMenuUI.GetComponent<GameObject>();
        worldMusicPlayer = FindObjectOfType<WorldMusicPlayer>();

        // Activate only the game play UI
        gamePlayUI.SetActive(true);
        endLevelUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        deathMenuUI.SetActive(false);
    }

    public void incrementLevel()
    {
        levelNumber++;
    }

    public void onSuccessfulLevelCompletion()
    {
        gamePlayUI.SetActive(false);
        endLevelUI.SetActive(true);
        incrementLevel();
    }

    public void LoadNextLevel()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        endLevelUI.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
        gamePlayUI.SetActive(true);
    }

    public void RestartLevel()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        deathMenuUI.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
        gamePlayUI.SetActive(true);
    }

    public void GameOver()
    {
        PlayAudioClip(onDeathAudioClip, false);
        levelNumber = 0;
        gamePlayUI.SetActive(false);
        deathMenuUI.SetActive(true);
    }

    public void ExitGame()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        PlayerPrefs.SetInt("LevelNumber", levelNumber);
        PlayerPrefs.SetInt("NumberOfKeys", numberOfKeys);
        Application.Quit();
    }

    public void PauseGame()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        Time.timeScale = 0f;
        gamePlayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        worldMusicPlayer.SetWorldState(WorldMusicPlayer.WorldState.InMenu);
    }

    public void ResumeGame()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gamePlayUI.SetActive(true);
        worldMusicPlayer.SetWorldState(WorldMusicPlayer.WorldState.Idle);
    }

    public void ResetLevelNumber()
    {
        levelNumber = 1;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    private AudioClip GetRandomAudioClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }

    private void PlayAudioClip(AudioClip clip, bool loopCondition)
    {
        gameManagerAudioSource.loop = loopCondition;

        if (clip == null)
            return;

        gameManagerAudioSource.PlayOneShot(clip);
    }
}
