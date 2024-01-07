using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "Data/Card", order = 0)]
public class CardData : ScriptableObject
{
    [Header("Card Profile")]
    [SerializeField] private string id;
    [SerializeField] private string cardName;
    [SerializeField] private int manaCost;
    [SerializeField] private Sprite cardSprite;
    [SerializeField] private Sprite cardBGSprite;

    [SerializeField] private RarityType rarity;

    [Header("Action Settings")]
    [SerializeField] private bool usableWithoutTarget;
    [SerializeField] private bool exhaustAfterPlay;
    [SerializeField] private List<CardActionData> cardActionDataList;

    [Header("Description")]
    [SerializeField] private string cardDescription;


    public string Id => id;
    public bool UsableWithoutTarget => usableWithoutTarget;
    public int ManaCost => manaCost;
    public string CardName => cardName;
    public Sprite CardSprite => cardSprite;

    public Sprite CardBGSprite => cardBGSprite;
    public List<CardActionData> CardActionDataList => cardActionDataList;
    public string CardDescription => cardDescription;

    public string MyDescription { get; set; }
    public RarityType Rarity => rarity;

    public bool ExhaustAfterPlay => exhaustAfterPlay;
    
}

[Serializable]
public class CardActionData
{
    [SerializeField] private CardActionType cardActionType;
    [SerializeField] private ActionTargetType actionTargetType;
    [SerializeField] private float actionValue;
    [SerializeField] private float actionDelay;

    public ActionTargetType ActionTargetType => actionTargetType;
    public CardActionType CardActionType => cardActionType;
    public float ActionValue => actionValue;
    public float ActionDelay => actionDelay;
   
}




