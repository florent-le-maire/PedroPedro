using Godot;

namespace PedroBulletV2.Scripts.States;

public partial class State : Node
{
    [Signal]
    public delegate void TransitionedEventHandler(string key);

    public StateMachine Fsm;

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public new virtual void Ready()
    {
    }

    public virtual void Update(double delta)
    {
    }

    public virtual void PhysicsUpdate(double delta)
    {
    }    
    public virtual void HandleInput(double delta)
    {
    }
}