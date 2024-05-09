using Godot;
using System;

public partial class GameManager : Node
{
    [Export] private HealthBar _healthBar;
    [Export] private Player _player;
    
    public override void _Ready()
    {
        _healthBar.SetMaxHearts(_player.PlayerMaxHealth);
        _healthBar.UpdateHearts(_player.PlayerCurrentHealth);
        _player.HealthChanged += _healthBar.UpdateHearts;
    }
}
