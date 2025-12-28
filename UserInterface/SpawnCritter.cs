public partial class SpawnCritter : Panel
{
	private UserInterface _userInterface;

	/// <summary>
	/// This is a scene of a critter which will be spawned.
	/// </summary>
	[Export] public PackedScene CritterScene;
	
	public override void _Ready()
	{
		this._userInterface = this.FindParent(nameof(UserInterface)) as UserInterface;

		CritterBase critterBase = CritterScene.Instantiate<CritterBase>();

		Label cost = this.GetNode<Label>("Stats/Cost");
		cost.Text = Map.Cost + "G";
		
		Label life = this.GetNode<Label>("Stats/Health");
		life.Text = critterBase.Health + "❤️";
		
		Label speed = this.GetNode<Label>("Stats/Speed");
		speed.Text = critterBase.Speed + "p/s";
	}

	public void OnGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left })
		{
			this._userInterface.TrySpawnCritter(CritterScene);
		}
	}
}
