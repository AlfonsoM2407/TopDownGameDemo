using Godot;


public partial class PlayerWeapon : Node2D, IWeapon
{
    [Export] public PackedScene _projectile;

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());
    }

    public void AttackTap()
    {
        Node2D projectile = _projectile.Instantiate<Node2D>();
        
        projectile.GlobalPosition = GlobalPosition;
        projectile.Rotation = Rotation;

        GetTree().Root.GetNode<Node2D>("Main/World").AddChild(projectile); //to be changed
    }
}