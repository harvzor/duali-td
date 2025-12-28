public partial class UserInterface : CanvasLayer
{
    private int _health = 100;
    private Label _healthLabel;
    
    private int _bank = 100;
    private Label _bankLabel;
    
    private int _income;
    private Label _incomeLabel;

    private TowerSpawner _towerSpawner;
    private Map _map;

    private RadialProgress _incomeTimerRadial;

    public override void _Ready()
    {
        this._bankLabel = this.GetNode<Label>("PlayerStats/MoneyContainer/Bank");
        this.SetBank(this._bank);
        
        this._incomeLabel = this.GetNode<Label>("PlayerStats/MoneyContainer/Income");
        this.IncreaseIncome(0);
        
        this._healthLabel = this.GetNode<Label>("PlayerStats/HealthContainer/Health");
        this.SetHealth(this._health);

        this._incomeTimerRadial = this.GetNode<RadialProgress>("PlayerStats/IncomeContainer/IncomeTimerRadial");
        
        this._towerSpawner = this
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<TowerSpawner>(nameof(TowerSpawner))!;

        this._map = this
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<Map>(nameof(Map))!;
    }
    
    private void SetHealth(int newHealth)
    {
        this._health = newHealth;
        this._healthLabel.Text = this._health + "❤️";
    }
    
    public void TakeDamage(int damage)
    {
        this.SetHealth(this._health - damage);
    }

    private void SetBank(int newBank)
    {
        this._bank = newBank;
        this._bankLabel.Text = this._bank + "G";
    }
    
    private void IncreaseIncome(int amount)
    {
        this._income += amount;
        this._incomeLabel.Text = this._income + "⬆️";
    }

    public void ShowTower(Vector2 position)
    {
        this._towerSpawner.ShowTower(position);
    }
    
    public void HideTower()
    {
        this._towerSpawner.HideTower();
    }
    
    public void TrySpawnTower(Vector2 position)
    {
        if (this._towerSpawner.Cost > this._bank)
            return;

        if (this._towerSpawner.TowerAlreadyExists(position))
            return;

        this.SetBank(this._bank - this._towerSpawner.Cost);
        this._towerSpawner.SpawnTower(position);
    }

    public void TrySpawnCritter(PackedScene critterScene)
    {
        if (Map.Cost > this._bank)
            return;

        IncreaseIncome(Map.Cost);
        this.SetBank(this._bank - Map.Cost);

        this._map.SpawnCritter(critterScene);
    }

    public void OnIncomeTimerTimeout()
    {
        if (_incomeTimerRadial.Progress >= _incomeTimerRadial.MaxValue)
        {
            this._incomeTimerRadial.Progress = 0;
            this.SetBank(this._bank += this._income);
        }

        this._incomeTimerRadial.Progress += 1;
    }
}
