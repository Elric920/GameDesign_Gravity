using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volumn;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    // Start is called before the first frame update
    public void Play(string name)
    {
        Sound s = FindSound(name);
        if(s == null) return;
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = FindSound(name);
        if(s == null) return;
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = FindSound(name);
        if(s == null) return;
        s.source.Stop();
    }

    public bool isPlaying(string name)
    {
        Sound s = FindSound(name);
        if(s == null) return false;
        return s.source.isPlaying;
    }

    Sound FindSound(string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("The audio source " + name + " is not found.");
            return null;
        }
        else return s;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
