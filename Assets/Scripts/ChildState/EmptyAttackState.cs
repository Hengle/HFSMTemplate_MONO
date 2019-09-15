using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyAttackState : BaseState
{

    public override void OnStart()
    {
        StateId = eStateId.EmptyAttack;
        AddTransition(eTransition.EmptyAttackToAttack1, eStateId.Attack1);
    }

    public override void OnEnter()
    {
        Debug.Log(StateId);
        Debug.Log(Control.ReceiveData);
    }

    public override void OnAction()
    {
        Control.PerformTransition(eTransition.EmptyAttackToAttack1);
    }
}
