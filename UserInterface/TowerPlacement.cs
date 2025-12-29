public partial class TowerPlacement : Panel
{
	private UserInterface _userInterface;

	private bool IsBuildable()
	{
		TileMapLayer buildable = (this
			.GetTree()
			.GetRoot()
			.GetChild(0)
			.FindChild(nameof(Map))!
			.FindChild("Buildable") as TileMapLayer)!;

		Vector2 mouseLocal = buildable.ToLocal(GetGlobalMousePosition());
		Vector2I cell = buildable.LocalToMap(mouseLocal);

		return buildable.GetCellSourceId(cell) != -1;
	}
	
	public override void _Ready()
	{
		this._userInterface = this.FindParent(nameof(UserInterface))! as UserInterface;
	}

	private void OnGuiInput(InputEvent inputEvent)
	{
		// On hover
		if (inputEvent is InputEventMouseMotion inputEventMouseMotion)
		{
			if (!IsBuildable())
			{
				_userInterface.HideTower();
				return;
			}

			_userInterface.ShowTower(inputEventMouseMotion.Position);
			return;
		}

		// On left click release
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: 0 } inputEventMouseButton)
		{
			if (!IsBuildable())
				return;

			_userInterface.TrySpawnTower(inputEventMouseButton.Position);
		}
	}

	private void OnMouseExited()
	{
		_userInterface.HideTower();
	}
}
