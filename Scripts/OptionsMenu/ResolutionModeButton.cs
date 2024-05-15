
using System.Collections.Generic;
using System.Linq;
using Godot;


namespace PedroBulletV2.Scripts.OptionsMenu;

public partial class ResolutionModeButton : Control
{
    [Export] private OptionButton _optionButton;

    private Dictionary<string, Vector2I> _resolutions = new Dictionary<string, Vector2I>
    {
        {"1152 x 648", new Vector2I(1152, 648)},
        {"1280 x 720", new Vector2I(1280, 720)},
        {"1920 x 1080", new Vector2I(1920, 1080)},
    };

    public override void _Ready()
    {
        AddResolutionModeItems();
        _optionButton.ItemSelected += OnResolutionSelected;
    }
    
    public void AddResolutionModeItems()
    {
        foreach (var resolution in _resolutions)
        {
            _optionButton.AddItem(resolution.Key.ToString());
        }
    }

    private void OnResolutionSelected(long index)
    {
        int i = (int) index;
        DisplayServer.WindowSetSize(_resolutions.ElementAt(i).Value);
    }
}