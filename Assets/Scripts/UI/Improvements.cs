using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Improvements : MonoBehaviour
{
    public GameObject gameManagerObject;

    private GameManager gameManager;
    private UIManager uIManager;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();

    }


}
