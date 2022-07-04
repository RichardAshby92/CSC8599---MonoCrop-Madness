using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlashAndBurn : MonoBehaviour
{
    [SerializeField]
    private GameObject NewField;
    [SerializeField]
    private GameObject GameManagerObject;

    private FieldProperties fieldProperties;
    private FieldActions fieldActions;
    private CommunityManager CommunityManager;
    private UIManager uIManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        fieldProperties = NewField.GetComponent<FieldProperties>();
        fieldActions = NewField.GetComponent<FieldActions>();
        CommunityManager = GameManagerObject.GetComponent<CommunityManager>();
        uIManager = GameManagerObject.GetComponent<UIManager>();
        gameManager = GameManagerObject.GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        uIManager.DisableMenus();
        uIManager.newFieldMenu.SetActive(true);
        uIManager.newFieldButton.onClick.AddListener(BurnField);
    }

    public void BurnField()
    {
        CommunityManager.communityHealth -= 20;
        CommunityManager.TopLimit -= 20;

        //Burn Effect
        //Takes 4 actions
        fieldProperties.gameManagerObject = GameManagerObject;
        fieldActions.gameManagerObject = GameManagerObject;
        Transform test = gameObject.transform;

        Instantiate(NewField, transform.position, transform.rotation);
        //gameManager.fields[].
        

        //Add into field Loops

        Destroy(gameObject);
    }
}
