using Godot;
using System;

public partial class TowerPlacement : Panel
{
	private UserInterface _userInterface;
	
	public override void _Ready()
	{
		this._userInterface = this.FindParent(nameof(UserInterface))! as UserInterface;
	}

	public void OnGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseMotion inputEventMouseMotion)
		{
			_userInterface.ShowTower(inputEventMouseMotion.Position);
		}

		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left } inputEventMouseButton)
		{
			_userInterface.TrySpawnTower(inputEventMouseButton.Position);
		}
	}

	public void OnMouseExited()
	{
		_userInterface.HideTower();
	}
}
