using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    void Awake()
    {
        var bankrupt = GetComponent<GameManager>().onBankrupt;
        bankrupt.AddListener(LoadEndGame);
        var turnsElasped = GetComponent<GameManager>().onTurnsElasped;
        turnsElasped.AddListener(LoadEndGame);
    }

    void LoadEndGame()
    {
        //Calculate Score
        SceneManager.LoadScene("EndGame Scene");
    }
}
