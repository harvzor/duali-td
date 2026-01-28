public class Player
{
    public int Health { get; private set; } = 100;
    public int Bank { get; private set; } = 30;
    public int Income { get; private set; } = 10;

    public event Action<int> OnBankChanged;
    public event Action<int> OnIncomeChanged;
    
    public void TakeDamage(int damage)
    {
        this.Health -= damage;
    }

    public void IncreaseBank(int amount)
    {
        this.Bank += amount;
        this.OnBankChanged?.Invoke(this.Bank);
    }
    
    public void IncreaseIncome(int amount)
    {
        this.Income += amount;
        this.OnIncomeChanged?.Invoke(this.Income);
    }
    
    public bool IsDead() => this.Health <= 0;
}
