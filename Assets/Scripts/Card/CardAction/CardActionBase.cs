using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class CardActionBase
{
    public abstract CardActionType ActionType { get; }
    public abstract void DoAction(CharacterBase tartget, float actionValue, CharacterBase selftarget);

    protected FxManager FxManager => FxManager.Ins;
    protected AudioManager AudioManager => AudioManager.Ins;

}
