using UnityEngine;
using TMPro;

public class DamagePopUpManager : MonoBehaviour
{
    public static DamagePopUpManager Instance{ get; private set; }

    [SerializeField] private GameObject popUpText;


    private void Awake(){
        Instance = this;
    }

    public void spawnText(int score, Vector3 spawnPos)
    {
        // Debug.Log("tryna spawn som text");
        string scoreString=score.ToString();
        GameObject newThing = Instantiate(popUpText, spawnPos, Quaternion.identity);

        TextMeshPro _text = newThing.GetComponentInChildren<TextMeshPro>();

        _text.text = scoreString;


        Destroy(newThing, 2f);
    }
}
