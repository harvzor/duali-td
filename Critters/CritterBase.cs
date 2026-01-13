public partial class CritterBase : CharacterBody2D
{
	[Export] public int Cost = 10;
	[Export] public int Speed = 500;
	[Export] public int Health = 5;
	
	/// <summary>
	/// How much damage to do to the player's health.
	/// </summary>
	[Export] public int Damage = 10;

	/// <summary>
	/// Which player this critter was spawned by.
	/// </summary>
	public int? Player = null;
	
	/// <summary>
	/// Speed is multiplied by this value.
	/// </summary>
	public float SpeedMultiplier = 1;
	
	/// <summary>
	/// Reduces incoming damage by this percentage.
	/// </summary>
	public int ShieldPercentage = 0;

	public override void _Process(double delta)
	{
		this.GetParent<PathFollow2D>().Progress = this.GetParent<PathFollow2D>().Progress + this.Speed * this.SpeedMultiplier * (float)delta;
		
		if (this.Health <= 0)
			this.GetParent<PathFollow2D>().QueueFree();
	}
	
	public void TakeDamage(int damage)
	{
		int effectiveDamage = damage - (damage * this.ShieldPercentage / 100);
		this.Health -= effectiveDamage;
	}
}
