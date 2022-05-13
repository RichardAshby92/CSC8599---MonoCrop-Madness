using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    public GameObject gameManagerObject;

    private GameManager gameManager;
    private UIManager uIManager;
    private LoadText loadText;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        uIManager = gameManagerObject.GetComponent<UIManager>();
        loadText = gameManagerObject.GetComponent<LoadText>();
    }

    public void AccessMenu()
    {
        uIManager.DisableMenus();
        gameObject.SetActive(true);
    }

    private void PopulatePage()
    {

    }
}
