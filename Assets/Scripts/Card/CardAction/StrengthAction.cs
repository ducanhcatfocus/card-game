using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.IncreaseStrength;

    public override void DoAction(CharacterBase tartget, float actionValue, CharacterBase selftarget)
    {
        tartget.CharacterStats.ApplyStatus(StatusType.Strength, (int)actionValue);
        FxManager.DisplayBuffEffect(tartget.PositiveBuffPos, "Damage ++", false);
        AudioManager.PlayBuffSound();

    }
}
