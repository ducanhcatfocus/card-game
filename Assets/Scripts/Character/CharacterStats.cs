using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class StatusStats
{
    public StatusType StatusType { get; set; }
    public int StatusValue { get; set; }
    public bool DecreaseOverTurn { get; set; } 
    public bool IsPermanent { get; set; } 
    public bool IsActive { get; set; }
    public bool CanNegativeStack { get; set; }
    public bool ClearAtNextTurn { get; set; }

    public Action OnTriggerAction;

    public StatusStats(StatusType statusType, int statusValue, bool decreaseOverTurn = false, bool isPermanent = false, bool isActive = false, bool canNegativeStack = false, bool clearAtNextTurn = false)
    {
        StatusType = statusType;
        StatusValue = statusValue;
        DecreaseOverTurn = decreaseOverTurn;
        IsPermanent = isPermanent;
        IsActive = isActive;
        CanNegativeStack = canNegativeStack;
        ClearAtNextTurn = clearAtNextTurn;
    }
}
public class CharacterStats
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    public int dmgModify = 0;

    public Action<int, int> OnHealthChanged;



    private readonly Action<StatusType, int> OnStatusChanged;
    private readonly Action<StatusType, int> OnStatusApplied;
    private readonly Action<StatusType> OnStatusCleared;
    public Action OnDeath;


    public readonly Dictionary<StatusType, StatusStats> StatusDict = new Dictionary<StatusType, StatusStats>();


    public CharacterStats(int maxHealth, CharacterCanvas characterCanvas)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        SetAllStatus();
        OnHealthChanged += characterCanvas.UpdateHealthText;
        OnStatusChanged += characterCanvas.UpdateStatusText;
        OnStatusApplied += characterCanvas.ApplyStatus;
        OnStatusCleared += characterCanvas.ClearStatus;       
    }

    private void SetAllStatus()
    {
        for (int i = 0; i < Enum.GetNames(typeof(StatusType)).Length; i++)
            StatusDict.Add((StatusType)i, new StatusStats((StatusType)i, 0));

        StatusDict[StatusType.Poison].DecreaseOverTurn = true;
       // StatusDict[StatusType.Poison].OnTriggerAction += DamagePoison;

        StatusDict[StatusType.Block].ClearAtNextTurn = true;

        StatusDict[StatusType.Strength].CanNegativeStack = true;
        StatusDict[StatusType.Dexterity].CanNegativeStack = true;

        StatusDict[StatusType.Stun].DecreaseOverTurn = true;
       // StatusDict[StatusType.Stun].OnTriggerAction += CheckStunStatus;

    }

    public void ApplyStatus(StatusType targetStatus, int value)
    {
        if (StatusDict[targetStatus].IsActive)
        {
            StatusDict[targetStatus].StatusValue += value;
            OnStatusChanged?.Invoke(targetStatus, StatusDict[targetStatus].StatusValue);

        }
        else
        {
            StatusDict[targetStatus].StatusValue = value;
            StatusDict[targetStatus].IsActive = true;
            OnStatusApplied?.Invoke(targetStatus, StatusDict[targetStatus].StatusValue);
        }
    }

    public void ClearStatus(StatusType targetStatus)
    {
        StatusDict[targetStatus].IsActive = false;
        StatusDict[targetStatus].StatusValue = 0;
        OnStatusCleared?.Invoke(targetStatus);
    }




    private void TriggerStatus(StatusType targetStatus)
    {
       
    }

    public void TriggerAllStatus()
    {
        for (int i = 0; i < Enum.GetNames(typeof(StatusType)).Length; i++)
            TriggerStatus((StatusType)i);
    }

    public void SetCurrentHealth(int targetCurrentHealth)
    {
        CurrentHealth = targetCurrentHealth <= 0 ? 1 : targetCurrentHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void SetCurrentGold(int targetCurrentHealth)
    {
        CurrentHealth = targetCurrentHealth; 
        //OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void Heal(int value)
    {
        CurrentHealth += value;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void IncreaseMaxHealth(int value)
    {
        MaxHealth += value;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

    }

    public void ApplyIncreaseStreng(int value)
    {
        dmgModify += value;
    }

    public void Damage(float value)
    {
        CurrentHealth -= (int)value;

        CheckDie();

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        //FxManager.Ins.DisplayFloatingDamage(target.transform);

    }

    public void PoisonDamage(Transform target)
    {
        if (StatusDict[StatusType.Poison].StatusValue <= 0) return;
        StatusDict[StatusType.Poison].StatusValue -= 2;
        OnStatusChanged?.Invoke(StatusType.Poison, StatusDict[StatusType.Poison].StatusValue);
        CurrentHealth -= 1;

        CheckDie();

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        FxManager.Ins.DisplayFloatingDamage(target.transform, 2);
    }

    public void BleedDamage(Transform target)
    {
        if (StatusDict[StatusType.Bleed].StatusValue <= 0) return;
        int dmg = (int)(MaxHealth * 0.1 * StatusDict[StatusType.Bleed].StatusValue);
        CurrentHealth -= dmg;

        CheckDie();
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        ClearStatus(StatusType.Bleed);
        FxManager.Ins.DisplayFloatingDamage(target.transform, dmg);

    }

    private void CheckDie()
    {
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDeath?.Invoke();

        }
    }

    private void DisplayNearlyDiePanel()
    {
        if(CurrentHealth <= 30)
        {
            UIManager.Ins.GetUI<NearlyDie>().Open();
        }
    }
}

