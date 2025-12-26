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

	public void OnGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left } inputEventMouseButton)
		{
			this
				.GetTree()
				.GetRoot()
				.GetChild(0)
				.GetNode<TowerSpawner>(nameof(TowerSpawner))
				.SpawnTower(inputEventMouseButton.Position);
		}
	}
}
