public partial class BulletBlue : Bullet
{
	public override void OnArea2DBodyEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;
		
		critter.SpeedMultiplier *= 0.8f;

		base.OnArea2DBodyEntered(body);
	}
}
