using Godot;
using PedroBulletV2.Scenes.Pattern.Attacks;

namespace PedroBulletV2.Scripts.Pattern;

public class PatternSchema
{
	private string _name;
	private int _difficulty;
	private float _duration;
	private float _frequency;
	private float _rotationSpeed;
	
}

public partial class Pattern : Node2D
{
	[Export] private PackedScene _rotator;
	[Export] private PackedScene _spawner;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (_rotator != null && _spawner != null)
		{
			var rotator = _rotator.Instantiate<Rotator>();
			var spawner = _spawner.Instantiate<Spawner>();
			rotator.SetValues(500,2);
			rotator.Position = new Vector2(0, 0);
			AddChild(rotator);
			spawner.Position = new Vector2(0, 0);
			spawner.SetValues(0.5f, 10);
			rotator.AddChild(spawner);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}