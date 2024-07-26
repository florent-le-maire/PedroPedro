using Godot;

namespace PedroBulletV2.Scenes._3D;

public partial class PerlinNoise : Node3D
{
	[Export] private FastNoiseLite _perlinNoise;
	[Export] private Node3D _sphereRoot;
	[Export] private PackedScene _sphere;

	[Export] private float _amplitude = 1.0f;
	[Export] private float _speed = .2f;

	private float _time = 0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = -20; i < 20; i++)
		{
			for (int j = -20; j < 20; j++)
			{
				Node3D newSphere = (Node3D)_sphere.Instantiate();
				_sphereRoot.AddChild(newSphere);
				newSphere.Translate(new Vector3(i,0,j));
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_time += _speed;

		foreach (Node3D sphere in _sphereRoot.GetChildren())
		{
			float perlinValue = _perlinNoise.GetNoise2D( sphere.Position.X + _time, sphere.Position.Z);
			Vector3 move = new Vector3(0,perlinValue*_amplitude,0);
			sphere.Translate(move);
		}
	}
}
