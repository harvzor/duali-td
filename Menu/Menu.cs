public partial class Menu : Control
{
	public void OnSimpleMapPressed()
	{
		this.LoadMap("res://Maps/MapSimple.tscn");
	}
	
	public void OnWindyMapPressed()
	{
		this.LoadMap("res://Maps/MapWindy.tscn");
	}

	private void LoadMap(string mapPath)
	{
		PackedScene worldScene = GD.Load<PackedScene>("res://World.tscn");
		
		Node world = worldScene.Instantiate();
		
		PackedScene windyMapScene = GD.Load<PackedScene>(mapPath);
		Node windyMap = windyMapScene.Instantiate();
		world.AddChild(windyMap);
		world.MoveChild(windyMap, 0);
		
		this.GetTree().Root.AddChild(world);
	}
}
