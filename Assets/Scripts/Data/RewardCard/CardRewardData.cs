using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Reward Data", menuName = "Data/Reward/CardRW", order = 0)]
public class CardRewardData : RewardDataBase
{
    [SerializeField] private List<CardData> rewardCardList;
    public List<CardData> RewardCardList => rewardCardList;
}
