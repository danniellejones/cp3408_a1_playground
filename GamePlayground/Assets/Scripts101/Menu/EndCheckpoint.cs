using UnityEngine;

// Add this to an empty game object with a box collider at the end of the level
public class EndCheckpoint : MonoBehaviour
{
    public AudioClip victoryAudio;

    private AudioSource audioSource;
    
    void OnTriggerEnter(Collider other)
    {
        PlayAudioClip(victoryAudio);
        
        if (other.GetComponent<Controller>() == null)
            return;

        GameSystem.Instance.StopTimer();
        GameSystem.Instance.FinishRun();
        Destroy(gameObject);
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