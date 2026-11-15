using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsMenuSoundManager : MonoBehaviour
{
    public static CosmeticsMenuSoundManager Instance{get; private set;}

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---------- Audio Clips ----------")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip popSFX;
    [SerializeField] private AudioClip chainsSFX;


    private void Awake()
    {
        Instance=this;
    }
    private void Start()
    {
        musicSource.loop=true;
        musicSource.clip=backgroundMusic;
        musicSource.Play();
    }

    public void PlayPopSFX()
    {
        SFXSource.clip=popSFX;
        SFXSource.Play();
    }
    public void PlayChainSFX()
    {
        SFXSource.clip=chainsSFX;
        SFXSource.Play();
    }
}
