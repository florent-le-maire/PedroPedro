using Godot;

namespace PedroBulletV2.Scripts.OptionsMenu;

public partial class SettingsMenu : Control
{
    [Export] private Button _exitButton;

    [Signal]
    public delegate void ExitSettingsMenuEventHandler();

    public override void _Ready()
    {
        _exitButton.ButtonDown += OnExitPressed;
        SetProcess(false);
    }

    private void OnExitPressed()
    {
        EmitSignal(nameof(ExitSettingsMenu));
        SetProcess(false);
    }
}