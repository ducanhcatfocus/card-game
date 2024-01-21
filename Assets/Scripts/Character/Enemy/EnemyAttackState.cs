using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<EnemyBase>
{
    public void OnEnter(EnemyBase enemy)
    {
        enemy.ChangeAnim("Attack");
    }

    public void OnExecute(EnemyBase enemy)
    {

    }

    public void OnExit(EnemyBase enemy)
    {

    }
}