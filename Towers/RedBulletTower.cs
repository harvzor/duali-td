using Godot;

public partial class RedBulletTower : StaticBody2D
{
	public bool Enabled = true;

	private PackedScene RedBullet = GD.Load<PackedScene>("res://Towers/RedBullet.tscn");
	private const int BulletDamage = 5;
	private Node2D _currentTarget;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!this.Enabled)
			return;

		if (IsInstanceValid(_currentTarget))
			this.LookAt(_currentTarget.GlobalPosition);
	}

	public void Disable()
	{
		this.Enabled = false;
		this.GetNode<Panel>("GhostRange").Show();
	}

	public void OnTowerBodyEntered(Node2D body)
	{
		if (!this.Enabled)
			return;
		
		// todo: could be better to keep track of enemies that have entered the area so if the current one has left, we can target the next without it needing to enter the area
		if (IsInstanceValid(_currentTarget))
			return;

		if (body.Name != nameof(Critter))
		{
			return;
		}

		_currentTarget = body;

		var bullet = RedBullet.Instantiate<RedBullet>();
		bullet.Target = _currentTarget;
		bullet.Damage = BulletDamage;
		bullet.Position = this.GetNode<Marker2D>("Aim").Position;
		
		this.GetNode("BulletContainer").AddChild(bullet);
	}

	public void OnTowerBodyExited(Node2D body)
	{
		if (!this.Enabled)
			return;

		if (_currentTarget == body)
		{
			_currentTarget = null;
		}
	}
}
