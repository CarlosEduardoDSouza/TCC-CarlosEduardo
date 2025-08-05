using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClickend);
    }

    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    void OnExitButtonClickend()
    {
        Application.Quit();
    }
}
