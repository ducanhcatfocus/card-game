using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<EnemyBase>
{
    public void OnEnter(EnemyBase enemy)
    {
        enemy.ChangeAnim("Idle");
    }

    public void OnExecute(EnemyBase enemy)
    {

    }

    public void OnExit(EnemyBase enemy)
    {

    }
}
