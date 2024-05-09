using Godot;
using System;

public partial class State : Node
{
    [Signal]
    public delegate void TransitionedEventHandler(State state, State newStateName);

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update(double delta)
    {
    }

    public void PhysicsUpdate(double delta)
    {
    }
}