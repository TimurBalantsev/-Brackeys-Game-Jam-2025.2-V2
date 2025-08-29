using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private GameObject TemporarySoundPrefab;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Multiple SoundManager instances found!");
        Instance = this;
    }

    public void SpawnTempSoundSourceAtWorldSpacePoint(Vector3 worldPos, AudioClipSO.AudioClipReference audioClipReference)
    {
        GameObject soundSource = Instantiate(TemporarySoundPrefab, worldPos, Quaternion.identity, transform);
        TemporarySoundSource tempSoundSource = soundSource.GetComponent<TemporarySoundSource>();
        tempSoundSource.SetAudioClip(audioClipReference.audioClip);
        tempSoundSource.SetVolume(audioClipReference.volume);
        tempSoundSource.PlaySound();
    }
}
