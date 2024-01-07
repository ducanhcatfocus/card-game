using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gameplay Data", menuName = "Data/GameplayData", order = 0)]
public class GameplayData : ScriptableObject
{
    [Header("Gameplay Settings")]
    [SerializeField] private int drawCount = 4;
    [SerializeField] private int maxMana = 4;
    [SerializeField] private PlayerBase initalPlayer;

    [Header("Decks")]
    [SerializeField] private DeckData initalDeck;
    [SerializeField] private int maxCardOnHand;

    [Header("Card Settings")]
    [SerializeField] private List<CardData> allCardsList;
    [SerializeField] private CardBase cardPrefab;
    [SerializeField] private EnemyCardBase enemyCardPrefab;


    [Header("Modifiers")]
    [SerializeField] private bool isRandomHand = false;
    [SerializeField] private int randomCardCount;

    public int DrawCount => drawCount;
    public int MaxMana => maxMana;
    public bool IsRandomHand => isRandomHand;
    public PlayerBase InitalPlayer => initalPlayer;
    public DeckData InitalDeck => initalDeck;
    public int RandomCardCount => randomCardCount;
    public int MaxCardOnHand => maxCardOnHand;
    public List<CardData> AllCardsList => allCardsList;
    public CardBase CardPrefab => cardPrefab;

    public EnemyCardBase EnemyCardPrefab => enemyCardPrefab;
}
