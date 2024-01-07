using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [Header("Enemy Base References")]
    [SerializeField] protected EnemyDataBase enemyCharacterData;
    [SerializeField] Animator animator;


    public EnemyDataBase EnemyCharacterData => enemyCharacterData;
    public override void BuildCharacter()
    {
        base.BuildCharacter();
        CharacterStats = new CharacterStats(EnemyCharacterData.MaxHealth, CharacterCanvas);
        CharacterStats.SetCurrentHealth(CharacterStats.CurrentHealth);
        CharacterStats.OnDeath += OnDeath;
    }

    

    protected override void OnDeath()
    {
        base.OnDeath();
        AudioManager.PlayEnemyDieSound();
        animator.SetBool("isDie", true);
       
    }

    public void CombatWin()
    {
        CombatManager.OnEnemyDeath();
    }


   
    //Enemy Anim
}
