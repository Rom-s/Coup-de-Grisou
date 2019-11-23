using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoiceAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] suffoquementSounds;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOne()
    {
        if (!_audioSource.isPlaying)
        {
            int index = Random.Range(0, suffoquementSounds.Length);
            _audioSource.PlayOneShot(suffoquementSounds[index]);
        }
    }
}