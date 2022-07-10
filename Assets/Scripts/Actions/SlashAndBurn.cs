using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlashAndBurn : MonoBehaviour
{
    [SerializeField]
    private GameObject NewFieldPrefab;
    [SerializeField]
    private GameObject GameManagerObject;
    [SerializeField]
    private GameObject FieldHealthObject;

    private FieldProperties fieldProperties;
    private FieldActions fieldActions;
    private CommunityManager CommunityManager;
    private UIManager uIManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
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
        gameManager.remainingActions -= 4;

        NewFieldPrefab = Instantiate(NewFieldPrefab, transform.position, transform.rotation);

        NewFieldPrefab.GetComponent<FieldProperties>().gameManagerObject = GameManagerObject;
        NewFieldPrefab.GetComponent<FieldActions>().gameManagerObject = GameManagerObject;
        NewFieldPrefab.GetComponent<FieldActions>().fieldHealthObject = FieldHealthObject; 

        NewFieldPrefab.GetComponent<FieldProperties>().Intialise();
        NewFieldPrefab.GetComponent<FieldActions>().Intialise();

        gameManager.AddField(NewFieldPrefab);

        Destroy(gameObject);
    }
}
