using Godot;
using System;

public partial class AoeV2 : Area2D
{
 [Export] private float _delay = 2.0f;
    [Export] private float _delayBeforeDeleting = 2.2f;
    [Export] private float _radius = 100f;
    [Export] private Color _fillColor = new Color(1, 0, 0, 0.5f);
    [Export] private Color _outlineColor = new Color(1, 1, 1, 1);
    [Export] private float _outlineThickness = 2f;

    [Export] private CollisionShape2D _collisionShape;


    private Timer _damageTimer;
    private Timer _deleteTimer;
    private float _elapsedTime = 0;
    private bool _isActive = false;

    public override void _Ready()
    {
        _SetTimers();

        if (_collisionShape != null)
        {
            _collisionShape.Position = Position;
            ((CircleShape2D)_collisionShape.Shape).Radius = _radius;
            _collisionShape.SetDeferred("disabled", true);
        }

        _damageTimer.Start();
        _deleteTimer.Start();
        _isActive = true;
    }

    private void _SetTimers()
    {
        _damageTimer = new Timer();
        _damageTimer.WaitTime = _delay;
        _damageTimer.OneShot = true;
        _damageTimer.Timeout += OnDamageTimeout;
        AddChild(_damageTimer);

        _deleteTimer = new Timer();
        _deleteTimer.WaitTime = _delayBeforeDeleting;
        _deleteTimer.OneShot = true;
        _deleteTimer.Timeout += OnDeleteTimeout;
        AddChild(_deleteTimer);
    }

    public override void _Process(double delta)
    {
        if (_isActive)
        {
            _elapsedTime += (float)delta;
            QueueRedraw();
        }
    }

    public override void _Draw()
    {
        float progress = Mathf.Clamp(_elapsedTime / _delay, 0, 1);
        Color currentColor = _fillColor;
        currentColor.A = 0.5f * progress;

        DrawCircle(Position, _radius * progress, currentColor);

        DrawArc(Position, _radius, 0, Mathf.Tau, 64, _outlineColor, _outlineThickness);
    }

    private void OnDamageTimeout()
    {
        if (_collisionShape != null)
        {
            _collisionShape.SetDeferred("disabled", false);
        }
    }

    private void OnDeleteTimeout()
    {
        _isActive = false;
        QueueFree();
    }
}
