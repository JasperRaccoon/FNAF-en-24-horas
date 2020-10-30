using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    Dictionary<Sound, AudioSource> sources = new Dictionary<Sound, AudioSource>();

    void Awake()
    {
        SoundSetup(sounds);
    }
    void SoundSetup(Sound[] soundArray)
    {
        foreach (Sound s in soundArray)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;
            sources.Add(s, source);
        }
    }
    public AudioSource FindSource(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == soundName);
        return sources[s];
    }
    public void Play(string soundName)
    {
        FindSource(soundName).Play();
    }
    public void Stop(string soundName)
    {
        FindSource(soundName).Stop();
    }
    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            if (sources[s].isPlaying)
            {
                sources[s].Stop();
            }
        }
    }
    public void Pause(string soundName)
    {
        FindSource(soundName).Pause();
    }
    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            if (sources[s].isPlaying)
            {
                sources[s].Pause();
            }
        }
    }
    public void Unpause(string soundName)
    {
        FindSource(soundName).UnPause();
    }
}
