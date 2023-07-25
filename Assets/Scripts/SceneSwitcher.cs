using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public Text timerText;
    private float timeRemaining = 120f;
    private bool timerIsRunning = true;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                SwitchScene();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }

    void SwitchScene()
    {
        SceneManager.LoadScene("NombreDeLaEscena"); // Reemplaza "NombreDeLaEscena" con el nombre de la escena a la que deseas cambiar
    }
}