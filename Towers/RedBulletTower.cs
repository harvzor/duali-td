using System.Collections.Generic;
using System.Linq;

public partial class RedBulletTower : StaticBody2D
{
	public bool Enabled = true;

	private PackedScene RedBullet = GD.Load<PackedScene>("res://Towers/RedBullet.tscn");
	private const int BulletDamage = 1;
	private readonly List<Node2D> _currentTargets = [];

	public void FireBullet()
	{
		if (_currentTargets.Count == 0)
			return;

		var currentTarget = _currentTargets[0];

		if (!IsInstanceValid(currentTarget))
			return;

		var bullet = RedBullet.Instantiate<RedBullet>();
		bullet.Target = currentTarget;
		bullet.Damage = BulletDamage;
		bullet.Position = this.GetNode<Marker2D>("Aim").Position;
		
		this.GetNode("BulletContainer").AddChild(bullet);
	}

	public override void _Process(double delta)
	{
		if (!this.Enabled)
			return;

		if (_currentTargets.Count == 0)
			return;

		if (!IsInstanceValid(_currentTargets[0]))
		{
			_currentTargets.RemoveAt(0);
			return;
		}

		this.LookAt(_currentTargets[0].GlobalPosition);
	}

	public void Disable()
	{
		this.Enabled = false;
		this.GetNode<Panel>("GhostRange").Show();
	}

	private void OnTowerBodyEntered(Node2D body)
	{
		if (!this.Enabled)
			return;

		if (body.GetType() != typeof(CritterBase))
			return;

		_currentTargets.Add(body);
	}

	private void OnTowerBodyExited(Node2D body)
	{
		if (!this.Enabled)
			return;

		_currentTargets.Remove(body);
	}

	private void OnFireTimerTimeout()
	{
		this.FireBullet();
	}
}
