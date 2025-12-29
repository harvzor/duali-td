public partial class RedBullet : CharacterBody2D
{
	public const float Speed = 1000.0f;
	
	/// <summary>
	/// Who this bullet is being fired at.
	/// </summary>
	public Node2D Target;

	public int Damage;

	public override void _PhysicsProcess(double delta)
	{
		if (!IsInstanceValid(Target))
		{
			this.QueueFree();
			return;
		}

		Velocity = this.GlobalPosition.DirectionTo(Target.GlobalPosition) * Speed;
		this.LookAt(Target.GlobalPosition);

		this.MoveAndSlide();
	}

	private void OnArea2DBodyEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;

		critter.Health -= this.Damage;
		this.QueueFree();
	}
}
