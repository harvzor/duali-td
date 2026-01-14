public partial class HealthBar : ProgressBar
{
	public override void _Process(double delta)
	{
		PathFollow2D pathFollow = this.GetParent().GetParent<PathFollow2D>();
		this.RotationDegrees = -pathFollow.RotationDegrees + 180;
	}
}
