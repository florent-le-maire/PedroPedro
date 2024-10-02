using Godot;
using System;

public partial class Destruction : Area2D
{
    [Export] public double GrowthRate = 200; // Vitesse de croissance en pixels par seconde

    [Export] public double MaxSize = 200; // Taille maximale avant disparition

    [Export] private CollisionShape2D _collisionShape;

    private bool _isGrowing = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // _collisionShape.SetDeferred("disabled", true);
        // _collisionShape.CallDeferred("set_disabled", false);
        var shape = _collisionShape.Shape as CircleShape2D;
        if (shape != null)
        {
            shape.Radius = 10;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        var shape = _collisionShape.Shape as CircleShape2D;
        if (shape != null)
        {
            shape.Radius += (float)(GrowthRate * delta);

            // Si la taille maximale est atteinte, on détruit l'objet
            if (shape.Radius >= MaxSize)
            {
                QueueFree();
            }
        }
    }

    void _on_area_entered(Area2D area2D)
    {
        area2D.QueueFree();
    }
}
