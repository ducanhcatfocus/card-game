using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    private IState<EnemyBase> currentState;


    protected override void Start()
    {

        ChangeState(new EnemyIdleState());

    }

    void Update()
    {
        currentState?.OnExecute(this);
    }

    public void ChangeState(IState<EnemyBase> state)
    {
        currentState?.OnExit(this);

        currentState = state;

        currentState?.OnEnter(this);
    }
}
