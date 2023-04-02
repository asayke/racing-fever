using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EliminationEndScreen : MonoBehaviour
{
    [SerializeField] private Text _resultLabel;
    [SerializeField] private Text _descLabel;

    public void FillScreen(bool result, int countLaps)
    {
        if (result)
        {
            _resultLabel.text = "Победа!";
            _resultLabel.color = new Color(1, 10/255f, 1);
            _descLabel.text = "Поздравялем, вы выиграли!";
        }
        else
        {
            _resultLabel.text = "Поражение!";
            _resultLabel.color = new Color(50/255f, 50/255f, 1);
            _descLabel.text = "Вы выбыли на круге "+countLaps+" :(";
        }
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        FindObjectOfType<GameSetup>().Restart();
    }
    
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
