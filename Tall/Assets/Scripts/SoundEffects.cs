using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static void PlayImpactSFX() => instance.PlayAudio();

    private static SoundEffects instance;

    [System.Serializable]
    private struct AudioArray
    {
        public AudioClip[] clips;
        public void PlayRandom(AudioSource source)
        {
            int clipIndex = Random.Range(0, clips.Length);
            source.pitch = Random.Range(.95f, 1.05f);
            source.loop = false;
            source.clip = clips[clipIndex];
            source.PlayDelayed(Random.Range(0.0f, .01f));
        }
    }

    [SerializeField] private AudioSource[] sources;
    [SerializeField] private AudioArray[] arrays;

    private void Awake()
    {
        instance = this;
    }


    private void PlayAudio()
    {
        for (int i = 0; i < arrays.Length; i++)
        {
            arrays[i].PlayRandom(sources[i]);
        }
    }
}
