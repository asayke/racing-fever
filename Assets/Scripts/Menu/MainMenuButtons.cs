using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _statistics;
    
    private void Awake()
    {
        _play.onClick.AddListener(Play);
        _exit.onClick.AddListener(Exit);
        _statistics.onClick.AddListener(Statistics);
    }

    private void Play() => SceneManager.LoadScene("SelectMode");

    private void Statistics() => Debug.Log("statistic");
    
    private void Exit() => Application.Quit();
}