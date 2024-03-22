using Godot;


public partial class PathfinderWithTimer : NavigationAgent2D
{
    public Node2D WalkingTo;

    public override void _Ready()
    {
        GetNode<Timer>("Timer").Timeout += RefreshPath;
    }

    private void RefreshPath()
    {
        if (WalkingTo != null)
        {
            TargetPosition = WalkingTo.GlobalPosition;
        }
    }
}