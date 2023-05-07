using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Slider topSlider;
    public Image speedAbility;
    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI myLevel;
    public TMPro.TextMeshProUGUI nextLevel;
    public Button tapToPlay;
    public Button restartGame;
    public Button nextLevelButton;

    private void Start()
    {
        myLevel.text = PlayerPrefs.GetInt("Level").ToString();
        nextLevel.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        topSlider.maxValue = PlayerPrefs.GetInt("Level") + 20;
        topSlider.value = 0;
    }

    public void UpdateSpeedAbility(float currentTime)
    {
        speedAbility.fillAmount = currentTime;
    }


    public void UpdateTopArea()
    {
        topSlider.value++;
        score.text = PlayerPrefs.GetInt("Score").ToString();
    }

}
