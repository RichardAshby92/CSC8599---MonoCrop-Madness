using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private int finalScore;

    void Awake()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadEndGame()
    {
        CalculateScore();
        SceneManager.LoadScene("EndGame Scene");
    }

    private void CalculateScore()
    {
        //Community Health + Money (Scaled) + turnNum (Clamped 100) 
    }
}
