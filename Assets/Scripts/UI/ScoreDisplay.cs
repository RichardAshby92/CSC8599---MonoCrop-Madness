using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _finalScoreText;
    [SerializeField]
    private TextMeshProUGUI _turnNumScoreText;
    [SerializeField]
    private TextMeshProUGUI _cashNumScoreText;
    [SerializeField]
    private TextMeshProUGUI _communityScoreText;
    [SerializeField]
    private TextMeshProUGUI _tipSummaryText;
    [SerializeField]
    private TextMeshProUGUI _tipTurnText;
    [SerializeField]
    private TextMeshProUGUI _tipCashText;
    [SerializeField]
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
        _finalScore = GameSceneManager.S_FinalScore;
        _turnNumScore = GameSceneManager.S_TurnNumScore;
        _cashNumScore = GameSceneManager.S_CashNumScore;
        _communityNumScore = GameSceneManager.S_CommunityNumScore;

        _finalTipsArray = LoadData.LoadCSVToStringArray(_finalTips);
    }

    private void Start()
    {
        _finalScoreText.text = "Final Score: " + _finalScore.ToString();
        _turnNumScoreText.text = "Turn Number Score: " + _turnNumScore.ToString();
        _cashNumScoreText.text = "Cash Score: " + _cashNumScore.ToString();
        _communityScoreText.text = "Community Score: " + _communityNumScore.ToString();

        SelectFinalTip();
    }

    private void SelectFinalTip()
    {
        //Final Summary
        if(_finalScore > 200)
        {
            _tipSummaryText.text = _finalTipsArray[1, 0];
        }
        if(_finalScore > 100 && _finalScore <= 200)
        {
            _tipSummaryText.text = _finalTipsArray[2, 0];
        }
        else
        {
            _tipSummaryText.text = _finalTipsArray[3, 0];
        }

        //Turn Number Summary
        if (_turnNumScore == GameManager.MaximumTurns)
        {
            //Successfully made it to the end
            _tipTurnText.text = _finalTipsArray[1, 1];
        }
        else
        {
            //try Again
            _tipTurnText.text = _finalTipsArray[2, 1];
        }

        //Community Score Summary
        if(_communityNumScore > 60)
        {
            //you Succeeded in preversing the community around you
            _tipCommunityText.text = _finalTipsArray[1, 2];
        }
        else
        {
            //you sacrfised to community to keep yourselves afloat
            _tipCommunityText.text = _finalTipsArray[2, 2];
        }

        //Cash Score Summary
        if(_cashNumScore > 80)
        {
            //You were able to make money
            _tipCashText.text = _finalTipsArray[1, 3];
        }
        if (_cashNumScore == 0)
        {
            //Bankruptcy Text
            _tipCashText.text = _finalTipsArray[2, 3];
        }
        else
        {
            //you managed to keep yourself afloat
            _tipCashText.text = _finalTipsArray[3, 3];
        }
    }      
}
