public partial class BlueBulletTower : BulletTower
{
	public override void _Ready()
	{
		this.Bullet = GD.Load<PackedScene>("res://Towers/BlueBullet.tscn");
		
		base._Ready(); 
	}
}
