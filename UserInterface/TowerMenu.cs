public partial class TowerMenu : Panel
{
	private UserInterface _userInterface;

	public override void _Ready()
	{
		this._userInterface = this.GetNode<UserInterface>("..")!;
		
		this._userInterface.SelectedTower = GD.Load<PackedScene>("res://Towers/RedBulletTower.tscn");
	}
	
	public void SelectTower(BulletTower bulletTower)
	{
		this._userInterface.SelectedTower = GD.Load<PackedScene>(bulletTower.GetSceneFilePath());
	}
}
