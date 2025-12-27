using Godot;
using System;

public partial class TowerSpawner : Node2D
{
	private PackedScene RedBulletTower = GD.Load<PackedScene>("res://Towers/RedBulletTower.tscn");
	
	/// <summary>
	/// Tower that hasn't been properly placed, just the outline is shown.
	/// </summary>
	private RedBulletTower GhostTower;

	private Vector2 SnapPosition(Vector2 position)
	{
		return position.Snapped(new Vector2(128, 128)) + new Vector2(64, 64);;
	}
	
	public void ShowTower(Vector2 position)
	{
		if (!IsInstanceValid(this.GhostTower))
		{
			this.GhostTower = RedBulletTower.Instantiate<RedBulletTower>();
			this.GhostTower.Disable();
			this.AddChild(this.GhostTower);
		}
		
		this.GhostTower.Position = this.SnapPosition(position);
	}
	
	public void HideTower()
	{
		if (IsInstanceValid(this.GhostTower))
		{
			this.GhostTower.QueueFree();
		}
	}
	
	public void SpawnTower(Vector2 position)
	{
		var tower = RedBulletTower.Instantiate<RedBulletTower>();
		tower.Position = this.SnapPosition(position);

		this.AddChild(tower);
	}
}
