using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinGridManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform gridParent;

    [Header("Skins")]
    [SerializeField] private List<GameObject> skinPrefabs;   // Your full gameplay skin prefabs
    [SerializeField] private GameObject lockedPrefab;

    private void Start()
    {
        PopulateGrid();
    }

    public void PopulateGrid()
    {
        for (int i = 0; i < skinPrefabs.Count; i++)
        {
            GameObject slot = Instantiate(skinPrefabs[i], gridParent);

            SkinsButtonPress skinScript = slot.GetComponent<SkinsButtonPress>();
            if (skinScript != null) skinScript.SetIndex(i);

            if (CosmeticsManager.Instance.lockStatusSkins[i])
            {
                Image skinImage = slot.transform.GetChild(0).GetComponent<Image>();
                if (skinImage != null)
                {
                    Color dimmedColor = skinImage.color;
                    dimmedColor.a = 0.1f; // Adjust this value (0-1) for how dimmed you want it
                    skinImage.color = dimmedColor;
                    // Color originalColor = skinImage.color;
                    // float gray = originalColor.r * 0.299f + originalColor.g * 0.587f + originalColor.b * 0.114f;
                    // skinImage.color = new Color(gray, gray, gray, originalColor.a);
                    skinImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Gray tint
                }
                else
                {
                    Debug.Log("its null");
                }
                GameObject lockOverlay = Instantiate(lockedPrefab, slot.transform);
                // Optional: position it properly if needed
                RectTransform lockRect = lockOverlay.GetComponent<RectTransform>();
                if (lockRect != null)
                {
                    lockRect.anchorMin = Vector2.zero;
                    lockRect.anchorMax = Vector2.one;
                    lockRect.offsetMin = Vector2.zero;
                    lockRect.offsetMax = Vector2.zero;
                }
            }
        }
    }
    public void ClearGridLayout()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
