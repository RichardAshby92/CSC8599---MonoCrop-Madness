using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementsManager : MonoBehaviour
{
    public static ImprovementsManager inst;

    [SerializeField]
    private const int numOfImprovements = 10;

    [SerializeField]
    private TextAsset ImprovementData;

    [SerializeField]
    private Improvement[] Improvements;

    private int currentImprovement;
    private GameManager gameManager;

    //References to Buttons


    void Awake()
    {
        inst = this;

        gameManager = GetComponent<GameManager>();
        currentImprovement = 0;
        Improvements = LoadData.LoadImprovementsData(ImprovementData);
    }

    public void ResearchImprovement()
    {
        if(!Improvements[currentImprovement].bIsComplete && currentImprovement != 0)
        {
            Improvements[currentImprovement].turnsRemaining--;
            if(Improvements[currentImprovement].turnsRemaining == 0)
            {
                Improvements[currentImprovement].bIsComplete = true;
                ApplyEffects(currentImprovement);
                //Unlocks
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
public struct Improvement
{
    [field: SerializeField]
    public int id { get; set; }
    [field: SerializeField]
    public string name { get; set; }
    [field: SerializeField]
    public int turnsRemaining { get; set; }
    [field: SerializeField]
    public int IntialCost { get; set; }
    [field: SerializeField]
    public bool bIsComplete { get; set; }
}

