[Tool]
public partial class Square : Node2D
{
    [Export] public float Length = 32;
    [Export] public Color Color = Colors.White;
    
    public override void _Ready()
    {
        QueueRedraw();
    }
    
    public override void _Draw()
    {
        this.DrawPolygon(
        [
                // Top left
                new Vector2(-Length, Length),
                // Top right
                new Vector2(Length, Length),
                // Bottom right
                new Vector2(Length, -Length),
                // Bottom left
                new Vector2(-Length, -Length),
            ],
        [Color]
        );
    }
}
