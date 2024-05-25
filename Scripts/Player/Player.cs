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


    [Signal]
    public delegate void HealthChangedEventHandler(int currentHealth);

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
        MoveAndSlide();
    }

    void _on_hurt_box_area_entered(Area2D area)
    {
        string areaName = area.Name;
        // if (areaName.ToLower() == "bullet")
        // {
        PlayerCurrentHealth--;
        DamageSound.Play();
        if (PlayerCurrentHealth < 0)
        {
            SetMaximunHealth();
        }
        EmitSignal(nameof(HealthChanged),PlayerCurrentHealth);
        // }
    }

    private void SetMaximunHealth()
    {
        PlayerCurrentHealth = PlayerMaxHealth;
    }
}
