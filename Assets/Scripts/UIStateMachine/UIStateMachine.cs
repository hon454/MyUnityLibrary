using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIStateMachine : MonoBehaviour
{
    private Stack<UIState> _uiStack;
    private Dictionary<int, UIState> _uiStates;

    [SerializeField] private UIState _initialState = null;

    private void Start()
    {
        ValidateStateMachine();
        if (_initialState != null)
        {
            if (ContainState(_initialState.StateID))
            {
                PushState(_initialState.StateID);
            }
            else
            {
                Debug.LogWarning($"[{gameObject.name}]: Initial State value is not childed to the UIStateMachine");
            }
        }
    }

    private void Update()
    {
        UIState currState = PeekState();
        if (currState != null)
        {
            OnUpdate(currState);
        }
    }

    public void PushState(int stateID)
    {
        ValidateStateMachine();

        if (!ContainState(stateID))
        {
            return;
        }

        UIState prevState = PeekState();
        if (prevState != null)
        {
            OnDefocus(prevState);
        }

        UIState nextState = _uiStates[stateID];
        if (nextState != null)
        {
            _uiStack.Push(nextState);
            OnEnter(nextState);
            OnFocus(nextState);
        }
    }

    public UIState PopState()
    {
        ValidateStateMachine();

        if (_uiStack.Count == 0)
        {
            return null;
        }

        UIState prevState = _uiStack.Pop();
        if (prevState != null)
        {
            OnDefocus(prevState);
            OnExit(prevState);
        }

        UIState nextState = PeekState();
        if (nextState != null)
        {
            OnFocus(nextState);
        }
        
        return prevState;
    }

    public UIState PeekState()
    {
        ValidateStateMachine();

        if (_uiStack.Count > 0)
        {
            return _uiStack.Peek();
        }

        return null;
    }

    public bool ContainState(int stateID)
    {
        ValidateStateMachine();
        if (!_uiStates.ContainsKey(stateID))
        {
            Debug.LogWarning($"[{gameObject.name}: Pushed StateID: {stateID} does not existed in the UIStateMachine]");
            return false;
        }

        return true;
    }

    public void ClearStates()
    {
        ValidateStateMachine();

        UIState prevState = PeekState();
        if (prevState != null)
        {
            OnDefocus(prevState);
        }

        while (_uiStack.Count > 0)
        {
            UIState tempState = _uiStack.Pop();
            OnExit(tempState);
        }
        
        _uiStack.Clear();
    }

    public void SetState(int stateID)
    {
        if (ContainState(stateID))
        {
            ClearStates();
            PushState(stateID);
        }
    }

    private void ValidateStateMachine()
    {
        if (_uiStack == null)
        {
            _uiStack = new Stack<UIState>();
        }

        if (_uiStates == null)
        {
            _uiStates = new Dictionary<int, UIState>();
        }
    }

    public void AddState(UIState state)
    {
        ValidateStateMachine();
        if (state == null)
        {
            Debug.LogWarning($"[{gameObject.name}: Adding null state to UIStateMachine");
            return;
        }

        if (!_uiStates.ContainsKey(state.StateID))
        {
            _uiStates.Add(state.StateID, state);
        }
        
        OnAwake(state);
    }

    private void OnAwake(UIState state)
    {
        IOnStateAwake i = state as IOnStateAwake;
        i?.OnAwake();
    }
    
    private void OnUpdate(UIState state)
    {
        IOnStateUpdate i = state as IOnStateUpdate;
        i?.OnUpdate();
    }

    private void OnEnter(UIState state)
    {
        IOnStateEnter i = state as IOnStateEnter;
        i?.OnEnter();
    }
    
    private void OnExit(UIState state)
    {
        IOnStateExit i = state as IOnStateExit;
        i?.OnExit();
    }
    
    private void OnFocus(UIState state)
    {
        IOnStateFocus i = state as IOnStateFocus;
        i?.OnFocus();
    }
    
    private void OnDefocus(UIState state)
    {
        IOnStateDefocus i = state as IOnStateDefocus;
        i?.OnDefocus();
    }
}
