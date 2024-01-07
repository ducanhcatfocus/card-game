using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    static AudioManager instance;
    public static AudioManager Ins => instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip buffSound;
    [SerializeField] private AudioClip attackSource;
    [SerializeField] private AudioClip negativeBuffSound;
    [SerializeField] private AudioClip blockSound;
    [SerializeField] private AudioClip cardSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip enemyDieSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip loseSound;

    private bool isMuted;

    public bool IsMuted=> isMuted;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBlockSound()
    {
        
            if (musicSource.isPlaying)
                return;
            musicSource.PlayOneShot(blockSound);
    }

    public void PlayAttackSound()
    {
        if (musicSource.isPlaying)
            return;
        musicSource.PlayOneShot(attackSource);
    }

    public void PlayBuffSound()
    {
        
            if (musicSource.isPlaying)
                return;
            musicSource.PlayOneShot(buffSound);
    }

    public void PlayNegativeBuffSound()
    {
        
            if (musicSource.isPlaying)
                return;
            musicSource.PlayOneShot(negativeBuffSound);
    }

    public void PlayCardSound()
    {

        if (musicSource.isPlaying)
            return;
        musicSource.PlayOneShot(cardSound);
    }


    public void PlayClickSound()
    {
        if (musicSource.isPlaying)
            return;
        musicSource.PlayOneShot(clickSound);
    }

    public void PlayEnemyDieSound()
    {
        musicSource.PlayOneShot(enemyDieSound);
    }

    public void PlayWinSound()
    {
        musicSource.PlayOneShot(winSound);
    }

    public void PlayCoinSound()
    {
        musicSource.PlayOneShot(coinSound);
    }

    public void PlayLoseSound()
    {
        musicSource.PlayOneShot(loseSound);
    }
    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            musicSource.volume = 0f;
        }
        else
        {
            musicSource.volume = 1f;
        }
    }




    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}

