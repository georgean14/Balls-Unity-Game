using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour {
    
    public static MainMenuSoundManager Instance;

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---------- Audio Clips ----------")]
    [SerializeField] private AudioClip mainMenuBackgroundMusic;
    [SerializeField] private AudioClip buttonClick;

    private void Awake(){
        Instance=this;
    }

    private void Start(){

        musicSource.clip = mainMenuBackgroundMusic;
        musicSource.loop=true;
        musicSource.Play();
    }

    public void ButtonClick(){
        Debug.Log("playersound");   
        SFXSource.clip = buttonClick;
        SFXSource.Play();
    }



}
