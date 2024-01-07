using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardDeck : UICanvas
{
    private List<CardBase> _spawnedCardList = new List<CardBase>();
    [SerializeField] private UICardBase cardUIPrefab;
    [SerializeField] private Transform cardSpawnRoot;
    public Transform CardSpawnRoot => cardSpawnRoot;
    public void CloseButton()
    {
        GameManager.Ins.AlowClick(true);
        UIManager.Ins.OpenUI<GamePlay>();

        Close();
    }

    public override void Open()
    {
        base.Open();
        if (DeckManager == null)
        {
            DeckManager = DeckManager.Ins;

        }
        SetCards(DeckManager.DiscardPile);
    }

    public void SetCards(List<CardData> cardDataList)
    {
        int count = 0;

        for (int i = 0; i < _spawnedCardList.Count; i++)
        {
            count++;
            if (i >= cardDataList.Count)
            {
                _spawnedCardList[i].gameObject.SetActive(false);
            }
            else
            {
                _spawnedCardList[i].SetCard(cardDataList[i], false);
                _spawnedCardList[i].gameObject.SetActive(true);
            }

        }
        int cal = cardDataList.Count - count;
        if (cal > 0)
        {
            for (int i = 0; i < cal; i++)
            {
                var cardData = cardDataList[count + i];
                var cardBase = Instantiate(cardUIPrefab, CardSpawnRoot.transform);
                cardBase.SetCard(cardData, false);
                _spawnedCardList.Add(cardBase);
            }
        }
    }
}
