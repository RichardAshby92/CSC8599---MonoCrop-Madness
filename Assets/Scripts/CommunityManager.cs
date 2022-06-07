using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityManager : MonoBehaviour
{
    public static CommunityManager inst;

    public int communityHealth;

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
        communityHealth = Mathf.Clamp(communityHealth, 0, 100);
    }

}
