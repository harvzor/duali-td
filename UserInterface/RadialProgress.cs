using System.Collections.Generic;
using System.Threading.Tasks;

[Tool]
public partial class RadialProgress : Control
{
	[Export] public float MaxValue { get; set; } = 100.0f;
	[Export] public float Radius { get; set; } = 120.0f;
	[Export] public float Progress { get; set; } = 0.0f;
	[Export] public float Thickness { get; set; } = 20.0f;
	[Export] public Color BgColor { get; set; } = new Color(0.5f, 0.5f, 0.5f, 1.0f);
	[Export] public Color BarColor { get; set; } = new Color(0.2f, 0.9f, 0.2f, 1.0f);
	[Export] public bool Ring { get; set; } = false;
	[Export] public int NbPoints { get; set; } = 32;

	public override void _Draw()
	{
		Vector2 offset = new(this.Radius, this.Radius);
		float angle = (this.Progress / this.MaxValue) * Mathf.Tau;

		if (this.Ring)
		{
			this.DrawRingArc(offset, this.Radius, this.Radius - this.Thickness, 0.0f, Mathf.Tau, this.BgColor);
			this.DrawRingArc(offset, this.Radius, this.Radius - this.Thickness, 0.0f, angle, this.BarColor);
		}
		else
		{
			this.DrawCircleArc(offset, this.Radius, 0.0f, Mathf.Tau, this.BgColor);
			this.DrawCircleArc(offset, this.Radius, 0.0f, angle, this.BarColor);
			this.DrawCircleArc(offset, this.Radius - this.Thickness, 0.0f, Mathf.Tau, this.BgColor);
		}
	}

	public override void _Process(double delta)
	{
		this.QueueRedraw();
	}

	public async Task Animate(float duration, bool clockwise = true, float initialValue = 0.0f)
	{
		float from = clockwise ? initialValue : this.MaxValue;
		float to = clockwise ? this.MaxValue : initialValue;

		Tween tween = this.CreateTween();
		tween.TweenProperty(this, nameof(this.Progress), to, duration)
			 .From(from)
			 .SetTrans(Tween.TransitionType.Linear)
			 .SetEase(Tween.EaseType.In);

		await this.ToSignal(tween, Tween.SignalName.Finished);
	}

	private void DrawCircleArc(
		Vector2 center,
		float radius,
		float angleFrom,
		float angleTo,
		Color color
	)
	{
		List<Vector2> points = [center];

		Color[] colors = [color];

		float a = angleFrom - (Mathf.Pi / 2.0f);
		float b = (angleTo - angleFrom) / this.NbPoints;

		for (int i = 0; i <= this.NbPoints; i++)
		{
			float anglePoint = a + i * b;
			points.Add(center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius);
		}

		this.DrawPolygon(points.ToArray(), colors);
	}

	private void DrawRingArc(
		Vector2 center,
		float radius1,
		float radius2,
		float angleFrom,
		float angleTo,
		Color color
	)
	{
		List<Vector2> points = [];
		Color[] colors = [color];

		float a = angleFrom - (Mathf.Pi / 2.0f);
		float b = (angleTo - angleFrom) / this.NbPoints;

		for (int i = 0; i <= this.NbPoints; i++)
		{
			float anglePoint = a + i * b;
			points.Add(center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius1);
		}

		for (int i = this.NbPoints; i >= 0; i--)
		{
			float anglePoint = a + i * b;
			points.Add(center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius2);
		}

		// Couldn't see why error "Error: Invalid polygon data, triangulation failed" was being thrown
		// https://forum.godotengine.org/t/error-invalid-polygon-data-triangulation-failed/19058/2
		if (!Geometry2D.TriangulatePolygon(points.ToArray()).IsEmpty())
		{
			this.DrawPolygon(points.ToArray(), colors);
		}
	}
}
