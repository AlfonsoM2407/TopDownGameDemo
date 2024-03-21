using Godot;


public partial class ArrowProjectile : Node2D, IProjectile
{
    public float Speed = 1500;

    private Area2D _hitbox; 

    public override void _Ready()
    {
        GetNode<Timer>("Lifetime").Timeout += QueueFree;

        _hitbox = GetNode<Area2D>("Hitbox");
        _hitbox.BodyEntered += ContactWithWall;
    }

    public override void _Process(double delta)
    {
        float effectiveSpeed = Speed * (float)delta;
        Vector2 direction = Vector2.Right.Rotated(Rotation); //i hate this
        
        Translate(effectiveSpeed * direction); 
    }

    public void ContactWithWall(Node2D wall)
    {
        QueueFree();
    }
    public void ContactWithCreature(Area2D area)
    {

    }
}