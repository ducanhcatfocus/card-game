using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.Curse;

    public override void DoAction(CharacterBase target, float actionValue, CharacterBase selftarget)
    {
        target.CharacterStats.ApplyStatus(StatusType.Curse, (int)actionValue);
        DeckManager.Ins.AddCurseCard();
        FxManager.DisplayBuffEffect(target.NegativeBuffPos, "Curse", true);

    }
}
