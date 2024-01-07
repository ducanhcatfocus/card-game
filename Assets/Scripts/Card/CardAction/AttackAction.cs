using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackAction : CardActionBase
{
    public float duration = 0.2f;
    public float magnitude = 0.1f;
    public override CardActionType ActionType => CardActionType.Attack;
    public override void DoAction(CharacterBase target, float actionValue, CharacterBase selftarget)
    {
        float totalDmg = actionValue + selftarget.CharacterStats.StatusDict[StatusType.Strength].StatusValue;
        float totalDef = target.CharacterStats.StatusDict[StatusType.Dexterity].StatusValue;
        float dmg = totalDmg - totalDef <=0 ? 0 : totalDmg - totalDef;
        target.CharacterStats.Damage(dmg);
        target.VibrateTarget();
        FxManager.PlayHitFx(target.transform);
        FxManager.DisplayFloatingDamage(target.transform, (int)dmg);
        AudioManager.PlayAttackSound();
    }

}
