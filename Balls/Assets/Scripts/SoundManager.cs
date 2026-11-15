using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance{ get; private set; }
    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---------- Audio Clips ----------")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip hitRedEnemy;
    [SerializeField] private AudioClip hitYellowEnemy;
    [SerializeField] private AudioClip died;
    [SerializeField] private AudioClip buttonClicked;
    [SerializeField] private AudioClip popSFX;
    [SerializeField] private AudioClip smallExplosionSFX;
    
    [SerializeField] private Player player;

    [SerializeField] private GameManager gameManager;

    // private float loopStart = 0f; // time in seconds where loop begins
    // private float loopEnd = 17.4f;   // time in seconds where loop ends
    private void Awake()
    {
        Instance=this;
    }
    private void Start(){
        player.OnCollideWithEnemy += Player_OnCollideWithEnemy;
        gameManager.OnDied += GameManager_OnDied;

        // Start background music at loopStart
        // if (backgroundMusic != null) {
        //     musicSource.clip = backgroundMusic;
        //     musicSource.time = loopStart;
        //     musicSource.Play();
        // }
        musicSource.clip=backgroundMusic;
        musicSource.loop=true;
        musicSource.volume=0.5f;
        musicSource.Play();
    }

    // private void Update() {
    //     // Manual loop for background music
    //     if (musicSource.isPlaying && musicSource.time >= loopEnd) {
    //         musicSource.time = loopStart;
    //     }
    // }

    private void Player_OnCollideWithEnemy(object sender, EventArgs e){
        SFXSource.clip = hitRedEnemy;
        SFXSource.volume=1f;
        SFXSource.Play();
    }

    private void GameManager_OnDied(object sender, EventArgs e){
        SFXSource.clip = died;
        SFXSource.volume=0.7f;
        SFXSource.Play();
        StartCoroutine(MusicFadeOut());
    }

    private IEnumerator MusicFadeOut(){
        float startVolume = musicSource.volume;
        float fadeDuration = 1.0f;
        while (musicSource.volume > 0) {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;

            yield return null;
        }
        musicSource.Stop();

        musicSource.volume=startVolume;
        
    }

    public void PlayButtonClick()
    {
        SFXSource.clip=buttonClicked;
        SFXSource.volume=1f;
        SFXSource.Play();
    }
    public void PlayPopSound()
    {
        SFXSource.clip=popSFX;
        SFXSource.volume=1f;
        SFXSource.Play();
    }
    public void PlaySmallExplosion()
    {
        SFXSource.clip=smallExplosionSFX;
        SFXSource.volume=1f;
        SFXSource.Play();
    }

}
