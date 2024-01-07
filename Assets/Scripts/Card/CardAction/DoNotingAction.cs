using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotingAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.DoNothing;
    public override void DoAction(CharacterBase target, float actionValue, CharacterBase selftarget)
    {
      
    }

}
