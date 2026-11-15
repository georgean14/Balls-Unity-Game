using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class CosmeticsManager : MonoBehaviour
{
    public static CosmeticsManager Instance{ get; private set; }

    [SerializeField] private List<GameObject> skinsUI;
    public List<bool> lockStatusSkins; //true is locked, false is unlocked

    public List<string> textsOfSkins= new List<string>{
        "do not fear the downhill path, gravity teaches us that falling can also be progress",
        "where there is light, there will always be shadow",
        "decay is inevitable, measured by half-life and governed by the quiet logic of the atomic nucleus",
        "the sharpest edge is not in the blades, but in knowing where the strike will fall before the world moves"
        };
    public List<int> costOfSkins= new List<int>{0, 5000, 3000, 100000};

    public event Action<int> OnSkinChanged;

    // private GameObject activeSkin;
    private const string PLAYER_PREFS_CURRENT_SKIN = "CurrentSkin";
    private const string PLAYER_PREFS_LOCKED_SKINS = "LockedSkins";


    private int currentSkinIdx=0;
    private int currentUnlockedSkinIdx;

    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        currentSkinIdx=PlayerPrefs.GetInt(PLAYER_PREFS_CURRENT_SKIN, 0);
        LoadSkinsStatus();
        Debug.Log(lockStatusSkins);
    }

    public void SelectSkin(int idx)
    {
        if (idx < 0 || idx >= skinsUI.Count) Debug.Log("big error index is out of range");
        // Debug.Log("selecting");

        currentSkinIdx = idx;
        PlayerPrefs.SetInt(PLAYER_PREFS_CURRENT_SKIN, currentSkinIdx);
        OnSkinChanged?.Invoke(currentSkinIdx);
    }

    public int getSkinIdx()
    {
        return currentSkinIdx;
    }

    public void setCurrentUnlockedSkinIdx(int i)
    {
        currentUnlockedSkinIdx=i;
    }

    public void SaveSkins()
    {
        string skinData = "";
        foreach (bool unlocked in lockStatusSkins)
        {
            skinData += unlocked ? "1" : "0";
        }
        PlayerPrefs.SetString(PLAYER_PREFS_LOCKED_SKINS, skinData);
        PlayerPrefs.Save();
    }
    public void LoadSkinsStatus()
    {
        string skinData = PlayerPrefs.GetString(PLAYER_PREFS_LOCKED_SKINS, "0111");
        
        lockStatusSkins.Clear();
        foreach (char c in skinData)
        {
            lockStatusSkins.Add(c == '1');
        }
    }
    public void UnlockSkin()
    {
        int i=currentUnlockedSkinIdx;
        if(!lockStatusSkins[i]) return;
        lockStatusSkins[i]=false;
        SaveSkins();
        SelectSkin(i);
    }
}
