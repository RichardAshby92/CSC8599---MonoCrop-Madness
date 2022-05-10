using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public int fertilizer = 0;
    public int pesticide = 0;

    public bool isThereFertilizer = false;
    public bool[] tools;

    public void SubtractFromInventory(int type)
    {
        switch (type)
        {
            case 0:
                fertilizer--;
                break;
            case 1:
                pesticide--;
                break;
            default:
                break;
        }

        if(fertilizer <= 0)
        {
            isThereFertilizer = false;
        }
    }

    public void AddToInvetory(int type)
    {
        switch (type)
        {
            case 0:
                fertilizer++;
                break;
            case 1:
                pesticide++;
                break;
            default:
                break;
        }

        if(fertilizer > 0)
        {
            isThereFertilizer = true;
        }
    }
}
