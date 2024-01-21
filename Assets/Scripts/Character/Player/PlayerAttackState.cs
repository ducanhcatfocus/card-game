using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState<PlayerBase>
{
    public void OnEnter(PlayerBase player)
    {
        player.ChangeAnim("Attack");
    }


    public void OnExecute(PlayerBase player)
    {

    }
    public void OnExit(PlayerBase player)
    {

    }
}
