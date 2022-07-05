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

    // Start is called before the first frame update
    void Awake()
    {
        inst = this;
    }

    public void CheckHealth()
    {
        //Grass

        //Water

        //Houses

        communityHealth++;
        communityHealth = Mathf.Clamp(communityHealth, 0, TopLimit);
    }

}