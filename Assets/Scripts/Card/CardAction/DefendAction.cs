using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefendAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.IncreaseBlock;

    public override void DoAction(CharacterBase target, float actionValue, CharacterBase selftarget)
    {
        target.CharacterStats.ApplyStatus(StatusType.Dexterity, (int)actionValue);
        FxManager.DisplayBuffEffect(target.PositiveBuffPos, "Defense ++", false);
        AudioManager.PlayBuffSound();
    }
}
