using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource AudioSource;
    
    public AudioClip background;
    public AudioClip success;
    public AudioClip defeat;

    private void Awake()
    {
        instance = this; 
        AudioSource = GetComponent<AudioSource>();
        PlayBackground();
    }

    public void PlayBackground()
    {
        StopAudioEffect();
        PlayAudioEffect(background, true);
        AudioSource.loop = true;
    }

    public void PlaySuccess()
    {
        StopAudioEffect();
        PlayAudioEffect(success, false);
    }

    public void PlayDefeat()
    {
        StopAudioEffect();
        PlayAudioEffect(defeat, false);
    }

    private void PlayAudioEffect(AudioClip clip, bool inLoop)
    {
        AudioSource.clip = clip;
        AudioSource.loop = inLoop;
        AudioSource.Play();
    }

    private void StopAudioEffect()
    {
        AudioSource.Stop();
    }

}
