using System;
using UnityEngine;

public class AttackState : BaseState
{
    public override void OnStart()
    {
        StateId = eStateId.Attack;
        AddTransition(eTransition.AttackToIdle, eStateId.Idle);
    }

    public override void OnEnter(in object data)
    {
        Debug.Log(StateId);
        Debug.Log("<color=green>" + StateId + " </color>" + data + StateId.ToString() + " Message");
        StartChildStateControl(eStateId.EmptyAttack, "<color=red>ChildState Receive Data</color>", OnAction);
    }

    private void OnAction(object data)
    {
        Debug.Log(data);
        //Invoke("DelayTransition", 1f);
        DelayTransition();
    }

    public override void OnExit()
    {
        EndChildStateControl();
        //CancelInvoke();
    }

    void DelayTransition()
    {
        Control.PerformTransition(eTransition.AttackToIdle);
    }


}
