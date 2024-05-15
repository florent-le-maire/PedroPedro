using Godot;

namespace PedroBulletV2.Scripts.OptionsMenu;

public partial class MainMenu : Control
{
    [Export] private Button _statButton;
    [Export] private Button _settingsButton;
    [Export] private Button _exitButton;

    [Export] private PackedScene _startLevel;
    [Export] private SettingsMenu _settingsMenu;
    [Export] private MarginContainer _marginContainer;

    public override void _Ready()
    {
        HandleConnectingSignals();
    }



    private void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToPacked(_startLevel);
    }

    private void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

    private void HandleConnectingSignals()
    {
        _statButton.ButtonDown += OnStartButtonPressed;
        _settingsButton.ButtonDown += OnSettingsButtonPressed;
        _exitButton.ButtonDown += OnExitButtonPressed;
        _settingsMenu.ExitSettingsMenu += OnExitSettingsMenu;
    }

    private void OnSettingsButtonPressed()
    {
        _marginContainer.Visible = false;
        _settingsMenu.Visible = true;
    }
    private void OnExitSettingsMenu()
    {
        _marginContainer.Visible = true;
        _settingsMenu.SetProcess(true);
        _settingsMenu.Visible = false;
    }


}