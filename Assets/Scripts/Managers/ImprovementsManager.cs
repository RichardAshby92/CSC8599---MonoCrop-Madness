using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementsManager : MonoBehaviour
{
    public static ImprovementsManager inst;

    [SerializeField]
    private const int numOfImprovements = 10;

    [SerializeField]
    private Improvement[] Improvements;

    private int currentImprovement;
    private GameManager gameManager;

    //References to Buttons


    // Start is called before the first frame update
    void Awake()
    {
        inst = this;

        gameManager = GetComponent<GameManager>();
        currentImprovement = 0;
        Improvements = new Improvement[numOfImprovements];
        //Load Data from CSV?
        //Add Listeners to Buttons
        //Disable all buttons
    }

    public void Improvement()
    {
        //for current improvement
        if(!Improvements[currentImprovement].bIsComplete && currentImprovement != 0)
        {
            Improvements[currentImprovement].turnsRemaining--;
            if(Improvements[currentImprovement].turnsRemaining == 0)
            {
                Improvements[currentImprovement].bIsComplete = true;
                ApplyEffects(currentImprovement);
                //enable next buttons
            }
        }
    }

    public void StartImprovement(int index) //index Passed from Button
    {
        currentImprovement = index;
        gameManager.cash -= Improvements[currentImprovement].IntialCost;
    }

    void ApplyEffects(int index)
    {
        switch(index)
        {
            case 1:
                //Effect
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
struct Improvement
{
    public int turnsRemaining;
    public int IntialCost;
    public bool bIsComplete;
    //Unlock elements
}

