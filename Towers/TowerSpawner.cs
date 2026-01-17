using System.Linq;

public partial class TowerSpawner : Node2D
{
	/// <summary>
	/// Tower that hasn't been properly placed, just the outline is shown.
	/// </summary>
	private BulletTower _ghostBulletTower;

	private Vector2 SnapPosition(Vector2 position)
	{
		const float cellSize = 64;
		const float offset = cellSize / 2f;

		float snappedX = MathF.Floor(position.X / cellSize) * cellSize + offset;
		float snappedY = MathF.Floor(position.Y / cellSize) * cellSize + offset;

		return new Vector2(snappedX, snappedY);
	}
	
	public void ShowTower(Vector2 position, BulletTower bulletTower)
	{
		if (!IsInstanceValid(this._ghostBulletTower))
		{
			this._ghostBulletTower = bulletTower;
			this._ghostBulletTower.Disable();
			this.AddChild(this._ghostBulletTower);
		}
		
		this._ghostBulletTower.Position = this.SnapPosition(position);
	}
	
	public void HideTower()
	{
		if (IsInstanceValid(this._ghostBulletTower))
		{
			this._ghostBulletTower.QueueFree();
		}
	}

	public bool TowerAlreadyExists(Vector2 position)
	{
		Vector2 snappedPosition = this.SnapPosition(position);

		return this.GetChildren()
			.Select(tower => (BulletTower)tower)
			.Any(tower => tower.Enabled && tower.Position == snappedPosition);
	}
	
	public void SpawnTower(Vector2 position, int player, BulletTower bulletTower)
	{
		bulletTower.Player = player;
		bulletTower.Position = this.SnapPosition(position);

		this.AddChild(bulletTower);
	}
}
