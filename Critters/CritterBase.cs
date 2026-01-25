public partial class CritterBase : Node2D
{
	[Export] public int Cost = 10;
	[Export] public int Speed = 100;
	[Export] public int Health = 100;
	[Export] public int KillReward = 1;
	
	/// <summary>
	/// How much damage to do to the player's health.
	/// </summary>
	[Export] public int Damage = 10;

	/// <summary>
	/// Which player this critter was spawned by.
	/// </summary>
	public Player Player = null;
	
	/// <summary>
	/// Speed is multiplied by this value.
	/// </summary>
	public float SpeedMultiplier = 1;
	
	/// <summary>
	/// Reduces incoming damage by this amount.
	/// </summary>
	public int Shield = 0;

	private int _currentSpeed;
	private int _currentHealth;

	private HealthBar _healthBar;

	public override void _Ready()
	{
		this._healthBar = this.GetNode<HealthBar>("HealthBar");

		this._currentSpeed = this.Speed;
		this._currentHealth = this.Health;
	}

	public override void _Process(double delta)
	{
		this.GetParent<PathFollow2D>().Progress = this.GetParent<PathFollow2D>().Progress + this._currentSpeed * this.SpeedMultiplier * (float)delta;
	}
	
	public void TakeDamage(int damage, out int killReward)
	{
		this._currentHealth -= damage - this.Shield;

		this._healthBar.Value = (float)this._currentHealth / this.Health * 100;

		if (this._currentHealth <= 0)
		{
			this.SpawnKillRewardText(this.KillReward);

			killReward = this.KillReward;
			this.GetParent<PathFollow2D>().QueueFree();

			return;
		}

		killReward = 0;
	}

	public void SetSpeed(int speed)
	{
		this._currentSpeed = speed;
	}
	
	public void ResetSpeed()
	{
		this._currentSpeed = this.Speed;
	}

	public virtual void Disable()
	{
		this._healthBar.Hide();
	}

	private void SpawnKillRewardText(int killReward)
	{
		KillRewardText killRewardText = GD.Load<PackedScene>("res://Critters/KillRewardText.tscn").Instantiate<KillRewardText>();
		killRewardText.Position = this.GlobalPosition;

		killRewardText.SetKillReward(killReward);

		this.FindWorld().AddChild(killRewardText);
	}
}
