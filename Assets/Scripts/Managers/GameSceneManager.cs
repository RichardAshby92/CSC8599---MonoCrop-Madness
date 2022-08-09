using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager inst;

    public static int FinalScore;
    public static int TurnNumScore;
    public static int CashNumScore;
    public static int CommunityNumScore;

    private GameManager gameManager;
    private CommunityManager communityManager;

    void Awake()
    {
        inst = this;
        gameManager = GetComponent<GameManager>();
        communityManager = GetComponent<CommunityManager>();
    }

    public void StartGame()
    {
        FinalScore = 0;
        TurnNumScore = 0;
        CashNumScore = 0;
        CommunityNumScore = 0;
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadEndGame()
    {
        CalculateScore();
        SceneManager.LoadScene("EndGame Scene");
    }

    private void CalculateScore()
    {
        TurnNumScore = Mathf.Clamp(gameManager.turnNum, 0, 100);
        CashNumScore = gameManager.cash; //Add Scaling Function;
        CommunityNumScore = communityManager.communityHealth;

        FinalScore = TurnNumScore + CashNumScore + CommunityNumScore;
        //Community Health + Money (Scaled) + turnNum (Clamped 100) 
    }
}
