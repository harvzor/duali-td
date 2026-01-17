using System.Collections.Generic;

public abstract partial class BulletTower : StaticBody2D
{
    [Export] public int BulletDamage = 5;
    [Export] public int BulletSpeed = 100;
    [Export] public int Cost = 10;
    [Export] public bool Enabled = true;

    public int? Player = null;

    protected PackedScene Bullet;

    private readonly List<Node2D> _currentTargets = [];

    public void FireBullet()
    {
        if (this._currentTargets.Count == 0)
            return;

        Node2D currentTarget = this._currentTargets[0];

        if (!IsInstanceValid(currentTarget))
            return;

        Bullet bullet = this.Bullet.Instantiate<Bullet>();

        bullet.Target = currentTarget;
        bullet.Damage = this.BulletDamage;
        bullet.Speed = this.BulletSpeed;
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