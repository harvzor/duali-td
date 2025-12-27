using Godot;
using System;

public partial class TowerPlacement : Panel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private TowerSpawner GetTowerSpawner()
	{
		return this
			.GetTree()
			.GetRoot()
			.GetChild(0)
			.GetNode<TowerSpawner>(nameof(TowerSpawner));
	}

	public void OnGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseMotion inputEventMouseMotion)
		{
			this.GetTowerSpawner().ShowTower(inputEventMouseMotion.Position);
		}

		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left } inputEventMouseButton)
		{
			this.GetTowerSpawner().SpawnTower(inputEventMouseButton.Position);
		}
	}

	public void OnMouseExited()
	{
		this.GetTowerSpawner().HideTower();
	}
}
