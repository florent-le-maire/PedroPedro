using Godot;

namespace PedroBulletV2.Scripts.Enemies;

public partial class Bullet : Area2D
{
	[Export] private int _speed = 100;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Position += new Vector2(1,0).Rotated(Rotation);
		Position += Transform.X * _speed * (float)delta;
	}

	void _on_kill_timer_timeout()
	{
		QueueFree();
	}
}