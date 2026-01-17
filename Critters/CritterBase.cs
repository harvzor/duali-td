public partial class CritterBase : Node2D
{
	[Export] public int Cost = 10;
	[Export] public int Speed = 100;
	[Export] public int Health = 100;
	
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
	/// Reduces incoming damage by this amount.
	/// </summary>
	public int Shield = 0;

	private int _currentHealth;

	private HealthBar _healthBar;

	public override void _Ready()
	{
		this._healthBar = this.GetNode<HealthBar>("HealthBar");

		this._currentHealth = this.Health;
	}

	public override void _Process(double delta)
	{
		this.GetParent<PathFollow2D>().Progress = this.GetParent<PathFollow2D>().Progress + this.Speed * this.SpeedMultiplier * (float)delta;
		
		if (this._currentHealth <= 0)
			this.GetParent<PathFollow2D>().QueueFree();
	}
	
	public void TakeDamage(int damage)
	{
		this._currentHealth -= damage - this.Shield;

		this._healthBar.Value = (float)this._currentHealth / this.Health * 100;
	}

	public virtual void Disable()
	{
		this._healthBar.Hide();
	}
}
