using System.Collections.Generic;

public partial class CritterChevron : CritterWithAura
{
	private List<CritterBase> _affectedCritters = [];
	
	public override void _Process(double delta)
	{
		base._Process(delta);

		foreach (CritterBase critter in this._affectedCritters)
		{
			critter.SetSpeed(this.Speed);
		}
	}
	
	private void OnAuraEntered(Node2D body)
	{
		if (body is not CritterBase critter)
			return;

		if (critter.Player != this.Player)
			return;

		if (critter == this)
			return;

		if (this.Speed > critter.Speed)
			this._affectedCritters.Add(critter);
	}
	
	private void OnAuraExited(Node2D body)
	{
		if (body is not CritterBase critter)
			return;

		if (critter.Player != this.Player)
			return;

		if (critter == this)
			return;

		this._affectedCritters.Remove(critter);
		critter.ResetSpeed();
	}
}
