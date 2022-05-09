using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    void Awake()
    {

    }

    public void LoadEndGame()
    {
        //Calculate Score
        SceneManager.LoadScene("EndGame Scene");
    }
}
