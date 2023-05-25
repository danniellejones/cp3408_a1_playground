using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Random Ambient Music Player based on states
/** Code to add to trigger states where required:
 * Add audio source object and add this script - make audio source 2D
 * Add audio clips for each state
 */
public class AmbientMusicPlayer : MonoBehaviour
{
    private AudioSource ambientMusicAudioSource;
    private bool isPlaying = false;
    private bool isOnCoolDown = false;
    private float ambientAudioCoolDown = 2f;

    [SerializeField] public float distanceThreshold = 20f;
    private bool hasReachedEnd = false;
    private GameObject player;

    private void Awake()
    {
        ambientMusicAudioSource = GetComponent<AudioSource>();
    }

    public enum LocationState
    {
        Adventure,
        Danger,
        Safe
    }

    public LocationState locationState;

    public AudioClip[] adventureClips;
    public AudioClip[] dangerClips;
    public AudioClip[] safeClips;
    public AudioClip defaultClip;

    public AudioClip GetRandomClip()
    {
        switch (locationState)
        {
            case LocationState.Safe:
                if (safeClips.Length > 0)
                    return safeClips[Random.Range(0, safeClips.Length)];
                else
                    return defaultClip;
            case LocationState.Adventure:
                if (adventureClips.Length > 0)
                    return adventureClips[Random.Range(0, adventureClips.Length)];
                else
                    return defaultClip;
            case LocationState.Danger:
                if (dangerClips.Length > 0)
                    return dangerClips[Random.Range(0, dangerClips.Length)];
                else
                    return defaultClip;
            default:
                return null;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayRandomAudioClip();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!ambientMusicAudioSource.isPlaying && !isOnCoolDown)
        {
            isPlaying = false;
            StartCoroutine(PlayRandomAudioClipwithCoolDown());
        }
    }

    IEnumerator PlayRandomAudioClipwithCoolDown()
    {
        isOnCoolDown = true;

        PlayRandomAudioClip();
        float coolDownDuration = GetRandomCoolDownDuration();
        yield return new WaitForSeconds(coolDownDuration);
        isOnCoolDown = false;
    }

    public void PlayRandomAudioClip()
    {
        ambientMusicAudioSource.clip = GetRandomClip();

        AudioClip clip = GetRandomClip();

        if (clip != null)
        {
            ambientMusicAudioSource.clip = clip;
            ambientMusicAudioSource.Play();
        }
    }

    public void SetAmbientState(LocationState newLocationState)
    {
        locationState = newLocationState;

        if (isPlaying)
        {
            ambientMusicAudioSource.Stop();
            isPlaying = false;
        }
        PlayRandomAudioClip();
    }

    float GetRandomCoolDownDuration()
    {
        float maxCoolDown = ambientAudioCoolDown;
        float minCoolDown = 0f;
        float maxCoolDownBias = maxCoolDown * 0.7f;

        float randomValue = Random.Range(minCoolDown, maxCoolDown + maxCoolDownBias);
        float biasedValue = Mathf.Pow(randomValue, maxCoolDownBias);

        return biasedValue;
    }

    private void checkAndUpdateState()
    {
        if (!hasReachedEnd)
        {
            GameObject endCheckpoint = GameObject.FindGameObjectWithTag("End");
            if (Vector3.Distance(player.transform.position, endCheckpoint.transform.position) <= distanceThreshold)
            {
                hasReachedEnd = true;
                SetNextAmbientState();
            }
        }
    }
    private void SetNextAmbientState()
    {
        int nextLocationState = (int)locationState + 1;
        if (nextLocationState > (int)LocationState.Danger)
            nextLocationState = (int)LocationState.Safe;

        SetAmbientState((LocationState)nextLocationState);
    }

}
