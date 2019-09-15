using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2State : BaseState
{
    public override void OnStart()
    {
        StateId = eStateId.Attack2;
        AddTransition(eTransition.Attack2ToAttack3, eStateId.Attack3);
    }

    public override void OnEnter()
    {
        Debug.Log(StateId);
    }

    public override void OnAction()
    {
        Control.PerformTransition(eTransition.Attack2ToAttack3, "<color=blue>Attack2 Send Message</color>");
    }
}
