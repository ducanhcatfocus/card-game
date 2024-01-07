using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHPAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.IncreaseMaxHealth;

    public override void DoAction(CharacterBase tartget, float actionValue, CharacterBase selftarget)
    {
        tartget.CharacterStats.IncreaseMaxHealth((int) actionValue);
        FxManager.DisplayBuffEffect(tartget.PositiveBuffPos, "+ " + (int)actionValue + "max hp", false);
        AudioManager.PlayBuffSound();
    }
}
