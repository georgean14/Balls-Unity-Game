using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticsUI : MonoBehaviour
{
    [SerializeField] private Button backToHomeButton;

    private void Awake(){
        backToHomeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
    }

}
