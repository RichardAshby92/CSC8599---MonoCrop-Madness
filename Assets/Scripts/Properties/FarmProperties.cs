using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmProperties : MonoBehaviour
{
    public int cash; //Amount
    public int waterLevel; //1 - 100 Percentage
    public int numPollinators; //1 - 100 Percentage
    public int pests; //1 - 100 Percentage
    public int diseaseRisk; //1 - 100 Percentage
    public int economyRisk; //1 - 100 Percentage
    //Seasonal Enum?

    public int numActions;

    private void Awake()
    {

    }
}
