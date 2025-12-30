using System.Collections.Generic;
using System.Linq;

public partial class RedBulletTower : StaticBody2D
{
	public bool Enabled = true;
	public int? Player = null;

	private PackedScene RedBullet = GD.Load<PackedScene>("res://Towers/RedBullet.tscn");
	private const int BulletDamage = 1;
	private readonly List<Node2D> _currentTargets = [];

	public void FireBullet()
	{
		if (this._currentTargets.Count == 0)
			return;

		Node2D currentTarget = this._currentTargets[0];

		if (!IsInstanceValid(currentTarget))
			return;

		RedBullet bullet = this.RedBullet.Instantiate<RedBullet>();
		bullet.Target = currentTarget;
		bullet.Damage = BulletDamage;
		bullet.Position = this.GetNode<Marker2D>("Aim").Position;
		
		this.GetNode("BulletContainer").AddChild(bullet);
	}

	public override void _Process(double delta)
	{
		if (!this.Enabled)
			return;

		if (this._currentTargets.Count == 0)
			return;

		if (!IsInstanceValid(this._currentTargets[0]))
		{
			this._currentTargets.RemoveAt(0);
			return;
		}

		this.LookAt(this._currentTargets[0].GlobalPosition);
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

		if (body is not CritterBase critter)
			return;

		if (critter.Player == this.Player)
			return;

		this._currentTargets.Add(critter);
	}

	private void OnTowerBodyExited(Node2D body)
	{
		if (!this.Enabled)
			return;

		this._currentTargets.Remove(body);
	}

	private void OnFireTimerTimeout()
	{
		this.FireBullet();
	}
}
