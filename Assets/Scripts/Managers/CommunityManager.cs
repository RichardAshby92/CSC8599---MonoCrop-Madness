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

    //public CommunityPresets CommunityImprovement;
    //HealthSate CurrentHealthSate;

    // Start is called before the first frame update
    void Awake()
    {
        inst = this;
        //CurrentHealthSate = HealthSate.AVERAGEHEALTH;
    }

    public void CheckHealth()
    {
        //Grass
        //Water
        //change state enum

        communityHealth++;
        communityHealth = Mathf.Clamp(communityHealth, 0, TopLimit);
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
