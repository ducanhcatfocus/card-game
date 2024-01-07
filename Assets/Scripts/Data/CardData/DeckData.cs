using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck Data", menuName = "Data/Deck", order = 0)]
public class DeckData : ScriptableObject
{
    [SerializeField] private string deckId;
    [SerializeField] private string deckName;

    [SerializeField] private List<CardData> cardList;
    public List<CardData> CardList => cardList;

    public string DeckId => deckId;

    public string DeckName => deckName;
}

