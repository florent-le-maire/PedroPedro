using Godot;
using System;

public partial class StateMachine : State
{
    // private Dictionary<string, State> _states;
    private State _currentState = null;

    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is State s)
            {
                // _states[child.Name] = s;
                // s.Transitioned = on_child_transition;
            }
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
        
    }
}
