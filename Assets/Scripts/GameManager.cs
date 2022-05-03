using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int turnNum;

    private void Awake()
    {
        //Check Singleton
    }

    void EndTurn()
    {
        turnNum++; //Add Listener for Game Elasped
        TogglePlayerControls();
        //Coroutine while waiting

        CalculateNewWaterLevel();
        CalculateRisks();
        CalculateFieldHealth();
        CalculateCropValues();
        CalculateFinances();
        //Invoke GC
    }

    void TogglePlayerControls()
    {

    }
    void CalculateNewWaterLevel()
    {
        //Set Lake Height
    }

    void CalculateRisks()
    {

    }

    void CalculateFieldHealth()
    {

    }

    void CalculateCropValues()
    {

    }

    void CalculateFinances()
    {

    }
}
