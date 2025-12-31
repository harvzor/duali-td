[Tool]
public partial class Pentagon : Node2D
{
    [Export] public float Length = 32;
    [Export] public Color Color = Colors.White;
    
    public override void _Ready()
    {
        this.QueueRedraw();
    }
    
    public override void _Draw()
    {
        const int sides = 5;
        Vector2[] points = new Vector2[sides];

        for (int i = 0; i < sides; i++)
        {
            float angle = Mathf.Tau * i / sides - Mathf.Pi / 2;
            points[i] = new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * this.Length;
        }
        
        this.DrawPolygon(
            points,
            [this.Color]
        );
    }
}