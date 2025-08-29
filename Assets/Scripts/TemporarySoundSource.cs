using System;
using UnityEngine;

public class TemporarySoundSource : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasStartedPlaying;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the SpawnedSoundSource GameObject.");
        }
    }

    private void Update()
    {
        if (hasStartedPlaying && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }

    public void PlaySound()
    {
        if (audioSource != null)
        {
            hasStartedPlaying = true;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
