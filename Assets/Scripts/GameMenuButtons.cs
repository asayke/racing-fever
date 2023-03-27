using UnityEngine;
using UnityEngine.UI;

public class GameMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;

    private void Awake()
    {
        _play.onClick.AddListener(Play);
        _exit.onClick.AddListener(Exit);
    }

    private void Update()
    {
        if (SimpleInput.GetKeyDown(KeyCode.Escape))
        {
            _window.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void Play()
    {
        _window.SetActive(false);
        Time.timeScale = 1;
    }

    private void Exit() => Application.Quit();
}
