using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : IState<EnemyBase> 
{
    public void OnEnter(EnemyBase enemy)
    {
        enemy.ChangeAnim("Move");
    }

    public void OnExecute(EnemyBase enemy)
    {

    }

    public void OnExit(EnemyBase enemy)
    {

    }
}
