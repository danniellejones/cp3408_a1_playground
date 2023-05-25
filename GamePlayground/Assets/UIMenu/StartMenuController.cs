using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [Header("Scene to Load")]
    public string startLevel;
    private AudioSource startMenuAudioSource;
    public AudioClip[] startMenuAudioClips;

    // Start is called before the first frame update
    void Start()
    {
        startMenuAudioSource = GetComponent<AudioSource>();
        PlayStartMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void PlayStartMenuMusic()
    {
        if (startMenuAudioClips == null)
            return;

        startMenuAudioSource.loop = false;
        AudioClip clip = startMenuAudioClips[Random.Range(0, startMenuAudioClips.Length)];
        startMenuAudioSource.PlayOneShot(clip);
    }


}
