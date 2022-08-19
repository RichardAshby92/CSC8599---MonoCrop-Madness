using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementNodeActioners : MonoBehaviour
{
    static Dictionary<int, float> S_MULTIPLIERS = new Dictionary<int, float>();
    static Dictionary<int, System.Action> S_ACTIONERS = new Dictionary<int, System.Action>()
    {
        { 0, () =>
            {
                //Local Trading Block
                S_MULTIPLIERS[2] = 1.15f;
            }
        },
        {1, () =>
            {
                //Diverse Cash Crop               
                GameManager.S_UnlockedCrops[0] = true;
                GameManager.S_UnlockedCrops[2] = true;
                GameManager.S_UnlockedCrops[8] = true;
            }
        },
        {2, () =>
            {
                //Slash and Burn
                SlashAndBurn.S_ImprovementUnlocked = true;
            }

        },
        {3, () =>
            {
                //Expanded Trading Block
                S_MULTIPLIERS[2] = 1.25f;
            }
        },
        {4, () =>
            {
                //Food Crops
                GameManager.S_UnlockedCrops[3] = true;
                GameManager.S_UnlockedCrops[5] = true;
                GameManager.S_UnlockedCrops[9] = true;
            }
        },
        {5, () =>
            {
                //Unlock Drought Crops
                GameManager.S_UnlockedCrops[1] = true;
                GameManager.S_UnlockedCrops[6] = true;
            }
        },
        {6, () =>
            {
                //Water Management Strategies
                //Water Management Prefab Created
                S_MULTIPLIERS[7] = 0.5f;
            }
        },
        {7, () =>
            {
                //Companion Cropping
               S_MULTIPLIERS[8] = 1.5f;
            }
        },
        {8, () =>
            {
                //Nitrogen Fixing Crop
                GameManager.S_UnlockedCrops[4] = true;
            }
        },
        {9, () =>
            {
                //Slash and Mulch
                S_MULTIPLIERS[10] = 1.2f;
            }
        },
    };

   

    public static void Apply(int ID)
    {
        System.Action action;
        if(S_ACTIONERS.TryGetValue(ID, out action))
        {
            action();
        }
    }

    public static float GetMultiplier(int ID)
    {
        float Multiplier;
        if(S_MULTIPLIERS.TryGetValue(ID, out Multiplier))
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
        return GameManager.S_UnlockedCrops[ID];
    }
}
