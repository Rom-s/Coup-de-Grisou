using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private AudioClip[] expSounds;
    
    private AudioSource _audioSource;

    private Animator _animator;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        
        _animator.SetTrigger("boum");
        
        int index = Random.Range(0, expSounds.Length);
        _audioSource.PlayOneShot(expSounds[index]);
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}