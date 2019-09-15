using System;
using System.Collections.Generic;
using UnityEngine;

public enum eStateId
{
    NullId = 0,
    Empty,
    Idle,
    Move,
    Attack,

    //Child
    EmptyAttack,
    Attack1,
    Attack2,
    Attack3,
}

public enum eTransition
{
    EmptyToIdle = 0,
    IdleToMove,
    MoveToAttack,
    AttackToIdle,

    //Child
    EmptyAttackToAttack1,
    Attack1ToAttack2,
    Attack2ToAttack3,
}

public abstract class BaseState : MonoBehaviour
{

    public eStateId StateId { get; set; }
    public StateControl Control { get; set; }

    private StateControl _childControl;
    private readonly Dictionary<eTransition, eStateId> _stateDic = new Dictionary<eTransition, eStateId>();

    /// <summary>
    /// 添加转换条件
    /// </summary>
    /// <param name="trans">转换条件</param>
    /// <param name="id">目标状态Id</param>
    public void AddTransition(eTransition trans, eStateId id)
    {
        if (_stateDic.ContainsKey(trans)) return;
        _stateDic.Add(trans, id);
    }

    public void RemoveTransition(eTransition trans)
    {
        if (!_stateDic.ContainsKey(trans)) return;
        _stateDic.Remove(trans);
    }

    public void StartChildStateControl(eStateId defaultStateId)
    {
        _childControl = gameObject.GetComponent<StateControl>();
        if (_childControl == null)
            _childControl = gameObject.AddComponent<StateControl>();
        _childControl.enabled = true;
        _childControl.IsChildControl = true;
        _childControl.SetCurState(defaultStateId);
    }

    /// <summary>
    /// 开启子状态控制器
    /// </summary>
    /// <param name="defaultStateId">子状态的默认状态</param>
    /// <param name="returnData">子状态接受的来自父状态的信息</param>
    /// <param name="receiveData">子状态发送到父状态的委托</param>
    public void StartChildStateControl(eStateId defaultStateId, object receiveData, Action<object> returnData)
    {
        _childControl = gameObject.GetComponent<StateControl>();
        if (_childControl == null)
            _childControl = gameObject.AddComponent<StateControl>();
        _childControl.enabled = true;
        _childControl.IsChildControl = true;
        _childControl.SendAction = returnData;
        _childControl.ReceiveData = receiveData;
        _childControl.SetCurState(defaultStateId);
    }



    public void EndChildStateControl()
    {
        if (_childControl == null) return;
        _childControl.enabled = false;
        _childControl.ReceiveData = null;
        _childControl.SendData = null;
    }

    public eStateId GetStateIdByTrans(eTransition trans)
    {
        if (_stateDic.ContainsKey(trans)) return _stateDic[trans];
        Debug.Log("<color=red>GetStateIdByTrans Null </color>" + trans + " no found in StateId");
        return eStateId.NullId;
    }

    #region 状态生命周期

    public abstract void OnStart();

    public virtual void OnEnter() { }
    /// <summary>
    /// 进入状态
    /// 接收信息
    /// </summary>
    /// <param name="data">接上个状态OnExit中传递的信息</param>
    public virtual void OnEnter(in object data) { }

    /// <summary>
    /// 执行状态
    /// </summary>
    public virtual void OnAction() { }

    /// <summary>
    /// 更新行为
    /// </summary>
    public virtual void OnUpdate() { }

    public virtual void OnExit() { }
    /// <summary>
    /// 离开状态
    /// 发送信息
    /// </summary>
    /// <param name="data">传递信息到下个状态中的OnEnter</param>
    public virtual void OnExit(out object data) { data = null; }

    #endregion

}
