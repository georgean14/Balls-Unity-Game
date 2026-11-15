using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParticlesPlayOnClickButton : MonoBehaviour
{
    // [SerializeField] private Button button;
    [SerializeField] private ParticleSystem clickParticles;
    [SerializeField] private RectTransform particlesParent; // UIParticlesCanvas

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        
    }

    public void PlayEffects()
    {

        // Spawn particles at button position
        ParticleSystem ps = Instantiate(
            clickParticles,
            rect.position,
            Quaternion.identity,
            particlesParent
        );

        ps.Play();
        Destroy(ps.gameObject, 1.5f);

        UICamera.Instance.ShakeTheCamera();
    }
}
