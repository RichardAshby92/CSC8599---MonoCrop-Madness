using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public int turnNum;
    public int remainingActions;

    public GameObject[] fields;

    public int rain;
    public int totalWaterUsed;

    public int cash; //Amount
    public int waterLevel; //1 - 100 Percentage
    public int numPollinators; //1 - 100 Percentage
    public int pests; //1 - 100 Percentage

    private void Awake()
    {
        inst = this;
    }

    public void EndTurn()
    {
        turnNum++; //Add Listener for Game Elasped
        //Coroutine while waiting?

        //Loan Repayment
        cash -= 50;
        print(cash);

        CalculateNewWaterLevel();
        CalculateFieldHealth();
        //Invoke GC
    }

    void CalculateNewWaterLevel()
    {
        totalWaterUsed = fields[0].GetComponent<FieldProperties>().waterUsed + fields[1].GetComponent<FieldProperties>().waterUsed +
            fields[2].GetComponent<FieldProperties>().waterUsed + fields[3].GetComponent<FieldProperties>().waterUsed;
        waterLevel += rain - totalWaterUsed;
        //Set Lake Height
    }

    void CalculateFieldHealth()
    {
        fields[0].GetComponent<FieldProperties>().CalculateFieldHealth();
    }

}
