public partial class CritterDiamondSquare : CritterBase
{
	private int _shieldModifier = 2;

	private void OnAuraEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;
		
		if (critter.Player != this.Player)
			return;

		if (critter == this)
			return;

		critter.Shield += this._shieldModifier;
	}
	
	private void OnAuraExited(Node2D body)
	{
		if (body is not CritterBase critter)
			return;
		
		if (critter.Player != this.Player)
			return;

		if (critter == this)
			return;

		critter.Shield -= this._shieldModifier;
	}
}
