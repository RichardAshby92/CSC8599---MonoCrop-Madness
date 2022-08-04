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
                //Local Trading Block
                MULTIPLIERS[2] = 1.15f;
            }
        },
        {1, () =>
            {
                //Diverse Cash Crop               
                GameManager.UnlockedCrops[0] = true;
                GameManager.UnlockedCrops[2] = true;
                GameManager.UnlockedCrops[8] = true;
            }
        },
        {2, () =>
            {
                //Slash and Burn
                SlashAndBurn.ImprovementUnlocked = true;
            }

        },
        {3, () =>
            {
                //Expanded Trading Block
                MULTIPLIERS[2] = 1.25f;
            }
        },
        {4, () =>
            {
                //Food Crops
                GameManager.UnlockedCrops[3] = true;
                GameManager.UnlockedCrops[5] = true;
                GameManager.UnlockedCrops[9] = true;
            }
        },
        {5, () =>
            {
                //Unlock Drought Crops
                GameManager.UnlockedCrops[1] = true;
                GameManager.UnlockedCrops[6] = true;
            }
        },
        {6, () =>
            {
                //Water Management Strategies
                //Water Management Prefab Created
                MULTIPLIERS[7] = 0.5f;
            }
        },
        {7, () =>
            {
                //Companion Cropping
               MULTIPLIERS[8] = 1.5f;
            }
        },
        {8, () =>
            {
                //Nitrogen Fixing Crop
                GameManager.UnlockedCrops[4] = true;
            }
        },
        {9, () =>
            {
                //Slash and Mulch
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
