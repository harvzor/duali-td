using System.Collections.Generic;
using System.Linq;

public partial class TowerMenu : Panel
{
	private UserInterface _userInterface;
	private IEnumerable<TowerMenuOption> _options;

	public override void _Ready()
	{
		this._userInterface = this.GetNode<UserInterface>("..")!;
		this._options = this.GetNode("ScrollContainer/FlowContainer")
			.GetChildren()
			.Select(x => x as TowerMenuOption)
			.ToArray();
		
		this.SelectTower(this._options.First(), this._options.First().Tower);
	}
	
	public void SelectTower(TowerMenuOption option, BulletTower bulletTower)
	{
		this._userInterface.SelectedTower = GD.Load<PackedScene>(bulletTower.GetSceneFilePath());

		this.DeselectOptions();
	}
	
	private void DeselectOptions()
	{
		foreach (TowerMenuOption option in this._options)
		{
			option.ButtonPressed = false;
		}
	}
}
