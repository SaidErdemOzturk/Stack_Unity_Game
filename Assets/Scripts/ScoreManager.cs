using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textMyLevel;
    public TMPro.TextMeshProUGUI textNextLevel;
    public Image circleSlider;
    public Button playButton;
    public void AddScore(int score)
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + score);
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }



}
