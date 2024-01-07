using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [SerializeField] private HandController handController;
    [SerializeField] CardData curseCard;
    public List<CardData> DrawPile  = new List<CardData>();
    public List<CardData> HandPile  = new List<CardData>();
    public List<CardData> DiscardPile  = new List<CardData>();
    public List<CardData> ExhaustPile  = new List<CardData>();

    public List<CardData> EnemyDrawPile = new List<CardData>();
    public List<CardData> EnemyHandPile = new List<CardData>();


    public HandController HandController => handController;

    private GameManager GameManager => GameManager.Ins;

    private CombatManager CombatManager => CombatManager.Ins;

    private UIManager UIManager => UIManager.Ins;

    public void DrawCards(int targetDrawCount)
    {
        var currentDrawCount = 0;

        for (var i = 0; i < targetDrawCount; i++)
        {
            //if (GameManager.GameplayData.MaxCardOnHand <= HandPile.Count)
                //return;

            if (DrawPile.Count <= 0)
            {
                var nDrawCount = targetDrawCount - currentDrawCount;

                if (nDrawCount >= DiscardPile.Count)
                    nDrawCount = DiscardPile.Count;

                ReshuffleDiscardPile();
                DrawCards(nDrawCount);
                break;
            }

            var randomCard = DrawPile.GetRandomListItem();
            HandController.AddCardToHand(randomCard);
            HandPile.Add(randomCard);
            DrawPile.Remove(randomCard);
            currentDrawCount++;
            //
            UIManager.SetCardDeckAmount(DrawPile.Count, DiscardPile.Count);
        }
    }
    public void DiscardHand()
    {
        foreach (var cardBase in HandController.cardHandList)
            cardBase.Discard();

        HandController.cardHandList.Clear();
    }

    public void OnCardDiscarded(CardBase targetCard)
    {
        HandPile.Remove(targetCard.CardData);
        DiscardPile.Add(targetCard.CardData);
        //UIManager.CombatCanvas.SetPileTexts();
        HandController.RemoveCardFromHand(targetCard);
        if(HandPile.Count <= 0)
        {
            CombatManager.DoEffectCardEachTurn();
            DrawCards(4);
            EnemyDrawCards(4);

        }
        UIManager.SetCardDeckAmount(DrawPile.Count, DiscardPile.Count);


    }

    public void OnCardExhausted(CardBase targetCard)
    {
        HandPile.Remove(targetCard.CardData);
        ExhaustPile.Add(targetCard.CardData);
        HandController.RemoveCardFromHand(targetCard);
        if (HandPile.Count <= 0)
        {
            DrawCards(4);
            EnemyDrawCards(4);

        }

        UIManager.SetCardDeckAmount(DrawPile.Count, DiscardPile.Count);

    }

    public void OnCardPlayed(CardBase targetCard)
    {
        if (targetCard.CardData.ExhaustAfterPlay)
            targetCard.Exhaust();
        else
            targetCard.Discard();
    }
    public void SetGameDeck()
    {
        foreach (var card in GameManager.InitGameplayData.CurrentCardsList)
            DrawPile.Add(card);
    }

    public void SetEnemyDeck(EnemyDataBase enemy)
    {
        foreach (var card in enemy.EnemyAbilityList)
            EnemyDrawPile.Add(card);
    }

    public void EnemyDrawCards(int drawCount )
    {  
        EnemyHandPile.Clear();
        HandController.RemoveCardFromEnemyHand();
        for (int i = 0; i < drawCount; i++)
        {
            var randomCard = EnemyDrawPile.GetRandomListItem();

            HandController.AddCardToEnemyHand(randomCard);
            EnemyHandPile.Add(randomCard);

        }
    }


    private void ReshuffleDiscardPile()
    {
        foreach (CardData card in DiscardPile)
            DrawPile.Add(card);

        DiscardPile.Clear();
    }

    public void ClearPiles()
    {
        DiscardPile.Clear();
        DrawPile.Clear();
        HandPile.Clear();
        ExhaustPile.Clear();
        HandController.cardHandList.Clear();
    }


    public void AddCurseCard()
    {
        DrawPile.Add(curseCard);
        UIManager.SetCardDeckAmount(DrawPile.Count, DiscardPile.Count);
    }

    public void MoveCard(CardBase card)
    {
       
        HandController.MoveCardToPos(card);
    }
}
