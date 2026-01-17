public partial class TowerMenuOption : Panel
{
	private TowerMenu _towerMenu;
	private BulletTower _tower;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._towerMenu = this.GetNode<TowerMenu>("../../..")!;
		this._tower = this.GetNode<BulletTower>("Tower")!;
	}

	public void OnOptionGuiInput(InputEvent inputEvent)
	{
		// On left click press.
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left } inputEventMouseButton)
		{
			this._towerMenu.SelectTower(this._tower);
		}
	}
}
