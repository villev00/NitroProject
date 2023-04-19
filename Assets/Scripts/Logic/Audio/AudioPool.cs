using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioPool : MonoBehaviour
{
    public IObjectPool<AudioSource> pool { get; private set; }

    void Awake()
    {
        pool = new ObjectPool<AudioSource>(CreateAudio, OnGetAudio, OnReleaseAudio, OnDestroyAudio);
    }

    public void ReleaseAfter(AudioSource audio, float delay)
    {
        StartCoroutine(ReleaseAfterCo(audio, delay));
    }

    IEnumerator ReleaseAfterCo(AudioSource audio, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        pool.Release(audio);
    }

    AudioSource CreateAudio()
    {
        AudioSource source = new GameObject().AddComponent<AudioSource>();
        source.playOnAwake = false;
        return source;
    }

    void OnGetAudio(AudioSource audio)
    {
        audio.gameObject.SetActive(true);
    }

    void OnReleaseAudio(AudioSource audio)
    {
        audio.Stop();
        audio.gameObject.SetActive(false);
    }

    void OnDestroyAudio(AudioSource audio)
    {
        Destroy(audio.gameObject);
    }
}
