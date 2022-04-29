using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void TogglePlayerControls()
    {

    }
    void CalculateNewWaterLevel()
    {

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
