using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PiocheAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] piocheSounds;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOne()
    {
        if (!_audioSource.isPlaying)
        {
            int index = Random.Range(0, piocheSounds.Length);
            _audioSource.PlayOneShot(piocheSounds[index]);
        }
    }
}