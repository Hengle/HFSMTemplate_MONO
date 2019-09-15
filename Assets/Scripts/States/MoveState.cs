using System.Threading;
using UnityEngine;

public class MoveState : BaseState
{

    public override void OnStart()
    {
        StateId = eStateId.Move;
        AddTransition(eTransition.MoveToAttack, eStateId.Attack);
    }

    public override void OnEnter()
    {
        Debug.Log(StateId);
    }

    public override void OnAction()
    {
        //Invoke("DelayTransition", 1f);
        DelayTransition();
    }

    public override void OnExit(out object data)
    {
        data = StateId + " send to ";
        //CancelInvoke();
    }

    private void DelayTransition()
    {
        Control.PerformTransition(eTransition.MoveToAttack);
    }
}
