using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu()]
public class AudioClipSO : ScriptableObject
{
    [Serializable]
    public class AudioClipReference
    {
        [SerializeField] public AudioClip audioClip;
        [SerializeField][Range(0,1)] public float volume = 1f;
    }
    public AudioClipReference[] audioClipReferences;

    public AudioClipReference GetRandomAudioClipReference()
    {
        return audioClipReferences[Random.Range(0, audioClipReferences.Length)];
    }
}
