using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameplayData
{
    private GameData gameData = new GameData();
    private SaveDataService saveDataService = new SaveDataService();
    private readonly GameplayData _gameplayData;
    [SerializeField] private int drawCount;
    [SerializeField] private int maxMana;
    [SerializeField] private int currentMana;
    [SerializeField] private int currentGold;
    [SerializeField] private int currentHP;
    [SerializeField] private int currentStageId;
    [SerializeField] private int currentEncounterId;
    [SerializeField] private bool isFinalEncounter;
    [SerializeField] private List<CardData> currentCardsList;

    public InitGameplayData(GameplayData gameplayData)
    {
        _gameplayData = gameplayData;

        InitData();
    }

    
    private void InitData()
    {
        DrawCount = _gameplayData.DrawCount;
        MaxMana = _gameplayData.MaxMana;
        CurrentMana = MaxMana;
        LoadDataFromSaveFile();

    }

    private void LoadDataFromSaveFile()
    {
        gameData = saveDataService.LoadGameData();
        if (gameData != null)
        {
            CurrentEncounterId = gameData.currentEncounterId;
            CurrentStageId = gameData.currentStageId;
            CurrentGold = gameData.currentGold;
            CurrentCardsList = gameData.currentCardsList;
            IsFinalEncounter = gameData.isFinalEncounter;
            CurrentHP = gameData.currentHP;
        }
        else
        {
            gameData = new GameData();
            CurrentEncounterId = 0;
            CurrentStageId = 0;
            CurrentGold = 1000;
            CurrentHP = 0;
            CurrentCardsList = new List<CardData>();
            IsFinalEncounter = false;
        }
    }



    public void SaveData()
    {

        gameData.SetData(CurrentGold, CurrentHP, CurrentStageId, CurrentEncounterId, IsFinalEncounter, CurrentCardsList);
        saveDataService.SaveData(gameData);
    }

    public void ResetData(GameplayData gameplayData)
    {
        gameData = new GameData();
        CurrentEncounterId = 0;
        CurrentStageId = 0;
        CurrentGold = 1000;
        CurrentHP = 0;
        CurrentCardsList = gameplayData.InitalDeck.CardList;
        IsFinalEncounter = false;
    }

    public void SetCurrentHP(int maxHp)
    {
        if (CurrentHP >0) return;
        CurrentHP = maxHp;
    }

    #region Encapsulation


    public int CurrentHP
    {
        get => currentHP;
        set => currentHP = value;   
    }


    public int DrawCount
    {
        get => drawCount;
        set => drawCount = value;
    }

    public int MaxMana
    {
        get => maxMana;
        set => maxMana = value;
    }

    public int CurrentMana
    {
        get => currentMana;
        set => currentMana = value;
    }


    public int CurrentStageId
    {
        get => currentStageId;
        set => currentStageId = value;
    }

    public int CurrentEncounterId
    {
        get => currentEncounterId;
        set => currentEncounterId = value;
    }

    public bool IsFinalEncounter
    {
        get => isFinalEncounter;
        set => isFinalEncounter = value;
    }

    public List<CardData> CurrentCardsList
    {
        get => currentCardsList;
        set => currentCardsList = value;
    }

 
    public int CurrentGold
    {
        get => currentGold;
        set => currentGold = value;
    }

    #endregion
}

