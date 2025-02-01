using Godot;
using PedroBulletV2.Scripts.Enemies;
using Timer = Godot.Timer;

namespace PedroBulletV2.Scenes.Pattern.Attacks;

public partial class Spawner : Node2D
{
	[Export] private PackedScene _bulletScene;
	[Export] public float Duration = 1;
	[Export] public float Frequency = 0.1f;
	
	public void SetValues(float frequency, float duration)
	{
		Frequency = frequency;
		Duration = duration;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var durationTimer = new Timer();
		durationTimer.WaitTime = Duration;
		var frequencyTimer = new Timer();
		frequencyTimer.WaitTime = Frequency;
		AddChild(durationTimer);
		AddChild(frequencyTimer);
		
		
		if (durationTimer != null && frequencyTimer != null)
		{
			durationTimer.Start();
			frequencyTimer.Start();

			durationTimer.Timeout += DurationTimerTimeout;
			frequencyTimer.Timeout += FrequencyTimerTimeout;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	void DurationTimerTimeout()
	{
		QueueFree();
	}	
	void FrequencyTimerTimeout()
	{
		var bullet = _bulletScene.Instantiate<Bullet>();
		GetTree().Root.AddChild(bullet);
		bullet.AddToGroup("InstancedObjects");
		bullet.Position = GlobalPosition;
		bullet.Rotation = GlobalRotation;
	}
}