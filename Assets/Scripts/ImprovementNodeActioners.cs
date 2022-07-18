using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementNodeActioners : MonoBehaviour
{
    static Dictionary<int, float> MULTIPLIERS = new Dictionary<int, float>();
    static Dictionary<int, System.Action> ACTIONERS = new Dictionary<int, System.Action>()
    {
        { 1, () =>
            {
                //Enables Cash Crop Buttons
            }
        },
        {2, () =>
            {
                MULTIPLIERS[2] = 1.15f;
            }
        },
        {3, () =>
            {
                //Enables Slash and Burn Mechanic
            }

        },
        {4, () =>
            {
                MULTIPLIERS[2] = 1.25f;
            }
        },
        {5, () =>
            {
                //Enable Food Crop Button
            }
        },
        {6, () =>
            {
                //Enable drought Crop Button
            }
        },
        {7, () =>
            {
                //Water Management Prefab Created
                MULTIPLIERS[7] = 0.5f;
            }
        },
        {8, () =>
            {
               MULTIPLIERS[8] = 1.5f;
            }
        },
        {9, () =>
            {
                //Enable bean Crop Button
            }
        },
        {10, () =>
            {
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
}
