using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityManager : MonoBehaviour
{
    public static CommunityManager inst;

    [field: SerializeField]
    public int communityHealth { get; set; }
    [field: SerializeField]
    public int TopLimit { get; set; }

    public CommunityPresets[] CommunityImprovement;
    HealthSate CurrentHealthSate;

    void Awake()
    {
        inst = this;
        CurrentHealthSate = HealthSate.AVERAGEHEALTH;
    }

    public void CheckHealth()
    {
        communityHealth++;
        communityHealth = Mathf.Clamp(communityHealth, 0, TopLimit);

        switch (CurrentHealthSate)
        {
            case HealthSate.EXCELLENTHEALTH:
                //Grass
                //Water
                break;
            case HealthSate.GOODHEALTH:
                //Grass
                //Water
                break;
            case HealthSate.AVERAGEHEALTH:
                //Grass
                //Water
                break;
            case HealthSate.BADHEALTH:
                //Grass
                //Water
                break;
            case HealthSate.TERRIBLEHEALTH:
                //Grass
                //Water
                break;
            default:
                break;
        }
    }

    public void GetImprovement(int index)
    {
        switch(index)
        {
            case 1:
            //CommunityImprovement = Resources.Load<CommunityPresets>("CommunityPresets/School");
            //Subtract Money
            //Add Community
            //Instantiate Prefab
            //Destroy any Prefabs
                break;
            default:
                break;
        }       
    }
}

enum HealthSate
{
    EXCELLENTHEALTH,
    GOODHEALTH,
    AVERAGEHEALTH,
    BADHEALTH,
    TERRIBLEHEALTH,
};
