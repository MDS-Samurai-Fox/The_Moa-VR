using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }

    public Transform moa;

    public Sound[] sounds;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake () {

        // Check if there is another instance of the same type and destroy it
        if (Instance != null & Instance != this) {
            Destroy (gameObject);
        }

        Instance = this;

        DontDestroyOnLoad (gameObject);

        // Add the audio sources
        foreach (Sound s in sounds) {

            if (s.name == "Footstep") {

                s.source = moa.gameObject.AddComponent<AudioSource> ();
                s.source.spatialBlend = 0.6f;

            } else if (s.name == "Screech") {

                s.source = moa.gameObject.AddComponent<AudioSource> ();
                s.source.spatialBlend = 0.35f;

            } else {

                s.source = gameObject.AddComponent<AudioSource> ();

            }

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }

    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {

        Play ("Background");

    }

    public void PlayWithRandomPitch (string name, float min, float max) {

        Sound s = Array.Find (sounds, sound => sound.name == name);

        if (s == null) {
            return;
        }

        s.RandomizePitch (min, max);
        s.source.Play ();

    }

    public void Play (string name) {

        Sound s = Array.Find (sounds, sound => sound.name == name);

        if (s == null) {
            return;
        }

        s.source.Play ();

    }

#if UNITY_EDITOR
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate () {

        foreach (Sound s in sounds) {

            if (s.source != null) {

                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

            }

        }

    }
#endif

}