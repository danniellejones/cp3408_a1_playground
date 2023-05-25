using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AmbientMusicPlayer;

public class ObjectAudioPlayer : MonoBehaviour
{
    private AudioSource objectAudioSource;
    public bool loopAudio = true;
    public AudioClip[] objectClips;

    // Start is called before the first frame update
    void Start()
    {
        objectAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!objectAudioSource.isPlaying)
        {
           PlayRandomAudioClip();
        }
    }

    public void PlayRandomAudioClip()
    {
        objectAudioSource.clip = GetRandomClip();

        AudioClip clip = GetRandomClip();

        if (clip != null)
        {
            objectAudioSource.clip = clip;
            objectAudioSource.loop = loopAudio;
            objectAudioSource.Play();
        }
    }

    public AudioClip GetRandomClip()
    {
        return objectClips[Random.Range(0, objectClips.Length)];
      
    }
}
