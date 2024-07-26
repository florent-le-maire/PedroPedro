using Godot;

namespace PedroBulletV2.Scripts.OptionsMenu;

public partial class PauseMenu : Control
{
	private PackedScene _mainMenu;

	[Export]private Node2D _game;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string scenePath = "res://Scenes/OptionsMenu/MainMenu.tscn";

		_mainMenu = ResourceLoader.Load<PackedScene>(scenePath);		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_IsEsc();
	}

	private void Resume()
	{
		GetTree().Paused = false;
		Visible = false;
	}	
	private void Pause()
	{
		GetTree().Paused = true;
		Visible = true;
	}

	private void _IsEsc()
	{
		if (Input.IsActionJustPressed("esc") && !GetTree().Paused)
		{
			Pause();
		}		
		else if (Input.IsActionJustPressed("esc") && !GetTree().Paused)
		{
			Resume();
		}
	}

	void _on_return_to_main_menu_pressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToPacked(_mainMenu);
	}
	void _on_restart_pressed()
	{
		Resume();
		foreach (Node obj in GetTree().GetNodesInGroup("InstancedObjects"))
		{
			obj.QueueFree();
		}
		GetTree().ReloadCurrentScene();
	}	
	void _on_resume_pressed()
	{
		Resume();
	}
}
