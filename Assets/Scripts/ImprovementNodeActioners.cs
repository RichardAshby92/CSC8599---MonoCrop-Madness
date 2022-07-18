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
            MULTIPLIERS[1] = 0.9f;
            }
        },
        {2, () =>
            {
                MULTIPLIERS[2] = 0.7f;
            }
        }

    };

    static void Apply(int ID)
    {
        System.Action action;
        if(ACTIONERS.TryGetValue(ID, out action))
        {
            action();
        }
    }

    static float GetMultiplier(int ID)
    {
        float Multiplier;
        if(MULTIPLIERS.TryGetValue(ID, out Multiplier))
        {
            return Multiplier;
        }
        else
        {
            print("Failed to find Value");
            return 1f;
        }
    }
}
