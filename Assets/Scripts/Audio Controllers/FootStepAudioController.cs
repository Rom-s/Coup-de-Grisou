using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootStepAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOne()
    {
        if (!_audioSource.isPlaying)
        {
            int index = Random.Range(0, footsteps.Length);
            _audioSource.PlayOneShot(footsteps[index]);
        }
    }
}