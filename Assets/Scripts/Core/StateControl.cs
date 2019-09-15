using System;
using UnityEngine;
using UnityEngine.Events;

public class StateControl : MonoBehaviour
{
    public bool IsChildControl { get; set; } = false;
    public object ReceiveData { get; set; } = null;
    public object SendData { get; set; } = null;
    public Action<object> SendAction { get; set; }

    private Transform _trans;
    private StateSystem _system;

    public void AddStateChangedListener(Action<eStateId> action)
    {
        _system.RemoveListener(action);
        _system.AddListener(action);
    }

    public void RemoveStateChangedListener(Action<eStateId> action)
    {
        _system.RemoveListener(action);
    }

    private void Awake()
    {

        _trans = transform;
        _system = new StateSystem(this);
        StateInit();
    }

    private void Start()
    {
        if (!IsChildControl)
        {
            //状态添加完成后设置一次默认状态(第一个子物体)
            SetCurState(_trans.GetChild(0).GetComponent<BaseState>().StateId);
        }
    }

    private void StateInit()
    {
        for (int i = 0; i < _trans.childCount; i++)
        {
            BaseState state = _trans.GetChild(i).GetComponent<BaseState>();
            _system.AddState(state);
        }
    }


    public void PerformTransition(eTransition trans)
    {
        _system.PerformTransition(trans);
    }

    /// <summary>
    /// 执行转换条件
    /// </summary>
    /// <param name="trans">转换条件</param>
    /// <param name="returnData">子状态返回给父状态的数据</param>
    public void PerformTransition(eTransition trans, object returnData)
    {
        if (returnData == null)
            _system.PerformTransition(trans);
        else
            SendAction(returnData);
    }

    /// <summary>
    /// 强制切换到某一状态
    /// </summary>   
    public void SetCurState(eStateId eStateId)
    {
        _system?.SetCurState(eStateId);
    }

    /// <summary>
    /// 获取当前StateID
    /// </summary>
    /// <returns></returns>
    public eStateId GetCurStateId()
    {
        return _system.CurStateId;
    }

    private void Update()
    {
        //Debug.LogError("CurrentState---> " + GetCurStateId());
        _system.CurState?.OnUpdate();
    }
}

