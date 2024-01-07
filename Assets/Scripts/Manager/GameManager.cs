using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static GameManager instance;

    public static GameManager Ins => instance;

    [SerializeField] private GameplayData gameplayData;

    [SerializeField] private EncounterData encounterData;

    [SerializeField] bool allowClick = true;

    public InitGameplayData InitGameplayData { get; private set; }

    public EncounterData EncounterData => encounterData;
    public GameplayData GameplayData => gameplayData;

    public bool AllowClick => allowClick;




    private void Awake()
    {
    
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitData();
        SetInitalHand();
    }


    public void InitData()
    {
        InitGameplayData = new InitGameplayData(GameplayData);
  
    }

    public void ResetData()
    {
        InitGameplayData.ResetData(GameplayData);
    }

    public void SetInitalHand()
    {
        if (InitGameplayData.CurrentCardsList.Count > 0)
            return;
        foreach (var cardData in gameplayData.InitalDeck.CardList)
            InitGameplayData.CurrentCardsList.Add(cardData);
    }

    public void AlowClick(bool isClick)
    {
        allowClick = isClick;
    }
}
