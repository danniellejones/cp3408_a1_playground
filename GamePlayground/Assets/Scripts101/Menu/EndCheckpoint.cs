using UnityEngine;

// Add this to an empty game object with a box collider at the end of the level
public class EndCheckpoint : MonoBehaviour
{
    public AudioClip victoryAudio;
    private AudioSource audioSource;
    GameManager gameManager;

    private void Awake()
    {
        //gameManager = GameManager.instance;
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log(gameManager.ToString());
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayAudioClip(victoryAudio);
        
        //if (other.GetComponent<Controller>() == null)
        //    return;

       gameManager.HandleLevelSuccess();
        //Destroy(gameObject);
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.loop = false;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}