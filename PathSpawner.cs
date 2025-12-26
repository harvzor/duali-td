using Godot;
using System;

public partial class PathSpawner : Node2D
{
	private PackedScene Stage1 = GD.Load<PackedScene>("res://Mobs/Stage1.tscn");

	public void OnTimerTimeout()
	{
		SpawnCritter();
	}

	public void SpawnCritter()
	{
		Node instance = Stage1.Instantiate();
		AddChild(instance);
	}
}
