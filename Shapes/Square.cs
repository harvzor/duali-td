[Tool]
public partial class Square : Node2D
{
    [Export] public float Length = 32;
    [Export] public Color Color = Colors.White;
    
    public override void _Ready()
    {
        this.QueueRedraw();
    }
    
    public override void _Draw()
    {
        this.DrawPolygon(
        [
                // Top left
                new Vector2(-this.Length, this.Length),
                // Top right
                new Vector2(this.Length, this.Length),
                // Bottom right
                new Vector2(this.Length, -this.Length),
                // Bottom left
                new Vector2(-this.Length, -this.Length),
            ],
        [this.Color]
        );
    }
}
