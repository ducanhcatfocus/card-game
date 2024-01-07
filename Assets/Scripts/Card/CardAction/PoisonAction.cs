using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PoisonAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.Poison;

    public override void DoAction(CharacterBase target, float actionValue, CharacterBase selftarget)
    {
        target.CharacterStats.ApplyStatus(StatusType.Poison, (int) actionValue);
        FxManager.DisplayBuffEffect(target.NegativeBuffPos, "Poison", true);

    }
}
