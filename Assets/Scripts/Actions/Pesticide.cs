using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Pesticide : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManagerObject;
    [SerializeField]
    private GameObject MarketGameObject;

    private GameManager gameManager;
    private CommunityManager communityManager;
    private Market market;
    private UIManager uIManager;

    [SerializeField]
    private int PesticideEffect;

    public void Awake()
    {
        gameManager = GameManagerObject.GetComponent<GameManager>();
        communityManager = GameManagerObject.GetComponent<CommunityManager>();
        market = GameManagerObject.GetComponent<Market>();
        uIManager = GameManagerObject.GetComponent<UIManager>();
        market = MarketGameObject.GetComponent<Market>();
    }

    public void UsePesticide()
    {
        if(gameManager.ActionRemaining(1))
        {
            //Instantiate Effects

            market.marketInventory.Pesticide--;
            gameManager.numPollinators -= PesticideEffect;
            communityManager.communityHealth -= PesticideEffect;

            for (int i = 0; i < gameManager.numPests.Length; i++)
            {
                gameManager.numPests[i] -= PesticideEffect;
            }
            uIManager.UpdateUIText();
        }
    }
}
