public partial class RedBulletTower : BulletTower
{
    public override void _Ready()
    {
        this.Bullet = GD.Load<PackedScene>("res://Towers/RedBullet.tscn");
        
        base._Ready(); 
    }
}
