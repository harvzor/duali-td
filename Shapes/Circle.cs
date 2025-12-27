[Tool]
public partial class Circle : Node2D
{
    [Export] public float Radius = 32;
    [Export] public Color Color = Colors.White;
    
    public override void _Ready()
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        this.DrawCircle(new Vector2(0, 0), Radius, new Color(Color));
    }
}
