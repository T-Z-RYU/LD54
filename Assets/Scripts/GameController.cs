using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isGameStart;
    public bool isGameLose;
    public float levelTime;
    public float countDownStartTime = 3.1f;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private AudioSource gameLose;

    private void Awake()
    {
        ResumeGame();
    }

    private void Update()
    {
        countDownStartTime -= Time.deltaTime;
        if(countDownStartTime <= 0)
        {
            isGameStart = true;
        }

        if (isGameStart)
        {
            levelTime -= Time.deltaTime;
        }
        if (levelTime <= 0)
        {
            levelTime = 0;
            if (!isGameLose)
            {
                isGameLose = true;
                Lose();
            }
        }
    }

    public void Win()
    {
        PauseGame();
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        gameLose.Play();
        PauseGame();
        losePanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
