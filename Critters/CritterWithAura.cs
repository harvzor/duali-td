public abstract partial class CritterWithAura : CritterBase
{
    public override void Disable()
    {
        base.Disable();

        this.GetNode<Panel>("VisibleAura").Hide();
    }
}
