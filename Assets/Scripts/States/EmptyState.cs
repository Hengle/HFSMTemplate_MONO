using UnityEngine;

public class EmptyState : BaseState
{

    public override void OnStart()
    {
        StateId = eStateId.Empty;
        AddTransition(eTransition.EmptyToIdle, eStateId.Idle);
    }

    public override void OnEnter()
    {
        Debug.Log("Empty");

    }
    public override void OnAction()
    {
        //Invoke("DelayTransition", 1f);
        DelayTransition();
    }

    public override void OnExit()
    {
        //CancelInvoke();
    }

    void DelayTransition()
    {
        Control.PerformTransition(eTransition.EmptyToIdle);
    }
}
