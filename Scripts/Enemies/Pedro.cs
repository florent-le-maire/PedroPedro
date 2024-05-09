using Godot;

namespace PedroBulletV2.Scripts.Enemies;

public partial class Pedro : Node2D
{
	[Export] private PackedScene _bulletScene;
	private Timer _shootTimer;
	private Node2D _rotator;
	
	[Export] private int _rotateSpeed = 100;
	[Export] private float _shooterTimerWaitTime = 0.2f;
	[Export] private int _spawnPointCount = 4;
	[Export] private int _radius = 100;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_shootTimer = GetNode<Timer>("ShootTimer");
		_rotator = GetNode<Node2D>("Rotator");
		
		var step = 2 * Mathf.Pi / _spawnPointCount;
		for (int i = 0; i < _spawnPointCount; i++)
		{
			var spawnPoint = new Node2D();
			var pos = new Vector2(_radius, 0).Rotated(step * i);
			spawnPoint.Position = pos;
			spawnPoint.Rotation = pos.Angle();
			_rotator.AddChild(spawnPoint);
		}

		_shootTimer.WaitTime = _shooterTimerWaitTime;
		_shootTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var newRotation = _rotator.RotationDegrees + _rotateSpeed * delta;
		_rotator.RotationDegrees = (float)newRotation % 360;

	}

	void _on_shoot_timer_timeout()
	{
		foreach (Node2D child in _rotator.GetChildren())
		{
			var bullet = _bulletScene.Instantiate<Bullet>();
			GetTree().Root.AddChild(bullet);
			bullet.Position = child.GlobalPosition;
			bullet.Rotation = child.GlobalRotation;
			
		}
	}
}