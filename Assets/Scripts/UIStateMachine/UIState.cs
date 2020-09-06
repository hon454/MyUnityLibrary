using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIState : MonoBehaviour
{
    public abstract int StateID { get; }

    private UIStateMachine _stateMachine;
    public UIStateMachine UIStateMachine => _stateMachine;

    public virtual string GetStateName()
    {
        return "UIState";
    }

    private void Awake()
    {
        _stateMachine = this.gameObject.GetComponentInParent<UIStateMachine>();
        if (_stateMachine == null)
        {
            Debug.LogWarning($"[{gameObject.name}]: Requires Parent GameObject to have a UIStateMachine Component Attached");
        }
        else
        {
            _stateMachine.AddState(this);
        }
    }
}
