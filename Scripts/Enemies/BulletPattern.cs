using Godot;

namespace PedroBulletV2.Scripts.Enemies;

public struct PatternInformation
{
    public PatternInformation(int rotateSpeed, float shooterTimerWaitTime, int spawnPointCount, int radius)
    {
        RotateSpeed = rotateSpeed;
        ShooterTimerWaitTime = shooterTimerWaitTime;
        SpawnPointCount = spawnPointCount;
        Radius = radius;
    }

    public int RotateSpeed { get; }
    public float ShooterTimerWaitTime { get; }
    public int SpawnPointCount { get; }
    public int Radius { get; }
}

public partial class BulletPattern : Node2D
{
    [Export] private PackedScene _bulletScene;
    [Export] private Node2D _rotator;

    [Export] private int _rotateSpeed = 100;
    [Export] private float _shooterTimerWaitTime = 1.0f;
    [Export] private int _spawnPointCount = 4;
    [Export] private int _radius = 100;

    [Export] private Conductor _conductor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        InitialisePattern();
        _conductor.Play();
    }

    private void InitialisePattern()
    {
        foreach (var child in _rotator.GetChildren())
        {
            child.QueueFree();
        }

        var step = 2 * Mathf.Pi / _spawnPointCount;
        for (int i = 0; i < _spawnPointCount; i++)
        {
            var spawnPoint = new Node2D();
            var pos = new Vector2(_radius, 0).Rotated(step * i);
            spawnPoint.Position = pos;
            spawnPoint.Rotation = pos.Angle();
            _rotator.AddChild(spawnPoint);
        }

        _conductor.Beat += BeatWasEmit;
        // _conductor.Measure += MeasureWasEmit;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var newRotation = _rotator.RotationDegrees + _rotateSpeed * delta;
        _rotator.RotationDegrees = (float)newRotation % 360;
    }

    void BeatWasEmit(float i)
    {
        // var bullet = _bulletScene.Instantiate<Bullet>();
        // GetTree().Root.AddChild(bullet);
        // bullet.AddToGroup("InstancedObjects");
        // bullet.Position = new Vector2(0, 50);
        foreach (Node2D child in _rotator.GetChildren())
        {
            var bullet = _bulletScene.Instantiate<Bullet>();
            GetTree().Root.AddChild(bullet);
            bullet.AddToGroup("InstancedObjects");
            bullet.Position = child.GlobalPosition;
            bullet.Rotation = child.GlobalRotation;
        }
    }

    void MeasureWasEmit(float i)
    {
        var bullet = _bulletScene.Instantiate<Bullet>();
        switch (i)
        {
            case 1:
                GetTree().Root.AddChild(bullet);
                bullet.AddToGroup("InstancedObjects");
                bullet.Position = new Vector2(0, 75);
                break;
            case 2:
                GetTree().Root.AddChild(bullet);
                bullet.AddToGroup("InstancedObjects");
                bullet.Position = new Vector2(0, 100);
                break;
            case 3:
                GetTree().Root.AddChild(bullet);
                bullet.AddToGroup("InstancedObjects");
                bullet.Position = new Vector2(0, 125);
                break;
            case 4:
                GetTree().Root.AddChild(bullet);
                bullet.AddToGroup("InstancedObjects");
                bullet.Position = new Vector2(0, 150);
                break;
            default:
                break;
        }
    }

    // void _on_shoot_timer_timeout()
    // {
    //     foreach (Node2D child in _rotator.GetChildren())
    //     {
    //         var bullet = _bulletScene.Instantiate<Bullet>();
    //         GetTree().Root.AddChild(bullet);
    //         bullet.AddToGroup("InstancedObjects");
    //         bullet.Position = child.GlobalPosition;
    //         bullet.Rotation = child.GlobalRotation;
    //     }
    // }

    public void ChangeParameter(PatternInformation patternInformation)
    {
        // _rotateSpeed = patternInformation.RotateSpeed;
        // _shooterTimerWaitTime = patternInformation.ShooterTimerWaitTime;
        // _spawnPointCount = patternInformation.SpawnPointCount;
        // _radius = patternInformation.Radius;

        // InitialisePattern();
    }


    private void Shoot()
    {
        foreach (Node2D child in _rotator.GetChildren())
        {
            var bullet = _bulletScene.Instantiate<Bullet>();
            GetTree().Root.AddChild(bullet);
            bullet.AddToGroup("InstancedObjects");
            bullet.Position = child.GlobalPosition;
            bullet.Rotation = child.GlobalRotation;
        }
    }
}
