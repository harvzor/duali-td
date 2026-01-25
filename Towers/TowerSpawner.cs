using System.Collections.Generic;
using System.Linq;

public partial class TowerSpawner : Node2D
{
	/// <summary>
	/// Tower that hasn't been properly placed, just the outline is shown.
	/// </summary>
	private List<BulletTower> _ghostBulletTowers = [];

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
		BulletTower existingBulletTower = this._ghostBulletTowers
			.FirstOrDefault(ghostBulletTower => ghostBulletTower.Player == bulletTower.Player);

		if (existingBulletTower == null)
		{
			bulletTower.Disable();
			this._ghostBulletTowers.Add(bulletTower);
			this.AddChild(bulletTower);
			existingBulletTower = bulletTower;
		}
		
		existingBulletTower.Position = this.SnapPosition(position);
	}
	
	public void HideTower(int forPlayer)
	{
		BulletTower existingBulletTower = this._ghostBulletTowers
			.FirstOrDefault(ghostBulletTower => ghostBulletTower.Player == forPlayer);

		if (existingBulletTower != null)
		{
			existingBulletTower.QueueFree();
			this._ghostBulletTowers.Remove(existingBulletTower);
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
