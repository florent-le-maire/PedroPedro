using Godot;

namespace PedroBulletV2.Scripts.Player;

public partial class Player : CharacterBody2D
{
    [Export] public int PlayerMaxHealth = 4;
    [Export] public AudioStreamPlayer2D DamageSound;
    public int PlayerCurrentHealth { get; set; } = 4;

    [Export] public int Speed { get; set; } = 200;

    [Signal]
    public delegate void HealthChangedEventHandler(int currentHealth);

    public override void _Ready()
    {
        SetMaximunHealth();
    }

    public void GetInput()
    {
        Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
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