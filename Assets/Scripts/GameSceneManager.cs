using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager inst;

    public static int finalScore;

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
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadEndGame()
    {
        CalculateScore();
        SceneManager.LoadScene("EndGame Scene");
        print(finalScore);
    }

    private void CalculateScore()
    {
        finalScore = gameManager.turnNum + gameManager.cash + communityManager.communityHealth;
        //Community Health + Money (Scaled) + turnNum (Clamped 100) 
    }
}
