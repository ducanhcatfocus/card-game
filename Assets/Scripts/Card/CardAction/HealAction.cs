using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.Heal;

    public override void DoAction(CharacterBase tartget, float actionValue, CharacterBase selftarget)
    {
        tartget.CharacterStats.Heal((int)actionValue);
        FxManager.DisplayBuffEffect(tartget.PositiveBuffPos, "Heal " + (int)actionValue + " hp" , false);
        AudioManager.PlayBuffSound();

    }
}
