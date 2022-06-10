using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreDisplay : MonoBehaviour
{
    public GameSceneManager gameSceneManagerObject;

    private TextMeshProUGUI scoreText;
    private int score;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        score = GameSceneManager.finalScore;
    }

    private void Start()
    {
        print(score);
        scoreText.text = "Final Score: " + score.ToString();
    }
}
