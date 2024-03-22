using Godot;


public partial class DynamiteGoblin : CharacterBody2D, IEnemy, IDamageable
{
    [Export] public float Speed = 30000;   
    public Node2D AggroOn
    {
        get { return _navAgent.WalkingTo; }
        set { _navAgent.WalkingTo = value; }
    }

    [Export] public float MaxHP;
    private float CurHP;

    private PathfinderWithTimer _navAgent;
    private Area2D _aggroTriggerRange;
    private Area2D _aggroMaintainRange;

    public override void _Ready()
    {
        _navAgent = GetNode<PathfinderWithTimer>("NavigationAgent");
        _aggroTriggerRange = GetNode<Area2D>("AggroTriggerRange");
        _aggroMaintainRange = GetNode<Area2D>("AggroMaintainRange");

        _aggroTriggerRange.BodyEntered += GainAggroOn;
        _aggroMaintainRange.BodyExited += ResetAggro;

        CurHP = MaxHP;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = ToLocal(_navAgent.GetNextPathPosition()).Normalized();

        if (direction != Vector2.Zero)
        {
            float effectiveSpeed = Speed * (float)delta;
        
            Velocity = direction * effectiveSpeed;
        }
        
        MoveAndSlide();
    }

    private void GainAggroOn(Node2D target)
    {
        AggroOn = target;
        GD.Print($"{this}: Aggroed on {target}");
    }

    private void ResetAggro(Node2D target)
    {
        AggroOn = null;
        _navAgent.TargetPosition = GlobalPosition;
        GD.Print($"{this}: Aggro Reset");
    }

    public void TakeDamage()
    {

    }

    public void Die()
    {
        QueueFree();
    }
}