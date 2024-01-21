using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState<PlayerBase>
{


        public void OnEnter(PlayerBase player)
        {
            player.ChangeAnim("Idle");
        }

        public void OnExecute(PlayerBase player)
        {

        }

        public void OnExit(PlayerBase player)
        {

        }
}
