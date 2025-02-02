using System;
using Godot;
using PedroBulletV2.Scripts.Pattern;

namespace PedroBulletV2.Scenes.Enemies;

public partial class PedroV3 : CharacterBody2D
{
    [Export] private int _rotateSpeed = 100;
    [Export] private float _shooterTimerWaitTime = 0.2f;
    [Export] private int _spawnPointCount = 4;
    [Export] private Conductor _conductor;
    [Export] private PatternManager _patternManager;


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
        _patternManager.CreatePattern((int)i);
        // var bullet = _bulletScene.Instantiate<Bullet>();
        switch (i)
        {
            case 1:
                
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
     
        }
    }
}