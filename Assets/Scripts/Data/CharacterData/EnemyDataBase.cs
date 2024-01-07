using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Enemy Data ", menuName = "Data/Enemy", order = 0)]
public class EnemyDataBase : CharacterDataBase
{
    [Header("Enemy Defaults")]
    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private bool followAbilityPattern;
    [SerializeField] private List<CardData> enemyAbilityList;
    public List<CardData> EnemyAbilityList => enemyAbilityList;

    public EnemyBase EnemyPrefab => enemyPrefab;

    public CardData GetAbility()
    {
        return EnemyAbilityList.GetRandomListItem();
    }

    public List<CardData> GetListAbility()
    {
        return EnemyAbilityList;
    }


    public CardData GetAbility(int usedAbilityCount)
    {
        if (followAbilityPattern)
        {
            var index = usedAbilityCount % EnemyAbilityList.Count;
            return EnemyAbilityList[index];
        }

        return GetAbility();
    }
}

[Serializable]
public class EnemyAbilityData
{
    [Header("Settings")]
    [SerializeField] private string name;
    [SerializeField] private List<EnemyActionData> actionList;
    public string Name => name;
    public List<EnemyActionData> ActionList => actionList;
}

[Serializable]
public class EnemyActionData
{
    [SerializeField] private CardActionType actionType;
    [SerializeField] private int minActionValue;
    [SerializeField] private int maxActionValue;
    public CardActionType ActionType => actionType;
    public int ActionValue => Random.Range(minActionValue, maxActionValue);

}
