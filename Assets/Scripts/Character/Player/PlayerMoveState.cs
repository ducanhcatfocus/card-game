using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState<PlayerBase>
{
    public void OnEnter(PlayerBase player)
    {
        player.ChangeAnim("Move");
    }

    public void OnExecute(PlayerBase player)
    {

    }

    public void OnExit(PlayerBase player)
    {

    }
}

