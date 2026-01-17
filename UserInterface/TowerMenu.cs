using System.Collections.Generic;
using System.Linq;

public partial class TowerMenu : Panel
{
	private UserInterface _userInterface;
	private IEnumerable<TowerMenuOption> _options;

	public override void _Ready()
	{
		this._userInterface = this.GetNode<UserInterface>("..")!;
		this._options = this.GetNode("ScrollContainer/FlowContainer").GetChildren().Select(x => x as TowerMenuOption);
		
		this._userInterface.SelectedTower = GD.Load<PackedScene>("res://Towers/RedBulletTower.tscn");
		
		this.SelectOption(this._options.First());
	}
	
	public void SelectTower(TowerMenuOption option, BulletTower bulletTower)
	{
		this._userInterface.SelectedTower = GD.Load<PackedScene>(bulletTower.GetSceneFilePath());

		this.DeselectOptions();
		this.SelectOption(option);
	}
	
	private void DeselectOptions()
	{
		foreach (TowerMenuOption option in this._options)
		{
			StyleBoxFlat style = new();
			style.BgColor = style.BgColor with { A = 0 };
			
			option.AddThemeStyleboxOverride("panel", style);
		}
	}

	private void SelectOption(TowerMenuOption option)
	{
		StyleBoxFlat style = new()
		{
			BorderColor = Color.FromHtml("#000000"),
		};

		style.SetBorderWidthAll(2);
		style.BgColor = style.BgColor with { A = 0 };
		
		option.AddThemeStyleboxOverride("panel", style);
	}
}
