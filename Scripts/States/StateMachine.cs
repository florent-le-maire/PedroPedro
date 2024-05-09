using Godot;
using Godot.Collections;

namespace PedroBulletV2.Scripts.States;

public partial class StateMachine : Node
{
    private Dictionary<string, State> _states;
    private State _currentState;

    [Export] private State _initialState;

    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is State s)
            {
                string childName = child.Name;
                _states[childName.ToLower()] = s;
                s.Transitioned += on_child_transition;
            }
        }

        if (_initialState != null)
        {
            _initialState.Enter();
            _currentState = _initialState;
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

    void on_child_transition(State state, State newStateName)
    {
        if (state != _currentState)
        {
            return;
        }

        string stateName = newStateName.Name;
        if (!_states.TryGetValue(stateName.ToLower(), out State newState))
        {
            return;
        }

        if (_currentState != null)
        {
            _currentState.Exit();
        }

        newState.Enter();
        _currentState = newState;
    }
}