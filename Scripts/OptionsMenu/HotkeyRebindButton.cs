using Godot;

namespace PedroBulletV2.Scripts.OptionsMenu;

public partial class HotkeyRebindButton : Control
{
    [Export] private Label _label;
    [Export] private Button _button;
    [Export] public string ActionName = "up";

    public override void _Ready()
    {
        SetProcessUnhandledKeyInput(false);
        SetActionName();
        SetTextForKey();
    }

    public void SetActionName()
    {
        _label.Text = ActionName;
    }

    private void SetTextForKey()
    {
        var actionEvents = InputMap.ActionGetEvents(ActionName);
        InputEventKey actionEvent = (InputEventKey)actionEvents[0];
        string keyName = OS.GetKeycodeString(actionEvent.PhysicalKeycode);
        _button.Text = keyName;
    }

    void _on_button_toggled(bool buttonPressed)
    {
        if (buttonPressed)
        {
            _button.Text = "Press any key...";
            SetProcessUnhandledKeyInput(true);
            foreach (HotkeyRebindButton rebindButton in GetTree().GetNodesInGroup("hotkeyButton"))
            {
                if (rebindButton.ActionName != ActionName)
                {
                    rebindButton._button.ToggleMode = false;
                    rebindButton.SetProcessUnhandledKeyInput(false);
                }
            }
        }
        else
        {
            foreach (HotkeyRebindButton rebindButton in GetTree().GetNodesInGroup("hotkeyButton"))
            {
                if (rebindButton.ActionName != ActionName)
                {
                    rebindButton._button.ToggleMode = true;
                    rebindButton.SetProcessUnhandledKeyInput(false);
                }
            }
            SetTextForKey();
        }
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        _RebindActionKey(@event);
        _button.ButtonPressed = false;
    }

    private void _RebindActionKey(InputEvent inputEvent)
    {
        InputMap.ActionEraseEvents(ActionName);
        InputMap.ActionAddEvent(ActionName, inputEvent);
        SetProcessUnhandledKeyInput(false);
        SetTextForKey();
        SetActionName();
    }
}