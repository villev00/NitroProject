using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public static class AudioManager
{
    public static AudioMixer Mixer = Resources.Load<AudioMixer>("GameAudioMixer");

    static AudioPool audioPool;

    public static AudioSource PlaySound(AudioClip audioClip, bool changePitch, bool looping = false, float minPitch = 0.9f, float maxPitch = 1.1f)
    {
        if (audioPool == null)
        {
            audioPool = new GameObject("Audio Pool").AddComponent<AudioPool>();
        }

        AudioSource soundSource = audioPool.pool.Get();
        // Find AudioMixerGroup you want to load
        AudioMixerGroup[] audioMixGroup = Mixer.FindMatchingGroups("Master/Sound");
        // Assign the AudioMixerGroup to AudioSource (Use first index)
        soundSource.outputAudioMixerGroup = audioMixGroup[0];
        if (changePitch)
        {
            soundSource.pitch = Random.Range(minPitch, maxPitch);
            soundSource.volume = Random.Range(0.9f, 1.0f);
        }
        else
        {
            soundSource.pitch = 1;
            soundSource.volume = 1;
        }

        soundSource.clip = audioClip;
        soundSource.loop = looping;
        soundSource.Play();

        if (!looping)
        {
            audioPool.ReleaseAfter(soundSource, audioClip.length);
        }

        return soundSource;
    }

    public static AudioSource PlayMusic(AudioClip audioClip, bool looping = true)
    {
        if (audioPool == null)
        {
            audioPool = new GameObject("Audio Pool").AddComponent<AudioPool>();
        }

        AudioSource musicSource = audioPool.pool.Get();
        musicSource.gameObject.name = "Music/" + audioClip.name;

        // Find AudioMixerGroup you want to load
        AudioMixerGroup[] audioMixGroup = Mixer.FindMatchingGroups("Master/Music");
        // Assign the AudioMixerGroup to AudioSource (Use first index)
        musicSource.outputAudioMixerGroup = audioMixGroup[0];
        musicSource.clip = audioClip;
        musicSource.loop = looping;

        musicSource.volume = 1f;
        musicSource.pitch = 1;

        musicSource.Play();

        if (!looping)
        {
            audioPool.ReleaseAfter(musicSource, audioClip.length);
        }

        return musicSource;
    }
}
