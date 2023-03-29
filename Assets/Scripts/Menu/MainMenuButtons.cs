using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;

    private void Awake()
    {
        _play.onClick.AddListener(Play);
        _exit.onClick.AddListener(Exit);
    }

    private void Play() => SceneManager.LoadScene("SelectMode");

    private void Exit() => Application.Quit();
}
