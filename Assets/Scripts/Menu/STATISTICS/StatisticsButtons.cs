using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatisticsButtons : MonoBehaviour
{
    [SerializeField] private Button _back;

    private void Awake() => _back.onClick.AddListener(Back);

    private void Back() => SceneManager.LoadScene("MainMenu");
}