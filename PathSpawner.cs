public partial class PathSpawner : Node2D
{
	public int Cost = 10;

	private PackedScene Stage1 = GD.Load<PackedScene>("res://Mobs/Stage1.tscn");

	public void SpawnCritter(PackedScene critterScene)
	{
		Stage1 stage1 = (Stage1.Instantiate() as Stage1)!;
		stage1.CritterScene = critterScene;
		this.AddChild(stage1);
	}
}
