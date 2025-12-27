using Godot;

public partial class Gate : Area2D
{
    private UserInterface _userInterface;
	
    public override void _Ready()
    {
        this._userInterface = this
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .FindChild(nameof(UserInterface))! as UserInterface;
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is not Critter critter)
            return;
        
        critter.QueueFree();
        this._userInterface.TakeDamage(critter.Damage);
    }
}
