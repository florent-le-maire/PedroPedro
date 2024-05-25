using System;
using Godot;

namespace PedroBulletV2.Scripts.OptionsMenu;

public partial class AudioSliderSettings : Control
{
    [Export] private Label _audioName;
    [Export] private Label _audioNumber;
    [Export] private HSlider _slider;

    [Export(PropertyHint.Enum, "Master,Music,Sfx")]
    private string _busName;

    private int _busIndex = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _slider.ValueChanged += _OnValueChanged;
        _GetBusNameByIndex();
        _SetNameLabelText();
        _SetSliderValue();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _SetNameLabelText()
    {
        _audioName.Text = $"{_busName} Volume";
    }    
    private void _SetNumberLabelText()
    {
        _audioNumber.Text = $"{(_slider.Value * 100)}%";
    }

    private void _SetSliderValue()
    {
        _slider.Value = Mathf.LinearToDb(AudioServer.GetBusVolumeDb(_busIndex));
        _SetNumberLabelText();
    }

    private void _GetBusNameByIndex()
    {
        _busIndex = AudioServer.GetBusIndex(_busName);
    }
    private void _OnValueChanged(double value)
    {
        AudioServer.SetBusVolumeDb(_busIndex, (float)Mathf.LinearToDb(value));
        _SetNumberLabelText();
    }
}