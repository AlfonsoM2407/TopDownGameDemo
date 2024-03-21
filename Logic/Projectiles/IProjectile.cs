


using Godot;

public interface IProjectile
{
    void ContactWithWall(Node2D wall);
    void ContactWithCreature(Area2D area);
}