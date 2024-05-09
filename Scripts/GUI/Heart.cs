using Godot;
using System;

public partial class Heart : Panel
{
	[Export] private Sprite2D _sprite2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Update(Boolean whole)
	{
		_sprite2D.Visible = whole;
	}
}
