using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gold Reward Data", menuName = "Data/Reward/GoldRW", order = 1)]
public class GoldRewardData : RewardDataBase
{
    [SerializeField] private int minGold;
    [SerializeField] private int maxGold;
    public int MinGold => minGold;
    public int MaxGold => maxGold;
}
