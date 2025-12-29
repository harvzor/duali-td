public partial class CritterBase : CharacterBody2D
{
	[Export] public int Speed = 500;
	[Export] public int Health = 5;
	
	/// <summary>
	/// How much damage to do to the player's health.
	/// </summary>
	[Export] public int Damage = 10;

	public override void _Process(double delta)
	{
		this.GetParent<PathFollow2D>().Progress = this.GetParent<PathFollow2D>().Progress + this.Speed * (float)delta;
		
		if (this.Health <= 0)
			this.GetParent<PathFollow2D>().QueueFree();
	}
}
