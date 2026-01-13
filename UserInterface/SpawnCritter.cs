public partial class SpawnCritter : Panel
{
	private UserInterface _userInterface;

	/// <summary>
	/// This is a scene of a critter which will be spawned.
	/// </summary>
	[Export] public PackedScene CritterScene;
	
	private DateTime ClickStartTime;
	
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
	}

	private void OnGuiInput(InputEvent inputEvent)
	{
		// On left click press.
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left })
		{
			this.ClickStartTime = DateTime.Now;
			return;
		}
		
		// On left click release.
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: 0 })
		{
			// Only trigger a click if the click was short (indicating a click rather than a drag).
			if (DateTime.Now.Subtract(this.ClickStartTime).TotalMilliseconds < 100)
			{
				this._userInterface.TrySpawnCritter(this.CritterScene, this._userInterface.Player);
			}
		}
	}
}
