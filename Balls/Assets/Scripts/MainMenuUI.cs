using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button cosmeticsButton;

    private void Awake(){
        playButton.onClick.AddListener(() => {
            MainMenuSoundManager.Instance.ButtonClick();
            SceneManager.LoadScene(1);
        });

        cosmeticsButton.onClick.AddListener(() =>
        {
            MainMenuSoundManager.Instance.ButtonClick();
            SceneManager.LoadScene(2);
        });
    }

}
