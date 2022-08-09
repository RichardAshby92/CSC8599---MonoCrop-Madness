using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreDisplay : MonoBehaviour
{
    public GameSceneManager gameSceneManagerObject;

    private TextMeshProUGUI _finalScoreText;
    private TextMeshProUGUI _turnNumScoreText;
    private TextMeshProUGUI _cashNumScoreText;
    private TextMeshProUGUI _communityScoreText;

    private int _finalScore;
    private int _turnNumScore;
    private int _cashNumScore;
    private int _communityNumScore;

    private void Awake()
    {
        _finalScoreText = GetComponent<TextMeshProUGUI>();
        _finalScore = GameSceneManager.FinalScore;
        _turnNumScore = GameSceneManager.TurnNumScore;
        _cashNumScore = GameSceneManager.CashNumScore;
        _communityNumScore = GameSceneManager.CommunityNumScore;
    }

    private void Start()
    {
        _finalScoreText.text = "Final Score: " + _finalScore.ToString();
        _turnNumScoreText.text = "Turn Number Score: " + _turnNumScore.ToString();
        _cashNumScoreText.text = "Cash Score: " + _cashNumScore.ToString();
        _communityScoreText.text = "Community Score: " + _communityNumScore.ToString();
    }
}
