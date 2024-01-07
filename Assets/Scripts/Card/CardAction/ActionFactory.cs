using System.Collections.Generic;
using System;

public class ActionFactory
{
    private static Dictionary<CardActionType, CardActionBase> actionCache = new Dictionary<CardActionType, CardActionBase>();

    public static CardActionBase GetAction(CardActionType type)
    {
        if (!actionCache.ContainsKey(type))
        {
  
            CardActionBase newAction = CreateAction(type);
            actionCache.Add(type, newAction);
        }

        return actionCache[type];
    }

    private static CardActionBase CreateAction(CardActionType type)
    {
        switch (type)
        {
            case CardActionType.Attack:
                return new AttackAction();
            case CardActionType.Block:
                return new BlockAction();
            case CardActionType.IncreaseStrength:
                return new StrengthAction();
            case CardActionType.Heal:
                return new HealAction();
            case CardActionType.IncreaseBlock:
                return new DefendAction();
            case CardActionType.IncreaseMaxHealth:
                return new IncreaseMaxHPAction();
            case CardActionType.Poison:
                return new PoisonAction();
            case CardActionType.Bleed:
                return new BleedAction();
            case CardActionType.Curse:
                return new CurseAction();
            case CardActionType.DoNothing:
                return new DoNotingAction();
            default:
                throw new ArgumentException("Invalid action type");
        }
    }
}

