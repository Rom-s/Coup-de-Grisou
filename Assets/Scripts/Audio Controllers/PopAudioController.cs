using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] popSounds;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        int index = Random.Range(0, popSounds.Length);
        _audioSource.PlayOneShot(popSounds[index]);
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}