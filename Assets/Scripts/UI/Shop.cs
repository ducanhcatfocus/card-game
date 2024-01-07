using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : UICanvas
{
    [SerializeField] private UICardBase choiceCardUIPrefab;
    [SerializeField] Transform cardParent;
    [SerializeField] TextMeshProUGUI currentCoinText;
    int currentGold;
    bool isFirstOpen= false;
    private List<CardData> cardShopList = new List<CardData>();
    InitGameplayData InitGameplayData;


    public override void Open()
    {
        base.Open();
  
        if(!isFirstOpen)
        {
            isFirstOpen = true;
            InitGameplayData = GameManager.InitGameplayData;
            BuildReward();
        }


    }

    private void BuildReward()
    {

        currentGold = InitGameplayData.CurrentGold;
        currentCoinText.text = currentGold.ToString();
        cardShopList = GameManager.GameplayData.AllCardsList;
        SetUpCard();
    }


    public void CloseButtton()
    {

        Close();
    }


    private void BuyCard(CardData cardData, UICardBase card)
    {
        if (currentGold >= card.Cost)
        { 
            card.RewardButton.interactable = false;
            card.OpenSoldOutPanel();
            StartCoroutine(GoldTextReduce(card.Cost, cardData));
            AudioManager.PlayCoinSound();
       
        }    
    }

    private void SetUpCard()
    {
        foreach (CardData item in cardShopList)
        {
            UICardBase rewardCard = Instantiate(choiceCardUIPrefab, cardParent);
            rewardCard.SetCard(item);
            rewardCard.SetCost();
            rewardCard.RewardButton.onClick.AddListener(() => BuyCard(item, rewardCard));
       
        }
    }

    private IEnumerator GoldTextReduce(int cost, CardData card)
    {
        int remainCoin = currentGold - cost;
        while (currentGold > remainCoin)
        {
            yield return new WaitForSeconds(0.01f);
            currentGold -= 11;
            currentCoinText.text = currentGold.ToString();
        }
        currentGold = remainCoin;
        currentCoinText.text = currentGold.ToString();
        InitGameplayData.CurrentCardsList.Add(card);
        InitGameplayData.CurrentGold = currentGold;
   
    }

}
