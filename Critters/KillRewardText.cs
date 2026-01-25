public partial class KillRewardText : Node2D
{
	private float _floatDistance = 30f;
	private float _duration = 0.5f;
	
	public override void _Ready()
	{
		Tween tween = this.CreateTween();

		tween.TweenProperty(this, "position:y", this.Position.Y - this._floatDistance, this._duration)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		tween.Parallel().TweenProperty(this, "modulate:a", 0f, this._duration);

		tween.TweenCallback(Callable.From(this.QueueFree));
	}
	
	public void SetKillReward(int killReward)
	{
		Label label = this.GetNode<Label>("Label");
		label.Text = label.Text.Replace("{0}", killReward.ToString());
	}
}
