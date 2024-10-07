using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource _audioSource;

    public AudioClip coinAudio;

    public AudioClip hurtAudio;

    void Awake()
    {
        if(instance != null && instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    /*public void CoinSFX()
    {
        _audioSource.PlayOneShot.(_coinAudio);
    }*/

    public void PlaySFX(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}