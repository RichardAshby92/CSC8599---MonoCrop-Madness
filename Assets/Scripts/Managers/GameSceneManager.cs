using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager S_inst;

    public static int S_FinalScore;
    public static int S_TurnNumScore;
    public static int S_CashNumScore;
    public static int S_CommunityNumScore;

    private GameManager _gameManager;
    private CommunityManager _communityManager;

    void Awake()
    {
        S_inst = this;
        _gameManager = GetComponent<GameManager>();
        _communityManager = GetComponent<CommunityManager>();
    }

    public void StartGame()
    {
        S_FinalScore = 0;
        S_TurnNumScore = 0;
        S_CashNumScore = 0;
        S_CommunityNumScore = 0;
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadEndGame()
    {
        CalculateScore();
        SceneManager.LoadScene("EndGame Scene");
    }

    private void CalculateScore()
    {
        S_TurnNumScore = Mathf.Clamp(_gameManager.TurnNum, 0, 100);
        S_CashNumScore = _gameManager.Cash; //Add Scaling Function;
        S_CommunityNumScore = _communityManager.CommunityHealth;

        S_FinalScore = S_TurnNumScore + S_CashNumScore + S_CommunityNumScore;
        //Community Health + Money (Scaled) + turnNum (Clamped 100) 
    }
}
