public partial class Map : Node2D
{
	public const int Cost = 10;

	public void SpawnCritter(PackedScene critterScene, int player)
	{
		if (critterScene == null)
			throw new Exception("Critter must be set on path.");

		Path2D path2d = (this.FindChild("Path2DP" + player) as Path2D)!;
		
		PathFollow2D pathFollow2D = (path2d.GetChild(0) as PathFollow2D)!;

		// Copy the PathFollow2D because every critter needs its own path with progress.
		Node pathFollow2DCopy = pathFollow2D.Duplicate();
		
		pathFollow2DCopy.AddChild(critterScene.Instantiate());
		
		path2d.AddChild(pathFollow2DCopy);
	}
}
