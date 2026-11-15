using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChanging : MonoBehaviour
{



    private void Start()
    {
        ScoreAndMultiplierManager.Instance.OnScoreChange += ScoreManager_OnScoreChange;
    }




    private void ScoreManager_OnScoreChange(int currentScore)
    {

        //from 100k to 200k theme changes
        if (currentScore > 10000)
        {
            
        }
    }
}
