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

		Vector2 mouseLocal = buildable.ToLocal(this.GetGlobalMousePosition());
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
			if (!this.IsBuildable())
			{
				this._userInterface.HideTower();
				return;
			}

			this._userInterface.ShowTower(inputEventMouseMotion.Position);
			return;
		}

		// On left click release
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: 0 } inputEventMouseButton)
		{
			if (!this.IsBuildable())
				return;

			this._userInterface.TrySpawnTower(inputEventMouseButton.Position);
		}
	}

	private void OnMouseExited()
	{
		this._userInterface.HideTower();
	}
}
