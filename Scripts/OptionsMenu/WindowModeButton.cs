using Godot;
using Godot.Collections;

namespace PedroBulletV2.Scripts.OptionsMenu;

public partial class WindowModeButton : Control
{
    [Export] private OptionButton _optionButton;

    private  string[] _windowModeArray = new string[]
        {"Full-screen", "Window mode", "Borderless Window", "Borderless full-screen"};

    public override void _Ready()
    {
        AddWindowModeItems();
        _optionButton.ItemSelected += OnWindowsModeSelected;
    }

    public void AddWindowModeItems()
    {
        foreach (var windowMode in _windowModeArray)
        {
            _optionButton.AddItem(windowMode);
        }
    }

    void OnWindowsModeSelected(long index)
    {
        switch (index)
        {
            case 0: // Full screen
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
                DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
                break;            
            case 1: // Window mode
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
                DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
                break;            
            case 2: // Borderless Window
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
                DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
                break;
            case 3: // Borderless full-screen
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
                DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
                break;
        }
    }
}