﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PiouAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] piouChillSounds;
    
    [SerializeField] private AudioClip piouLowPanickedSound;
    
    [SerializeField] private AudioClip piouHighPanickedSound;

    [SerializeField] private float piouProbability;
    
    private AudioSource _audioSource;

    private bool panicked = false;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_audioSource.isPlaying && panicked)
        {
            float p = Random.Range(0f, 1f);
            if (p < piouProbability)
            {
                int index = Random.Range(0, piouChillSounds.Length);
                _audioSource.PlayOneShot(piouChillSounds[index]);
            }
        }
    }


    public void PlayLowPanicked()
    {
        panicked = true;
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(piouLowPanickedSound);
        }
    }

    public void PlayHighPanicked()
    {
        panicked = true;
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(piouHighPanickedSound);
        }
        
    }

    public void Panick()
    {
        panicked = true;
    }
    public void NoPanick()
    {
        panicked = false;
    }
}