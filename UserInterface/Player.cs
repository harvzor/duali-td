public class Player
{
    public int Health { get; private set; } = 100;
    public int Bank { get; private set; } = 100;
    public int Income { get; private set; } = 0;
    
    public void TakeDamage(int damage)
    {
        this.Health -= damage;
    }

    public void IncreaseBank(int amount)
    {
        this.Bank += amount;
    }
    
    public void IncreaseIncome(int amount)
    {
        this.Income += amount;
    }
    
    public bool IsDead() => this.Health <= 0;
}
