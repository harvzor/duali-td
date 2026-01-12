public partial class CritterChevron : CritterBase
{
    private float _speedModifier = 0.25f;


    private void OnAuraEntered(Node2D body)
    {
        if (body is not CritterBase critter)
            return;

        critter.SpeedMultiplier += this._speedModifier;
    }
    
    private void OnAuraExited(Node2D body)
    {
       
        if (body is not CritterBase critter)
            return;

        critter.SpeedMultiplier -= this._speedModifier;
    }
}
