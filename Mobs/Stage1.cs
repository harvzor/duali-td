public partial class Stage1 : Path2D
{
    [Export] public PackedScene CritterScene;

    public override void _Ready()
    {
        if (this.CritterScene == null)
            throw new Exception("Critter must be set on path.");
        
        (this.GetChild(0) as PathFollow2D)!
            .AddChild(CritterScene.Instantiate());
    }
}
