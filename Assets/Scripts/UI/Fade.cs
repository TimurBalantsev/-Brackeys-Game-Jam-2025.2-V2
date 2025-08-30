using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade Instance { get; private set; }

    [SerializeField] private Image image;

    private Coroutine fadeRoutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple Fade UI instances in scene");
        }
    }

    public void FadeIn(float duration, Action callback = null)
    {
        StartFade(0f, 1f, duration, callback);
    }

    public void FadeOut(float duration, Action callback = null)
    {
        StartFade(1f, 0f, duration, callback);
    }

    private void StartFade(float from, float to, float duration, Action callback)
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(FadeRoutine(from, to, duration, callback));
    }

    private IEnumerator FadeRoutine(float from, float to, float duration, Action callback)
    {
        PersistantCanvas.Instance.SetBlocking(true);

        float time = 0f;
        Color color = image.color;
        color.a = from;
        image.color = color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            color.a = Mathf.Lerp(from, to, t);
            image.color = color;
            yield return null;
        }

        fadeRoutine = null;

        PersistantCanvas.Instance.SetBlocking(false);

        callback?.Invoke();
    }
}
