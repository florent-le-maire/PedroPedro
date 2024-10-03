using Godot;

namespace PedroBulletV2.Scripts;

public partial class Game : Node2D
{
	// [Export] private AudioStreamPlayer2D _music;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// _music.Play();
		// _music.Finished += _CheckIfGameEnd;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _CheckIfGameEnd()
	{
		GD.Print("WOOOOOO");
	}
}
