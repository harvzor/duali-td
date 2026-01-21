public partial class Map : Node2D
{
	public void SpawnCritter(CritterBase critter, int player)
	{
		Path2D path2d = (this.FindChild("Path2DP" + player) as Path2D)!;
		
		PathFollow2D pathFollow2D = (path2d.GetChild(0) as PathFollow2D)!;

		// Copy the PathFollow2D because every critter needs its own path with progress.
		Node pathFollow2DCopy = pathFollow2D.Duplicate();
		
		pathFollow2DCopy.AddChild(critter);
		
		path2d.AddChild(pathFollow2DCopy);
	}
}
