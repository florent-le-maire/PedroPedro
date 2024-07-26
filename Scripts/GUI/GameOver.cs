using Godot;
using System;

public partial class GameOver : Control
{
	private PackedScene _mainMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string scenePath = "res://Scenes/OptionsMenu/MainMenu.tscn";

		_mainMenu = ResourceLoader.Load<PackedScene>(scenePath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void _on_restart_pressed()
	{
		GetTree().Paused = false;
		Visible = false;
		foreach (Node obj in GetTree().GetNodesInGroup("InstancedObjects"))
		{
			obj.QueueFree();
		}
		GetTree().ReloadCurrentScene();
	}

	void _on_main_menu_pressed()
	{

		GetTree().Paused = false;
		GetTree().ChangeSceneToPacked(_mainMenu);
	}
}
