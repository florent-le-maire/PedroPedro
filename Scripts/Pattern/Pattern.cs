using Godot;
using PedroBulletV2.Scenes.Pattern.Attacks;

namespace PedroBulletV2.Scripts.Pattern;

public class PatternSchema(
	string name,
	int difficulty,
	float radius,
	float spawnPointCount,
	float duration,
	float frequency,
	float rotationSpeed,
	int rotationDirection)
{
	public string Name {get;set;} = name;
	public int Difficulty {get;set;} = difficulty;
	public float Radius {get;set;} = radius;
	public float SpawnPointCount {get;set;} = spawnPointCount;
	public float Duration {get;set;} = duration;
	public float Frequency {get;set;} = frequency;
	public float RotationSpeed {get;set;} = rotationSpeed;
	public int RotationDirection {get;set ;} = rotationDirection;
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
			// _generateCirclePattern(new PatternSchema( "CirclePulse", 1, 100, 30, 20, 0.5f, 0));
		}
	}
	
	
	public void GenerateCirclePattern(PatternSchema schema)
	{

	
		for (int i = 0; i < 1; i++)
		{
			var rotator = _rotator.Instantiate<Rotator>();
			rotator.SetValues(schema.RotationSpeed * schema.RotationDirection,schema.Duration);
			rotator.Position = new Vector2(0, 0);
			AddChild(rotator);
			var step = 2 * Mathf.Pi / schema.SpawnPointCount;
			for (int j = 0; j < schema.SpawnPointCount; j++)
			{
				var spawner = _spawner.Instantiate<Spawner>();
				var pos = new Vector2(schema.Radius, 0).Rotated(step * j);

				spawner.Position = pos;
				spawner.Rotation = pos.Angle();
				spawner.SetValues(schema.Frequency, schema.Duration);
				rotator.AddChild(spawner);
			}
		}
	}
}