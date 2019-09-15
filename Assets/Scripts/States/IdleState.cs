using UnityEngine;

public class IdleState : BaseState
{

    public override void OnStart()
    {
        StateId = eStateId.Idle;
        AddTransition(eTransition.IdleToMove, eStateId.Move);
    }

    public override void OnEnter()
    {
        Debug.Log(StateId);
    }

    public override void OnAction()
    {
        Invoke("DelayTransition", 1f);
        //DelayTransition();
    }

    public override void OnExit()
    {
        CancelInvoke();
    }

    private void DelayTransition()
    {
        Control.PerformTransition(eTransition.IdleToMove);
    }
}
