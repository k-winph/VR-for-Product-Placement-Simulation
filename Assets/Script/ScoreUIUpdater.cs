using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + ScoreManager.totalScore;
    }
}
