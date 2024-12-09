using Godot;

namespace PedroBulletV2.Scripts;

public partial class GameManager : Node
{
    [Export] private HealthBar _healthBar;
    [Export] private Player.Player _player;
    [Export] private Control _gameOverScreen;
    [Export] private AudioStreamPlayer2D _deathSong;

    public override void _Ready()
    {
        _healthBar.SetMaxHearts(_player.PlayerMaxHealth);
        _healthBar.UpdateHearts(_player.PlayerCurrentHealth);
        _player.HealthChanged += _healthBar.UpdateHearts;
        _player.PlayerDie += ShowGameOver;

    }

    private void ShowGameOver()
    {
        _gameOverScreen.Visible = true;
        _deathSong.Play();
        GetTree().Paused = true;
    }

}
