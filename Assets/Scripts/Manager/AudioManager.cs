using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private GameObject tempSoundPrefab;

    [SerializeField] private float musicFade = 2f;

    private const string EXPOSED_PARAM_MENUMUSIC = "menuMusic";
    private const string EXPOSED_PARAM_GAMEMUSIC = "gameMusic";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }
        else
        {
            Debug.LogError("Multiple AudioManager instances found!");
        }
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                menuMusic.Play();
                StartCoroutine(StartFade(audioMixer, EXPOSED_PARAM_GAMEMUSIC, musicFade, 0));
                StartCoroutine(StartFade(audioMixer, EXPOSED_PARAM_MENUMUSIC, musicFade, 1));
                break;
            case "Game":
                gameMusic.Play();
                StartCoroutine(StartFade(audioMixer, EXPOSED_PARAM_MENUMUSIC, musicFade, 0));
                StartCoroutine(StartFade(audioMixer, EXPOSED_PARAM_GAMEMUSIC, musicFade, 1));
                break;
        }
    }
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, Action callback = null)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        callback?.Invoke();
        yield break;
    }

    public void PlayTempSoundAt(Vector3 worldPos, AudioClipSO.AudioClipReference audioClipReference)
    {
        GameObject soundSource = Instantiate(tempSoundPrefab, worldPos, Quaternion.identity, transform);
        TemporarySoundSource tempSoundSource = soundSource.GetComponent<TemporarySoundSource>();
        tempSoundSource.SetAudioClip(audioClipReference.audioClip);
        tempSoundSource.SetVolume(audioClipReference.volume);
        tempSoundSource.PlaySound();
    }
}
