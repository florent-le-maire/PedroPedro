using System;
using Godot;

namespace PedroBulletV2.Scripts.States;

public partial class EnemyIdle : State
{

    [Export] private CharacterBody2D _enemy;
    [Export] private float _moveSpeed = 10;
    
    private Vector2 _moveDirection;
    private double _wanderTime;

    private void RandomizeWander()
    {
        Random random = new Random();
        _moveDirection = new Vector2(random.Next(-1,1), random.Next(-1,1)).Normalized();
        _wanderTime = random.Next(1, 3);
    }

    public override void Enter()
    {
        RandomizeWander();
    }    
    public override void Update(double delta)
    {
        if (_wanderTime > 0)
        {
            _wanderTime -= delta;
        }
        else
        {
            RandomizeWander();
        }
    }
    
    public override void PhysicsUpdate(double delta)
    {
        if (_enemy != null)
        {
            _enemy.Velocity = _moveDirection * _moveSpeed;
        }
    }
}