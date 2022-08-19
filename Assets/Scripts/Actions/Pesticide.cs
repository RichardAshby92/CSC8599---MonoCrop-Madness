using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Pesticide : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private GameObject _marketGameObject;

    private GameManager _gameManager;
    private CommunityManager _communityManager;
    private Market _market;
    private UIManager _uiManager;

    [SerializeField]
    private int PesticideEffect;

    public void Awake()
    {
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        _communityManager = _gameManagerObject.GetComponent<CommunityManager>();
        _market = _gameManagerObject.GetComponent<Market>();
        _uiManager = _gameManagerObject.GetComponent<UIManager>();
        _market = _marketGameObject.GetComponent<Market>();
    }

    public void UsePesticide()
    {
        if(_gameManager.ActionRemaining(1))
        {
            //Instantiate Effects

            _market.MarketInventory.Pesticide--;
            _gameManager.NumPollinators -= PesticideEffect;
            _communityManager.CommunityHealth -= PesticideEffect;

            for (int i = 0; i < _gameManager.NumPests.Length; i++)
            {
                _gameManager.NumPests[i] -= PesticideEffect;
            }
            _uiManager.UpdateUIText();
        }
    }
}
