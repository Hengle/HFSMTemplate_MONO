using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3State : BaseState
{
    public override void OnStart()
    {
        StateId = eStateId.Attack3;
    }

    public override void OnEnter()
    {
        Debug.Log(StateId);
    }

    public override void OnAction()
    {
        //Control.PerformTransition(eTransition.Attack1ToAttack2);
    }
}
