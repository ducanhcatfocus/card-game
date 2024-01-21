using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerBase : CharacterBase
{
    private IState<PlayerBase> currentState;

    protected override void Start()
    {

        ChangeState(new PlayerIdleState());

    }

    void Update()
    {
        currentState?.OnExecute(this);


    }

    public void ChangeState(IState<PlayerBase> state)
    {
        currentState?.OnExit(this);

        currentState = state;

        currentState?.OnEnter(this);
    }

}
