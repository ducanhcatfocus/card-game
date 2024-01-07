using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.Block;

    public override void DoAction(CharacterBase tartget, float actionValue, CharacterBase selftarget)
    {
      
    }
}
