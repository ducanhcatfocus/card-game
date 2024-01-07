using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemoveShop : UICanvas
{
    [SerializeField] private UICardBase choiceCardUIPrefab;
    [SerializeField] Transform cardParent;
    [SerializeField] TextMeshProUGUI removeTimeText;
    int timeRemove = 1;
    bool isFirstOpen = false;
    private List<CardData> cardShopList = new List<CardData>();
    InitGameplayData InitGameplayData;


    public override void Open()
    {
        base.Open();
        if (!isFirstOpen)
        {
            isFirstOpen = true;
            InitGameplayData = GameManager.InitGameplayData;
            BuildReward();
        }


    }

    private void BuildReward()
    {


        
        cardShopList =InitGameplayData.CurrentCardsList;
        SetUpCard();
    }


    public void CloseButtton()
    {

        Close();
    }


    private void BuyCard(CardData cardData, UICardBase card)
    {
        if (timeRemove <= 0) return;

        timeRemove--;
        card.RewardButton.interactable = false;
        card.OpenSoldOutPanel();
        removeTimeText.text = "Card remove times: " + timeRemove;
        AudioManager.PlayCoinSound();
        InitGameplayData.CurrentCardsList.Remove(cardData);  
    }

    private void SetUpCard()
    {
        foreach (CardData item in cardShopList)
        {
            UICardBase rewardCard = Instantiate(choiceCardUIPrefab, cardParent);
            rewardCard.SetCard(item);
            rewardCard.SetFreeCost();
            rewardCard.RewardButton.onClick.AddListener(() => BuyCard(item, rewardCard));
        }
    }
}
