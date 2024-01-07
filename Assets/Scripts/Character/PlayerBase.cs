using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{

    [Header("Enemy Base References")]
    [SerializeField] protected PlayerDataBase playerData;
    public PlayerDataBase PlayerData => playerData;
    public override void BuildCharacter()
    {
        base.BuildCharacter();
        CharacterStats = new CharacterStats(PlayerData.MaxHealth, CharacterCanvas);
        GameManager.InitGameplayData.SetCurrentHP(PlayerData.MaxHealth);
        CharacterStats.SetCurrentHealth(GameManager.InitGameplayData.CurrentHP);
        characterCanvas.UpdateGoldText(GameManager.InitGameplayData.CurrentGold);
        CharacterStats.OnDeath += OnDeath;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        CombatManager.OnPlayerDeath();
    }
}
