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
		Vector2 offset = new Vector2(Radius, Radius);
		float angle = (Progress / MaxValue) * Mathf.Tau;

		if (Ring)
		{
			DrawRingArc(offset, Radius, Radius - Thickness, 0.0f, Mathf.Tau, BgColor);
			DrawRingArc(offset, Radius, Radius - Thickness, 0.0f, angle, BarColor);
		}
		else
		{
			DrawCircleArc(offset, Radius, 0.0f, Mathf.Tau, BgColor);
			DrawCircleArc(offset, Radius, 0.0f, angle, BarColor);
			DrawCircleArc(offset, Radius - Thickness, 0.0f, Mathf.Tau, BgColor);
		}
	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public async Task Animate(float duration, bool clockwise = true, float initialValue = 0.0f)
	{
		float from = clockwise ? initialValue : MaxValue;
		float to = clockwise ? MaxValue : initialValue;

		Tween tween = CreateTween();
		tween.TweenProperty(this, nameof(Progress), to, duration)
			 .From(from)
			 .SetTrans(Tween.TransitionType.Linear)
			 .SetEase(Tween.EaseType.In);

		await ToSignal(tween, Tween.SignalName.Finished);
	}

	private void DrawCircleArc(
		Vector2 center,
		float radius,
		float angleFrom,
		float angleTo,
		Color color
	)
	{
		var points = new List<Vector2> { center };

		var colors = new[] { color };

		float a = angleFrom - (Mathf.Pi / 2.0f);
		float b = (angleTo - angleFrom) / NbPoints;

		for (int i = 0; i <= NbPoints; i++)
		{
			float anglePoint = a + i * b;
			points.Add(center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius);
		}

		DrawPolygon(points.ToArray(), colors);
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
		var points = new List<Vector2>();
		var colors = new[] { color };

		float a = angleFrom - (Mathf.Pi / 2.0f);
		float b = (angleTo - angleFrom) / NbPoints;

		for (int i = 0; i <= NbPoints; i++)
		{
			float anglePoint = a + i * b;
			points.Add(center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius1);
		}

		for (int i = NbPoints; i >= 0; i--)
		{
			float anglePoint = a + i * b;
			points.Add(center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius2);
		}

		DrawPolygon(points.ToArray(), colors);
	}
}
