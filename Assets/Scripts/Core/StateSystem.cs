using System;
using System.Collections.Generic;
using UnityEngine;

public class StateSystem
{
    private readonly StateControl _control;
    private readonly List<BaseState> _stateList;
    private event Action<eStateId> OnStateChangedEvent;

    public eStateId CurStateId { get; private set; }
    public BaseState CurState { get; private set; }

    public StateSystem(StateControl control)
    {
        _control = control;
        _stateList = new List<BaseState>();
    }

    public void AddListener(Action<eStateId> action)
    {
        OnStateChangedEvent += action;
    }

    public void RemoveListener(Action<eStateId> action)
    {
        OnStateChangedEvent -= action;
    }

    public void AddState(BaseState state)
    {
        if (_stateList.Contains(state)) return;
        _stateList.Add(state);
        state.Control = _control;
        state.OnStart();
    }

    public void RemoveState(BaseState state)
    {
        if (!_stateList.Contains(state)) return;
        _stateList.Remove(state);
    }

    public void PerformTransition(eTransition trans)
    {
        eStateId id = CurState.GetStateIdByTrans(trans);
        //处理状态转换错误
        if (id == eStateId.NullId)
        {
            Debug.LogError("CurState -> " + "<color=red>" + CurStateId + "</color>" + " no found the eTransition -> " + "<color=green>" + trans + "</color>");
            throw new UnityException();
        }

        foreach (var state in _stateList)
        {
            if (state.StateId != id) continue;
            CurState.OnExit();
            CurState.OnExit(out object data);
            CurState = state;
            CurStateId = state.StateId;
            OnStateChangedEvent?.Invoke(CurStateId);
            CurState.OnEnter();
            CurState.OnEnter(in data);
            CurState.OnAction();
            return;
        }
        Debug.LogError("<color=red>PerformTransition </color> You don't found a state " + id + " from _stateList");
    }

    public void SetCurState(eStateId eStateId)
    {
        if (eStateId == eStateId.NullId)
        {
            Debug.Log("You couldn't set a stateId to Null");
            throw new UnityException();
        }

        foreach (var state in _stateList)
        {
            if (state.StateId != eStateId) continue;
            if (CurState == null)
            {
                CurState = state;
                CurStateId = state.StateId;
                OnStateChangedEvent?.Invoke(CurStateId);
                CurState.OnEnter();
                CurState.OnAction();
                return;
            }
            else
            {
                CurState.OnExit();
                CurState.OnExit(out object data);
                CurState = state;
                CurStateId = state.StateId;
                OnStateChangedEvent?.Invoke(CurStateId);
                CurState.OnEnter();
                CurState.OnEnter(in data);
                CurState.OnAction();
                return;
            }
        }
        Debug.LogError("<color=red>SetCurState </color> You don't found a state " + eStateId + " from _stateList");
    }
}
