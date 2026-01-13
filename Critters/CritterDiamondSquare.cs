public partial class CritterDiamondSquare : CritterBase
{
	private int _shieldModifier = 25;

	private void OnAuraEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;
		
		if (critter.Player != this.Player)
			return;

		critter.ShieldPercentage += this._shieldModifier;
	}
	
	private void OnAuraExited(Node2D body)
	{
		if (body is not CritterBase critter)
			return;
		
		if (critter.Player != this.Player)
			return;

		critter.ShieldPercentage -= this._shieldModifier;
	}
}
