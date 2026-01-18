public partial class TowerMenuOption : Button
{
	public BulletTower Tower;
	
	private TowerMenu _towerMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._towerMenu = this.GetNode<TowerMenu>("../../..")!;
		this.Tower = this.GetNode<BulletTower>("Tower")!;
		
		Label cost = this.GetNode<Label>("Stats/Cost");
		cost.Text = this.Tower.Cost + "G";
		
		Label damage = this.GetNode<Label>("Stats/Damage");
		damage.Text = this.Tower.BulletDamage + "💥";
	}

	public void OnOptionGuiInput(InputEvent inputEvent)
	{
		// On left click press.
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left } inputEventMouseButton)
		{
			this._towerMenu.SelectTower(this, this.Tower);
		}
	}
}
