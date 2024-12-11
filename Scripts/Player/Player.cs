using Godot;

namespace PedroBulletV2.Scripts.Player;

public partial class Player : CharacterBody2D
{
    [Export] public int PlayerMaxHealth = 4;
    [Export] public AudioStreamPlayer2D DamageSound;
    public int PlayerCurrentHealth { get; set; } = 4;

    [Export] private int _normalSpeed = 200;
    public int Speed { get; set; }

    [Export] private Dash _dash;
    [Export] private int _dashSpeed = 1000;
    [Export] private float _dashLength = 0.1f;

    [Export] private PackedScene _destruction;


    [Signal]
    public delegate void HealthChangedEventHandler(int currentHealth);

    [Signal]
    public delegate void PlayerDieEventHandler();

    public override void _Ready()
    {
        Speed = _normalSpeed;
        SetMaximunHealth();
    }

    public void GetInput()
    {
        Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
        if (Input.IsActionJustPressed("dash") && _dash.IsInCooldown())
        {
            _dash.StartDash(_dashLength);
        }

        if (_dash.IsDashing)
        {
            Speed = _dashSpeed;
        }
        else
        {
            Speed = _normalSpeed;
        }

        Velocity = inputDirection * Speed;
    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndCollide(Velocity * (float)delta);
    }

    void _on_hurt_box_area_entered(Area2D area)
    {
        string areaName = area.Name;
        // if (areaName.ToLower() == "bullet")
        // {
        var destruction = _destruction.Instantiate();
        CallDeferred("add_child", destruction);
        destruction.AddToGroup("InstancedObjects");
        PlayerCurrentHealth--;
        DamageSound.Play();
        IsThePlayerDead();
        EmitSignal(nameof(HealthChanged), PlayerCurrentHealth);
        // }
    }

    private void SetMaximunHealth()
    {
        PlayerCurrentHealth = PlayerMaxHealth;
    }

    private void IsThePlayerDead()
    {
        if (PlayerCurrentHealth <= 0)
        {
            EmitSignal(nameof(PlayerDie));
        }
    }
}
