using System;
using Godot;

namespace PedroBulletV2.Scripts.Enemies;

public partial class Bullet : Area2D
{
	[Export] private int _speed = 100;

	[Export] private Sprite2D _petal1;
	[Export] private Sprite2D _petal2;

	private float _petalRotation = 0.1f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var random = new Random();
		_petal1.Visible = true;
		_petal2.Visible = false;
		if (random.Next(0, 2) == 1)
		{
			_petal1.Visible = false;
			_petal2.Visible = true;
		}

		_petalRotation = random.Next(-8, 8) / 10.0f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Position += new Vector2(1,0).Rotated(Rotation);
		Position += Transform.X * _speed * (float)delta;
		_petal1.Rotate(_petalRotation * (float)delta);
		_petal2.Rotate(_petalRotation * (float)delta);
	}

	void _on_kill_timer_timeout()
	{
		QueueFree();
	}
}
