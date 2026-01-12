public partial class MenuControls : Control
{
	public void OnPauseButtonGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left })
		{
			// this.GetTree().Paused = !this.GetTree().Paused;
			this.GetTree().ReloadCurrentScene();
		}
	}
}
