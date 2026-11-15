using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarts : MonoBehaviour
{
    private static GameStarts instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        Screen.SetResolution(
            Mathf.RoundToInt(Screen.width * 0.85f),
            Mathf.RoundToInt(Screen.height * 0.85f),
            true
        );

        Time.fixedDeltaTime = 1f / 60f;
        DontDestroyOnLoad(gameObject);
    }
}
