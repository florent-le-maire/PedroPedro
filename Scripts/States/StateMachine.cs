using Godot;
using Godot.Collections;

namespace PedroBulletV2.Scripts.States;

public partial class StateMachine : Node
{
    private Dictionary<string, State> _states;
    private State _currentState;

    [Export] private NodePath _initialState;

    public override void _Ready()
    {
        _states = new Dictionary<string, State>();
        

        foreach (var child in GetChildren())
        {
            if (child is State s)
            {
                string childName = child.Name;
                _states.Add(childName.ToLower(),s);
                s.Fsm = this;
                s.Ready();
                s.Exit(); // Reset all states
                s.Transitioned += TransitionTo;
            }
        }
        
        if (_initialState != null)
        {
            _currentState = GetNode<State>(_initialState);
            _currentState.Enter();
        }
    }

    public override void _Process(double delta)
    {
        if (_currentState != null)
        {
            _currentState.Update(delta);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_currentState != null)
        {
            _currentState.PhysicsUpdate(delta);
        }
    }

    public void TransitionTo(string key)
    {
        if(!_states.ContainsKey(key) || _currentState == _states[key])
            return;
        _currentState.Exit();
        _currentState = _states[key];
        _currentState.Enter();
    }

    // void on_child_transition(State state, State newStateName)
    // {
    //     if (state != _currentState)
    //     {
    //         return;
    //     }
    //
    //     string stateName = newStateName.Name;
    //     if (!_states.TryGetValue(stateName.ToLower(), out State newState))
    //     {
    //         return;
    //     }
    //
    //     if (_currentState != null)
    //     {
    //         _currentState.Exit();
    //     }
    //
    //     newState.Enter();
    //     _currentState = newState;
    // }
}