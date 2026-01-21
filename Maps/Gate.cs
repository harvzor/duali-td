public partial class Gate : Area2D
{
	[Export] public int Player;

	private UserInterface _userInterface;
	
	public override void _Ready()
	{
		this._userInterface = this.FindUserInterface(this.Player)!;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;
		
		critter.QueueFree();
		this._userInterface.TakeDamage(critter.Damage);
	}
}
