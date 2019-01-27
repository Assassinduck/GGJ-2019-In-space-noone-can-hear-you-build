using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager_Script : MonoBehaviour
{
    public AudioClip[] MusiClips;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(MusiClips[0]);
        _audioSource.PlayOneShot(MusiClips[0]);
        Invoke("Play", 53f);
    }

    void Play()
    {
        Debug.Log("play music");
        _audioSource.loop = true;;
        _audioSource.clip = MusiClips[1];
        _audioSource.Play();
    }

}
