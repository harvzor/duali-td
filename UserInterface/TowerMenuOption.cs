public partial class TowerMenuOption : Button
{
	public BulletTower Tower;
	
	private TowerMenu _towerMenu;

	public override void _Ready()
	{
		this._towerMenu = this.GetNode<TowerMenu>("../../..")!;
		this.Tower = this.GetNode<BulletTower>("Tower")!;
		
		Label cost = this.GetNode<Label>("Stats/Cost");
		cost.Text = this.Tower.Cost + "G";
		
		Label damage = this.GetNode<Label>("Stats/Damage");
		damage.Text = this.Tower.BulletDamage + "💥";
	}

	public void OnOptionGuiInput(InputEvent _)
	{
		this._towerMenu.SelectTower(this.Tower);
	}
}
