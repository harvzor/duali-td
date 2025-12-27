public partial class SpawnCritter : Panel
{
	private UserInterface _userInterface;
	
	public override void _Ready()
	{
		this._userInterface = this.FindParent(nameof(UserInterface)) as UserInterface;
	}

	public void OnGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left })
		{
			this._userInterface.SpawnCritter();
		}
	}
}
