using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BleedAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.Bleed;

    public override void DoAction(CharacterBase target, float actionValue, CharacterBase selftarget)
    {
        target.CharacterStats.ApplyStatus(StatusType.Bleed, (int)actionValue);
        FxManager.DisplayBuffEffect(target.NegativeBuffPos, "Bleeding", true);

    }
}
