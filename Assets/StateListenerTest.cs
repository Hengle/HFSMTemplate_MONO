using UnityEngine;

public class StateListenerTest : MonoBehaviour
{
    StateControl control;

    private void Start()
    {
        control = GetComponent<StateControl>();
        control.AddStateChangedListener(ABC);
    }

    [ContextMenu("DODODO")]
    private void DoDoDo()
    {
        Debug.Log(control.GetCurStateId());
    }

    private void ABC(eStateId eStateId)
    {
        Debug.Log("StateListenerTest ABC---> " + eStateId);
        Debug.Log("StateListenerTest ABC control.GetCurStateId--->" + control.GetCurStateId());
        System.Diagnostics.Trace.WriteLine("StateListenerTest---> " + eStateId);
    }
}
