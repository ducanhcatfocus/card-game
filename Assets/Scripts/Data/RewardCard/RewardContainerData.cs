using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Reward Container", menuName = "Data/Reward/RewardContainer", order = 2)]
public class RewardContainerData : ScriptableObject
{
    [SerializeField] private List<CardRewardData> cardRewardDataList;
    [SerializeField] private List<GoldRewardData> goldRewardDataList;
    public List<CardRewardData> CardRewardDataList => cardRewardDataList;
    public List<GoldRewardData> GoldRewardDataList => goldRewardDataList;

    public List<CardData> GetRandomCardRewardList()
    {
      

        List<CardData> cardList = new List<CardData>();

        foreach (var cardData in CardRewardDataList.GetRandomListItem().RewardCardList)
            cardList.Add(cardData);

        return cardList;
    }
    public int GetRandomGoldReward(out GoldRewardData rewardData)
    {
        rewardData = GoldRewardDataList.GetRandomListItem();
        var value = Random.Range(rewardData.MinGold, rewardData.MaxGold);

        return value;
    }

}
