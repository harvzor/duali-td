using Godot;
using System;

public partial class TowerSpawner : Node2D
{
	private PackedScene RedBulletTower = GD.Load<PackedScene>("res://Towers/RedBulletTower.tscn");
	
	public void SpawnTower(Vector2 position)
	{
		var tower = RedBulletTower.Instantiate<RedBulletTower>();
		tower.Position = position;
		
		this.AddChild(tower);
	}
}
