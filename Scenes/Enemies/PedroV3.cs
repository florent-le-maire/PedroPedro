using System;
using Godot;

namespace PedroBulletV2.Scenes.Enemies;

public partial class PedroV3 : CharacterBody2D
{
    [Export] private int _rotateSpeed = 100;
    [Export] private float _shooterTimerWaitTime = 0.2f;
    [Export] private int _spawnPointCount = 4;
    [Export] private Conductor _conductor;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _conductor.Play();
        _conductor.Beat += BeatWasEmit;
        _conductor.Measure += MeasureWasEmit;
    }
    void BeatWasEmit(float i)
    {
        // foreach (Node2D child in _rotator.GetChildren())
        // {
        //     var bullet = _bulletScene.Instantiate<Bullet>();
        //     GetTree().Root.AddChild(bullet);
        //     bullet.AddToGroup("InstancedObjects");
        //     bullet.Position = child.GlobalPosition;
        //     bullet.Rotation = child.GlobalRotation;
        // }
    }
    
    void MeasureWasEmit(float i)
    {
        // var bullet = _bulletScene.Instantiate<Bullet>();
        // switch (i)
        // {
        //     case 1:
        //         GetTree().Root.AddChild(bullet);
        //         bullet.AddToGroup("InstancedObjects");
        //         bullet.Position = new Vector2(0, 75);
        //         break;
        //     case 2:
        //         GetTree().Root.AddChild(bullet);
        //         bullet.AddToGroup("InstancedObjects");
        //         bullet.Position = new Vector2(0, 100);
        //         break;
        //     case 3:
        //         GetTree().Root.AddChild(bullet);
        //         bullet.AddToGroup("InstancedObjects");
        //         bullet.Position = new Vector2(0, 125);
        //         break;
        //     case 4:
        //         GetTree().Root.AddChild(bullet);
        //         bullet.AddToGroup("InstancedObjects");
        //         bullet.Position = new Vector2(0, 150);
        //         break;
        //     default:
        //         break;
        // }
    }
}