using System.Diagnostics.CodeAnalysis;
using System.Linq;

public partial class TowerSpawner : Node2D
{
	public int Cost = 10;
	
	private PackedScene _redBulletTower = GD.Load<PackedScene>("res://Towers/RedBulletTower.tscn");
	
	/// <summary>
	/// Tower that hasn't been properly placed, just the outline is shown.
	/// </summary>
	private RedBulletTower _ghostTower;

	private Vector2 SnapPosition(Vector2 position)
	{
		const float cellSize = 64;
		const float offset = cellSize / 2f;

		float snappedX = MathF.Floor(position.X / cellSize) * cellSize + offset;
		float snappedY = MathF.Floor(position.Y / cellSize) * cellSize + offset;

		return new Vector2(snappedX, snappedY);
	}
	
	public void ShowTower(Vector2 position)
	{
		if (!IsInstanceValid(this._ghostTower))
		{
			this._ghostTower = this._redBulletTower.Instantiate<RedBulletTower>();
			this._ghostTower.Disable();
			this.AddChild(this._ghostTower);
		}
		
		this._ghostTower.Position = this.SnapPosition(position);
	}
	
	public void HideTower()
	{
		if (IsInstanceValid(this._ghostTower))
		{
			this._ghostTower.QueueFree();
		}
	}

	public bool TowerAlreadyExists(Vector2 position)
	{
		Vector2 snappedPosition = this.SnapPosition(position);

		return this.GetChildren()
			.Select(tower => (RedBulletTower)tower)
			.Any(tower => tower.Enabled && tower.Position == snappedPosition);
	}
	
	public void SpawnTower(Vector2 position)
	{
		RedBulletTower tower = this._redBulletTower.Instantiate<RedBulletTower>();
		tower.Position = this.SnapPosition(position);

		this.AddChild(tower);
	}
}
