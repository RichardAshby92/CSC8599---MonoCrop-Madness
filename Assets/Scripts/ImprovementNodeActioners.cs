using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementNodeActioners : MonoBehaviour
{
    static Dictionary<int, float> MULTIPLIERS = new Dictionary<int, float>();
    static Dictionary<int, System.Action> ACTIONERS = new Dictionary<int, System.Action>()
    {
        { 0, () =>
            {
                //Unlock Cash Crops
                GameManager.UnlockedCrops[1] = true;
                GameManager.UnlockedCrops[2] = true;
                GameManager.UnlockedCrops[3] = true;
            }
        },
        {1, () =>
            {
                //Improve Crop Prices
                MULTIPLIERS[2] = 1.15f;
            }
        },
        {2, () =>
            {
                //Enables Slash and Burn Mechanic
            }

        },
        {3, () =>
            {
                //Improve Crop Prices
                MULTIPLIERS[2] = 1.25f;
            }
        },
        {4, () =>
            {
                //Unlock Food Crops
                GameManager.UnlockedCrops[4] = true;
                GameManager.UnlockedCrops[6] = true;
                GameManager.UnlockedCrops[10] = true;
            }
        },
        {5, () =>
            {
                //Unlock Drought Crops
                GameManager.UnlockedCrops[2] = true;
                GameManager.UnlockedCrops[7] = true;
            }
        },
        {6, () =>
            {
                //Water Management
                //Water Management Prefab Created
                MULTIPLIERS[7] = 0.5f;
            }
        },
        {7, () =>
            {
                //Increase Field Health
               MULTIPLIERS[8] = 1.5f;
            }
        },
        {8, () =>
            {
                //Unlock Nitrogen Fixing Crop
                GameManager.UnlockedCrops[5] = true;
            }
        },
        {9, () =>
            {
                //Increase Soil Quality
                MULTIPLIERS[10] = 1.2f;
            }
        },


    };

   

    public static void Apply(int ID)
    {
        System.Action action;
        if(ACTIONERS.TryGetValue(ID, out action))
        {
            action();
        }
    }

    public static float GetMultiplier(int ID)
    {
        float Multiplier;
        if(MULTIPLIERS.TryGetValue(ID, out Multiplier))
        {
            return Multiplier;
        }
        else
        {
            return 1f;
        }
    }
    public static bool CheckUnlocked(int ID)
    {
        return GameManager.UnlockedCrops[ID];
    }
}
