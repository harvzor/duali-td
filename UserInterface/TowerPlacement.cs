public partial class TowerPlacement : Panel
{
	private UserInterface _userInterface;
	
	public override void _Ready()
	{
		this._userInterface = this.FindParent(nameof(UserInterface))! as UserInterface;
	}

	public void OnGuiInput(InputEvent inputEvent)
	{
		// On hover
		if (inputEvent is InputEventMouseMotion inputEventMouseMotion)
		{
			_userInterface.ShowTower(inputEventMouseMotion.Position);
			return;
		}

		// On left click release
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: 0 } inputEventMouseButton)
		{
			_userInterface.TrySpawnTower(inputEventMouseButton.Position);
		}
	}

	public void OnMouseExited()
	{
		_userInterface.HideTower();
	}
}
