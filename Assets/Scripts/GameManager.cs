using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SoundManager soundManager; 
    private ScoreManager scoreManager;
    private CanvasController canvasController;
    private void Awake()
    {
        soundManager=FindObjectOfType<SoundManager>();
        scoreManager=FindObjectOfType<ScoreManager>();
        canvasController = FindObjectOfType<CanvasController>();

    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLevel()
    {
        Time.timeScale = 0.0F;
        scoreManager.ResetScore();
        soundManager.Play(AudioType.Lose);
        canvasController.restartGame.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartLevel()
    {
        Time.timeScale = 1F;
        FindObjectOfType<BallController>().GetComponent<Rigidbody>().isKinematic = false;
        canvasController.tapToPlay.gameObject.SetActive(false);
    }

    public void NextObstacle(float currentTime)
    {
        scoreManager.AddScore(20);
        canvasController.UpdateTopArea();
        canvasController.UpdateSpeedAbility(currentTime);
        soundManager.Play(AudioType.Destroy);
    }

    public void WinLevel()
    {
        soundManager.Play(AudioType.Win);
        Time.timeScale = 0.0F;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        canvasController.nextLevelButton.gameObject.SetActive(true);

    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
