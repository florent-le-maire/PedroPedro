using Godot;
using System;

public partial class HealthBar : HBoxContainer
{
	[Export] private PackedScene _heart; 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetMaxHearts(int max)
	{
		for (int i = 0; i < max; i++)
		{
			var heart = _heart.Instantiate<Heart>();
			AddChild(heart);
		}
	}

	public void UpdateHearts(int currentHealth)
	{
		var heartsNodes = GetChildren();
		for (int i = 0; i < currentHealth; i++)
		{
			Heart heart = (Heart)heartsNodes[i];
			heart.Update(true);
		}
		for (int i = currentHealth; i < heartsNodes.Count; i++)
		{
			Heart heart = (Heart)heartsNodes[i];
			heart.Update(false);
		}
	}
}
