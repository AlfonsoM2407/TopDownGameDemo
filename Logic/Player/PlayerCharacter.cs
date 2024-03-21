using Godot;


public partial class PlayerCharacter : CharacterBody2D
{
    [Export] public float Speed = 20000;
    [Export] public Node2D ActiveWeapon {get {return (Node2D)_activeWeapon;} set {_activeWeapon = (IWeapon)value;} }
    private IWeapon _activeWeapon;

    private AnimationPlayer _animationPlayer;

    public Camera3D Camera;

    private Vector3 _targetVelocity = Vector3.Zero;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        //Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        HorizontalPhysicsProcess(delta);

        MoveAndSlide();
    }

    public override void _Process(double delta)
    {
        
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("Attack"))
        {
            _activeWeapon.AttackTap();
        }
    

        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().Quit();
        }
        if (@event is InputEventKey eventKey && eventKey.Keycode == Key.F11)
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
    }

    

    private void HorizontalPhysicsProcess(double delta)
    {
        Vector2 targetVelocity = Vector2.Zero;

        float effectiveSpeed = Speed * (float)delta;

        Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
        Vector2 directionNormal = direction.Normalized();

        if (directionNormal != Vector2.Zero)
        {
            targetVelocity += directionNormal * effectiveSpeed;
        }

        ResetHorizontalVelocity();
        Velocity += targetVelocity;
    
    }

    private void ResetHorizontalVelocity()
    {
        Vector2 targetVelocity = Vector2.Zero;
        Velocity = targetVelocity;
    }
}