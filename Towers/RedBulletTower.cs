public partial class RedBulletTower : StaticBody2D
{
	public bool Enabled = true;

	private PackedScene RedBullet = GD.Load<PackedScene>("res://Towers/RedBullet.tscn");
	private const int BulletDamage = 1;
	private Node2D _currentTarget;

	public void FireBullet()
	{
		if (!IsInstanceValid(_currentTarget))
			return;

		var bullet = RedBullet.Instantiate<RedBullet>();
		bullet.Target = _currentTarget;
		bullet.Damage = BulletDamage;
		bullet.Position = this.GetNode<Marker2D>("Aim").Position;
		
		this.GetNode("BulletContainer").AddChild(bullet);
	}

	public override void _Process(double delta)
	{
		if (!this.Enabled)
			return;

		if (IsInstanceValid(_currentTarget))
		{
			this.LookAt(_currentTarget.GlobalPosition);
		}
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

		if (body.GetType() != typeof(CritterBase))
		{
			return;
		}

		_currentTarget = body;
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

	public void OnFireTimerTimeout()
	{
		this.FireBullet();
	}
}
