using Godot;

namespace PedroBulletV2.Scenes.Pattern.Attacks;

public partial class Rotator : Node2D
{
    [Export] public float RotationSpeed = 1;
    [Export] public float Duration = 3;

    public void SetValues(float rotationSpeed, float duration)
    {
        RotationSpeed = rotationSpeed;
        Duration = duration;
    }
    public override void _Ready()
    {
        var durationTimer = new Timer();
        durationTimer.WaitTime = Duration;
        durationTimer.Timeout += DurationTimerTimeout;
        AddChild(durationTimer);
        durationTimer.Start();
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var incrementRotationInDegrees = (float)(RotationSpeed * delta);
        RotationDegrees = RotationDegrees % 360 + incrementRotationInDegrees;
    }
    
    void DurationTimerTimeout()
    {
        QueueFree();
    }
}