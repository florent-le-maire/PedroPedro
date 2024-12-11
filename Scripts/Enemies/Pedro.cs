using System;
using Godot;

namespace PedroBulletV2.Scripts.Enemies;

public partial class Pedro : CharacterBody2D
{
    [Export] private BulletPattern _bulletPattern;

    [Export] private int _rotateSpeed = 100;
    [Export] private float _shooterTimerWaitTime = 0.2f;
    [Export] private int _spawnPointCount = 4;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndCollide(Velocity * (float)delta);
    }

    void _on_timer_timeout()
    {
        var rand = new Random();
        // int rotationSpeed = rand.Next(0, 100);
        // int spawnPoint = rand.Next(1, 100);
        // float shootWaitTime = rand.NextSingle() + 0.1f;
        int rotationSpeed = _rotateSpeed;
        int spawnPoint = _spawnPointCount;
        float shootWaitTime = _shooterTimerWaitTime;
        // _bulletPattern.ChangeParameter(new PatternInformation(rotationSpeed,shootWaitTime,spawnPoint,100));
    }
}
