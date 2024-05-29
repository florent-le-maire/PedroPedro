using Godot;

namespace PedroBulletV2.Scripts.Player;

public partial class Dash : Node2D
{

	[Export] private Timer _dashTimer;
	[Export] private Timer _dashCooldown;

	public bool IsDashing = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void StartDash(float duration)
	{
		_dashTimer.WaitTime = duration;
		_dashTimer.Start();
		IsDashing = true;
	}

	public bool IsInCooldown()
	{
		return _dashCooldown.IsStopped();
	}

	void _on_dash_timer_timeout()
	{
		IsDashing = false;
		_dashCooldown.Start();
	}
}
