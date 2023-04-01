using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _elemPrefab;
    [SerializeField] private GameObject container;

    public void FillScreen(List<Car> finishedCars)
    {
        for (int i = 0; i < finishedCars.Count; i++)
        {
            var elem = Instantiate(_elemPrefab, container.transform).GetComponent<EndScreenElem>();
            elem.Name.text = finishedCars[i].Name;
            elem.Position.text = (i + 1) + ". ";
            elem.Time.text = finishedCars[i].CarLapInfo.LapTimes.Sum().GetTimeString();

            if (finishedCars[i] is PrometeoCarController)
            {
                elem.Name.color = new Color(145 / 255f, 1, 1);
                elem.Position.color = new Color(145 / 255f, 1, 1);
                elem.Time.color = new Color(145 / 255f, 1, 1);
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
    }
    
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
