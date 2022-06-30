using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAndBurn : MonoBehaviour
{
    [SerializeField]
    private GameObject NewField;
    private GameObject GameManagerObject;

    private FieldProperties FieldProperties;
    private CommunityManager CommunityManager;
    // Start is called before the first frame update
    void Awake()
    {
        FieldProperties = GetComponent<FieldProperties>();
        CommunityManager = GameManagerObject.GetComponent<CommunityManager>();
    }

    void BurnField()
    {
        //Burn Effect
        //Enviroment Harm
        Instantiate(NewField);
        //Get and Set Needed Properties
        //FieldProperties

        Destroy(this);
    }
}
