using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private GameObject TemporarySoundPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Multiple AudioManager instances found!");
        }
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
