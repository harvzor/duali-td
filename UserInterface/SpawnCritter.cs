using System.Linq;

public partial class SpawnCritter : Button
{
	private UserInterface _userInterface;

	/// <summary>
	/// This is a scene of a critter which will be spawned.
	/// </summary>
	[Export] public PackedScene CritterScene;
	
	private DateTime _clickStartTime;
	private ProgressBar _progressBar;
	private Timer _timer;

	public override void _Ready()
	{
		this._userInterface = this.GetNode<UserInterface>("../../../..")!;

		CritterBase critterBase = this.CritterScene.Instantiate<CritterBase>();

		Label cost = this.GetNode<Label>("Stats/Cost");
		cost.Text = critterBase.Cost + "G";
		
		Label life = this.GetNode<Label>("Stats/Health");
		life.Text = critterBase.Health + "❤️";
		
		Label speed = this.GetNode<Label>("Stats/Speed");
		speed.Text = critterBase.Speed + "p/s";
		
		this._progressBar = this.GetNode<ProgressBar>("ProgressBar");
		this._timer = this.GetNode<Timer>("ProgressBarTimer");
		this._timer.Timeout += () =>
		{
			this._progressBar.Value -= 15;
		};
		this._timer.Start();

		CritterBase critter = (this.GetChildren().First(x => x.Name.ToString().StartsWith("Critter")) as CritterBase)!;

		critter.Disable();
	}

	private void Spawn()
	{
		if (this._userInterface.TrySpawnCritter(this.CritterScene))
		{
			this._progressBar.Value = 100;
		}
	}

	private void OnGuiInput(InputEvent inputEvent)
	{
		if (this._progressBar.Value > 0)
			return;

		// On left click press.
		if (inputEvent.IsLeftClickOrTouchDown(out PointerEvent _))
		{
			this._clickStartTime = DateTime.Now;
			return;
		}
		
		// On left click release.
		if (inputEvent.IsLeftClickOrTouchUp(out PointerEvent _))
		{
			// Only trigger a click if the click was short (indicating a click rather than a drag).
			if (DateTime.Now.Subtract(this._clickStartTime).TotalMilliseconds < 100)
			{
				this.Spawn();
			}
		}
	}
}
