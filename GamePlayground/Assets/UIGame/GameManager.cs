using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // World Values to track
    private int levelNumber = 1;
    private int numberOfKeys = 0;

    // UI Menus
    private GameObject startMenuUI;
    private GameObject endLevelUI;
    private GameObject pauseMenuUI;
    private GameObject deathMenuUI;
    private GameObject gamePlayUI;

    [Header("Scene")]
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
        gameManagerAudioSource = this.GetComponent<AudioSource>();


        //startMenuUI = startMenuUI.GetComponent<GameObject>();
        //startMenuUI.SetActive(true);
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (currentScene.name == "StartMenu")
        {
            PlayAudioClip(GetRandomAudioClip(startMenuAudioClips), true);
        }
    }

    private void Start()
    {
        //gameManagerAudioSource = this.GetComponent<AudioSource>();
    }

    // On start menu if Play is selected continue saved from save
    public void ContinueGame()
    {
        levelNumber = PlayerPrefs.HasKey("LevelNumber") ? PlayerPrefs.GetInt("LevelNumber") : 1;
        numberOfKeys = PlayerPrefs.HasKey("NumberOfKeys") ? PlayerPrefs.GetInt("NumberOfKeys") : 0;
        StartGame();
    }

    // On start menu if New Game is selected
        public void StartNewGame()
    {
        ResetLevelNumber();
        StartGame();
    }

    // Start game
    public void StartGame()
    {
        PlayAudioClip(selectButtonAudioClip, false);
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

    // Increment level
    public void incrementLevel()
    {
        levelNumber++;
    }

    // Display level complete UI
    public void onSuccessfulLevelCompletion()
    {
        gamePlayUI.SetActive(false);
        endLevelUI.SetActive(true);
        incrementLevel();
    }

    // Select next level button from End Level UI
    public void LoadNextLevel()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        endLevelUI.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
        gamePlayUI.SetActive(true);
    }

    // Select try again button from Death Menu UI
    public void RestartLevel()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        deathMenuUI.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
        gamePlayUI.SetActive(true);
    }

    // Display Death Menu UI on player death
    public void GameOver()
    {
        PlayAudioClip(onDeathAudioClip, false);
        levelNumber = 0;
        gamePlayUI.SetActive(false);
        deathMenuUI.SetActive(true);
    }

    // Save level and keys, then exit game
    public void ExitGame()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        PlayerPrefs.SetInt("LevelNumber", levelNumber);
        PlayerPrefs.SetInt("NumberOfKeys", numberOfKeys);
        Application.Quit();
    }

    // Open pause menu and pause game
    public void PauseGame()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        Time.timeScale = 0f;
        gamePlayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        worldMusicPlayer.SetWorldState(WorldMusicPlayer.WorldState.InMenu);
    }

    // Resume game from pause menu
    public void ResumeGame()
    {
        PlayAudioClip(selectButtonAudioClip, false);
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gamePlayUI.SetActive(true);
        worldMusicPlayer.SetWorldState(WorldMusicPlayer.WorldState.Idle);
    }

    // Reset Level Counter
    public void ResetLevelNumber()
    {
        levelNumber = 1;
    }

    // Get the level number
    public int GetLevelNumber()
    {
        return levelNumber;
    }

    // Get a random audio clip from an array
    private AudioClip GetRandomAudioClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }

    // Play audio clip, nominate looping or not
    private void PlayAudioClip(AudioClip clip, bool loopCondition)
    {
        gameManagerAudioSource.loop = loopCondition;

        if (clip == null)
            return;

        gameManagerAudioSource.PlayOneShot(clip);
    }
}
