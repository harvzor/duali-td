public abstract partial class Bullet : CharacterBody2D
{
	public int Speed = 0;
	
	/// <summary>
	/// Who this bullet is being fired at.
	/// </summary>
	public Node2D Target;
	
	/// <summary>
	/// Who fired this bullet.
	/// </summary>
	public Player Player = null;

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

	public virtual void OnArea2DBodyEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;

		critter.TakeDamage(this.Damage, out int killReward);
		
		if (killReward > 0)
		{
			this.Player.IncreaseBank(killReward);
		}

		this.QueueFree();
	}
}
