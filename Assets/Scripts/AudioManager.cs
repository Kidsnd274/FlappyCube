using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject); // Persistent between scenes

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start() {
        Play("BGM");
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, (s) => s.name == name);
        if (s == null) {
            Debug.LogWarning("Can't find sound with name " + name);
            return;
        }
        s.source.Play();
    }
}
