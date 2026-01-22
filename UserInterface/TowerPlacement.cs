public partial class TowerPlacement : Panel
{
	private UserInterface _userInterface;

	private bool IsBuildable(Vector2 position)
	{
		TileMapLayer buildable = this.GetNode<TileMapLayer>("/root/World/Map/Buildable")!;

		Vector2 mouseLocal = buildable.ToLocal(position);
		Vector2I cell = buildable.LocalToMap(mouseLocal);

		return buildable.GetCellSourceId(cell) != -1;
	}
	
	public override void _Ready()
	{
		this._userInterface = this.GetParent<UserInterface>()!;
	}

	private void OnGuiInput(InputEvent inputEvent)
	{
		if (inputEvent.IsLeftClickOrTouchDown(out PointerEvent pointerEventDown))
		{
			this.OnHover(pointerEventDown);
		}
		
		if (inputEvent.IsDrag(out PointerEvent pointerEventDrag))
		{
			this.OnHover(pointerEventDrag);
		}

		if (inputEvent.IsLeftClickOrTouchUp(out PointerEvent pointerEventUp))
		{
			Vector2 globalPosition = this.GetGlobalTransform() * pointerEventUp.Position;

			if (!this.IsBuildable(globalPosition))
				return;
			
			this._userInterface.TrySpawnTower(globalPosition);
			this._userInterface.HideTower();
		}
	}
	
	private void OnHover(PointerEvent pointerEvent)
	{
		Vector2 globalPosition = this.GetGlobalTransform() * pointerEvent.Position;
			
		if (!this.IsBuildable(globalPosition))
		{
			this._userInterface.HideTower();
			return;
		}

		this._userInterface.ShowTower(globalPosition);
	}

	private void OnMouseExited()
	{
		this._userInterface.HideTower();
	}
}
