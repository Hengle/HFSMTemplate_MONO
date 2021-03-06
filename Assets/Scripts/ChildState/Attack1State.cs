﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1State : BaseState
{
    public override void OnStart()
    {
        StateId = eStateId.Attack1;
        AddTransition(eTransition.Attack1ToAttack2, eStateId.Attack2);
    }

    public override void OnEnter()
    {
        Debug.Log(StateId);
    }

    public override void OnAction()
    {
        Control.PerformTransition(eTransition.Attack1ToAttack2);
    }
}
