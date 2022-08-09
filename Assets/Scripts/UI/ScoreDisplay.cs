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
    private TextMeshProUGUI _tipSummaryText;
    private TextMeshProUGUI _tipTurnText;
    private TextMeshProUGUI _tipCashText;
    private TextMeshProUGUI _tipCommunityText;

    private int _finalScore;
    private int _turnNumScore;
    private int _cashNumScore;
    private int _communityNumScore;
    [SerializeField]
    private TextAsset _finalTips;
    private string[,] _finalTipsArray;

    private void Awake()
    {
        _finalScoreText = GetComponent<TextMeshProUGUI>();
        _finalScore = GameSceneManager.FinalScore;
        _turnNumScore = GameSceneManager.TurnNumScore;
        _cashNumScore = GameSceneManager.CashNumScore;
        _communityNumScore = GameSceneManager.CommunityNumScore;

        _finalTipsArray = LoadData.LoadCSVToStringArray(_finalTips);
    }

    private void Start()
    {
        _finalScoreText.text = "Final Score: " + _finalScore.ToString();
        _turnNumScoreText.text = "Turn Number Score: " + _turnNumScore.ToString();
        _cashNumScoreText.text = "Cash Score: " + _cashNumScore.ToString();
        _communityScoreText.text = "Community Score: " + _communityNumScore.ToString();


    }

    private void SelectFinalTip()
    {
        //Final Summary
        if(_finalScore > 200)
        {
            //_tipSummaryText.text = _finalTipsArray[];
        }
        if(_finalScore > 100 && _finalScore <= 200)
        {
            //_tipSummaryText.text = _finalTipsArray[];
        }
        else
        {
            //_tipSummaryText.text = _finalTipsArray[];
        }

        //Turn Number Summary
        if (_turnNumScore == GameManager.MaximumTurns)
        {
            //Successfully made it to the end
            //_tipTurnText.text = _finalTipsArray[];
        }
        else
        {
            //try Again
            //_tipTurnText.text = _finalTipsArray[];
        }

        //Community Score Summary
        if(_communityNumScore > 60)
        {
            //you Succeeded in preversing the community around you
            //_tipCommunityText.text = _finaltipsArray[];
        }
        else
        {
            //you sacrfised to community to keep yourselves afloat
            //_tipCommunityText.text = _finaltipsArray[];
        }

        //Cash Score Summary
        if(_cashNumScore > 80)
        {
            //You were able to make money
            //_tipCashText.text = _finaltipsArray[];
        }
        if (_cashNumScore == 0)
        {
            //Bankruptcy Text
            //_tipCashText.text = _finaltipsArray[];
        }
        else
        {
            //you managed to keep yourself afloat
            //_tipCashText.text = _fianltipsArray[];
        }
    }      
}
