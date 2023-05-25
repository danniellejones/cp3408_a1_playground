using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Random World Music Player based on states
/** Code to add to trigger states where required:
 * WorldMusicPlayer worldMusicPlayer;  -- Inside the class --
 * worldMusicPlayer = FindObjectOfType<WorldMusicPlayer>();  -- Add this to awake --
 * worldMusicPlayer.SetCharacterState(WorldMusicPlayer.CharacterState.Idle);  -- Add this to start and update --
 * worldMusicPlayer.SetCharacterState(WorldMusicPlayer.CharacterState.Attacking);  -- update on change --
 * worldMusicPlayer.SetCharacterState(WorldMusicPlayer.CharacterState.InMenu);  -- update on change --
 */
public class WorldMusicPlayer : MonoBehaviour
{
    private AudioSource worldMusicPlayerAudioSource;
    private bool isPlaying = false;

    private void Awake()
    {
        worldMusicPlayerAudioSource = GetComponent<AudioSource>();
    }

    public enum CharacterState
    {
        Idle,
        Attacking,
        InMenu
    }

    public CharacterState characterState;

    public AudioClip[] idleClips;
    public AudioClip[] attackClips;
    public AudioClip[] inMenuClips;

    public AudioClip GetRandomClip()
    {
        switch (characterState)
        {
            case CharacterState.Idle:
                return idleClips[Random.Range(0, idleClips.Length)];
            case CharacterState.Attacking:
                return attackClips[Random.Range(0, attackClips.Length)];
            case CharacterState.InMenu:
                return inMenuClips[Random.Range(0, inMenuClips.Length)];
            default:
                return null;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayRandomAudioClip();
    }

    // Update is called once per frame
    void Update()
    {
        if(!worldMusicPlayerAudioSource.isPlaying)
        {
            isPlaying = false;
            PlayRandomAudioClip();
        }
    }

    public void PlayRandomAudioClip()
    {
        worldMusicPlayerAudioSource.clip = GetRandomClip();

        AudioClip clip = GetRandomClip();

        if(clip != null)
        {
            worldMusicPlayerAudioSource.clip = clip;
            worldMusicPlayerAudioSource.Play();
        }
    }

   public void SetCharacterState(CharacterState newCharacterState)
    {
        characterState = newCharacterState;

        if (isPlaying)
        {
            worldMusicPlayerAudioSource.Stop();
            isPlaying = false;
        }
        PlayRandomAudioClip();
    }


}
