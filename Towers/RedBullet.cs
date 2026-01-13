public partial class RedBullet : CharacterBody2D
{
	public int Speed = 0;
	
	/// <summary>
	/// Who this bullet is being fired at.
	/// </summary>
	public Node2D Target;

	public int Damage;

	public override void _PhysicsProcess(double delta)
	{
		if (!IsInstanceValid(this.Target))
		{
			this.QueueFree();
			return;
		}

		this.Velocity = this.GlobalPosition.DirectionTo(this.Target.GlobalPosition) * this.Speed;
		this.LookAt(this.Target.GlobalPosition);

		this.MoveAndSlide();
	}

	private void OnArea2DBodyEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;

		critter.TakeDamage(this.Damage);
		this.QueueFree();
	}
}
