using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public int currentGold;
    public int currentHP;
    public int currentStageId;
    public int currentEncounterId;
    public bool isFinalEncounter;
    public List<CardData> currentCardsList;

    public void SetData(int currentGold, int currentHP, int currentStageId, int currentEncounterId, bool isFinalEncounter, List<CardData> currentCardsList)
    {
    
        this.currentGold = currentGold;
        this.currentHP = currentHP;
        this.currentStageId = currentStageId;
        this.currentEncounterId = currentEncounterId;
        this.isFinalEncounter = isFinalEncounter;
        this.currentCardsList = currentCardsList;
    }
}
