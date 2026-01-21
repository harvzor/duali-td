public partial class MenuControls : Control
{
	public void OnPauseButtonGuiInput(InputEvent inputEvent)
	{
		if (inputEvent.IsLeftClickOrTouchDown(out PointerEvent _))
		{
			// this.GetTree().Paused = !this.GetTree().Paused;
			this.GetTree().ReloadCurrentScene();
		}
	}
}
