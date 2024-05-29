using System;
using Godot;

namespace PedroBulletV2.Scripts.Enemies;

public partial class Pedro : CharacterBody2D
{
	[Export] private BulletPattern _bulletPattern;

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
		MoveAndSlide();
	}

	void _on_timer_timeout()
	{
		var rand = new Random();
		int rotationSpeed = rand.Next(0, 100);
		int spawnPoint = rand.Next(1, 100);
		float shootWaitTime = rand.NextSingle() + 0.1f;
		_bulletPattern.ChangeParameter(new PatternInformation(rotationSpeed,shootWaitTime,spawnPoint,10));
	}
}
