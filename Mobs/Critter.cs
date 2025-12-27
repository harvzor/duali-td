public partial class Critter : CharacterBody2D
{
	private const int Speed = 500;
	public int Health = 5;
	
	/// <summary>
	/// How much damage to do to the player's health.
	/// </summary>
	public int Damage = 10;

	public override void _Process(double delta)
	{
		this.GetParent<PathFollow2D>().Progress = this.GetParent<PathFollow2D>().Progress + Speed * (float)delta;
		
		if (this.Health <= 0)
			this.GetParent<PathFollow2D>().GetParent<Path2D>().QueueFree();
	}
}
